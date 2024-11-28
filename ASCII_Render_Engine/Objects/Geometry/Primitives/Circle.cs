using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vector;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Shader;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Primitives
{
    public class Circle : IRenderable
    {
        public Vec2 Pos;
        public Vec2 Size;
        public Vec2 Color;
        public IShader? Shader;

        // object pool
        private ShaderPixel shaderPixel = new();
        private Vec2 shaderPixelScreenRes = new();

        public Circle(Vec2 pos, Vec2 size, Vec2 color)
        {
            Pos = pos;
            Size = size;
            Color = new Vec2(color.x, color.y);
        }

        public Circle(Vec2 pos, Vec2 size, IShader shader, double alpha = 1)
        {
            Pos = pos;
            Size = size;
            Shader = shader;
            Color = new Vec2(1, alpha);
        }

        public void Render(ScreenBuffer buffer, int frame, double runTime)
        {
            int posx = (int)Math.Floor(Pos.x);
            int posy = (int)Math.Floor(Pos.y);
            int sizex = (int)Math.Ceiling(Size.x) + 1;
            int sizey = (int)Math.Ceiling(Size.y) + 1;

            ShaderPixel pix = shaderPixel;
            shaderPixelScreenRes.x = sizex; shaderPixelScreenRes.y = sizey;
            pix.ScreenRes = shaderPixelScreenRes;


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
}
