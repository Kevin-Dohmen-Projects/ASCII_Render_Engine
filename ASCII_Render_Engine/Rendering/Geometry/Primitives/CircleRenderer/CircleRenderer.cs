using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using System.Drawing;

namespace ASCII_Render_Engine.Rendering.Geometry.Primitives.CircleRenderer;

public class CircleRenderer : ICircleRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj)
    {
        if (obj is not Circle2D circle)
        {
            throw new ArgumentException("Object is not a Circle");
        }

        Vec2 Pos = circle.Pos;
        Vec2 Size = circle.Size;
        Vec2 Color = circle.Color;
        IShader? Shader = circle.Shader;

        int posx = (int)Math.Floor(Pos.x);
        int posy = (int)Math.Floor(Pos.y);
        int sizex = (int)Math.Ceiling(Size.x) + 1;
        int sizey = (int)Math.Ceiling(Size.y) + 1;

        ShaderPixel pix = new ShaderPixel();
        pix.ScreenRes = new Vec2(buffer.Width, buffer.Height);


        pix.Frame = frame;
        pix.Time = runTime;

        for (int y = int.Max(posy, 0); y < int.Min(posy + sizey, buffer.Height); y++)
        {
            for (int x = int.Max(posx, 0); x < int.Min(posx + sizex, buffer.Width); x++)
            {
                Vec2 circleUV = new Vec2(
                    (x - Pos.x) / Size.x - 0.5,
                    (y - Pos.y) / Size.y - 0.5
                );

                if (circleUV.Length() < 0.5)
                {
                    if (Shader != null)
                    {
                        pix.ScreenPos.x = x - posx;
                        pix.ScreenPos.y = y - posy;
                        pix.UV = pix.ScreenPos / pix.ScreenRes;

                        Vec2 col = Shader.Render(pix);
                        buffer.Buffer[y][x] = RenderFuncs.AlphaTransform(col * Color, buffer.Buffer[y][x]);
                    }
                    else
                    {
                        buffer.Buffer[y][x] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[y][x]);
                    }
                }
            }
        }
    }
}
