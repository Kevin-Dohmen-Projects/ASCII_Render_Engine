using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.Rendering.Shaders;
using System.Drawing;

namespace ASCII_Render_Engine.Rendering.Geometry.Primitives.RectangleRenderer;

public class RectangleRenderer : IRectangleRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj)
    {
        if (obj is not Rectangle2D rect)
        {
            throw new ArgumentException("Object is not a Rectangle2D");
        }

        Vec2 Pos = rect.Pos;
        Vec2 Size = rect.Size;
        Vec2 Color = rect.Color;
        bool IsFilled = rect.IsFilled;
        IShader? Shader = rect.Shader;

        int posx = (int)Math.Floor(Pos.x);
        int posy = (int)Math.Floor(Pos.y);
        int sizex = (int)Math.Ceiling(Size.x);
        int sizey = (int)Math.Ceiling(Size.y);

        if (IsFilled)
        {
            ShaderPixel pix = new ShaderPixel();
            pix.ScreenRes = new Vec2(buffer.Width, buffer.Height);

            pix.Frame = frame;
            pix.Time = runTime;

            for (int y = int.Max(posy, 0); y < int.Min(posy + sizey, buffer.Height); y++)
            {
                for (int x = int.Max(posx, 0); x < int.Min(posx + sizex, buffer.Width); x++)
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
        else
        {
            for (int y = int.Max(posy, 0); y < int.Min(posy + sizey, buffer.Height); y++)
            {
                for (int x = int.Max(posx, 0); x < int.Min(posx + sizex, buffer.Width); x++)
                {
                    if (x == posx || x == posx + sizex - 1 || y == posy || y == posy + sizey - 1)
                        buffer.Buffer[y][x] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[y][x]);
                }
            }
        }
    }
}
