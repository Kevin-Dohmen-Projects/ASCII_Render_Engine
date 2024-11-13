using CSharp_ASCII_Render_Engine.ScreenRelated;
using CSharp_ASCII_Render_Engine.Shader;
using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Geometry.Primitives
{
    public class Circle: IRenderable
    {
        public Vec2 Pos;
        public Vec2 Size;
        public Vec2 Color;
        public IShader? Shader;

        // object pool
        private ShaderPixel shaderPixel = new();
        private Vec2 shaderPixelScreenRes = new();
        private Vec2 tmpVec = new();
        private Vec2 circleUV = new();

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

        public void Render(ScreenBuffer buffer, int frame)
        {
            int posx = (int)Math.Floor(Pos.x);
            int posy = (int)Math.Floor(Pos.y);
            int sizex = (int)Math.Ceiling(Size.x);
            int sizey = (int)Math.Ceiling(Size.y);

            ShaderPixel pix = shaderPixel;
            shaderPixelScreenRes.x = sizex; shaderPixelScreenRes.y = sizey;
            pix.ScreenRes = shaderPixelScreenRes;

            for (int y = int.Max(posy, 0); y < int.Min(posy + sizey, buffer.Height); y++)
            {
                for (int x = int.Max(posx, 0); x < int.Min(posx + sizex, buffer.Height); x++)
                {
                    circleUV.SetInPlace(
                        (x - Pos.x) / Size.x,
                        (y - Pos.y) / Size.y
                    ).AddInPlace(-0.5);

                    if (circleUV.Length() < 0.5)
                    {
                        if (Shader != null)
                        {
                            pix.ScreenPos.x = x - posx;
                            pix.ScreenPos.y = y - posy;
                            pix.Frame = frame;
                            pix.UV.DivideInPlace(pix.ScreenPos, pix.ScreenRes);

                            Vec2 col = Shader.Render(pix);
                            buffer.Buffer[y][x].SetInPlace(RenderFuncs.AlphaTransform(col.MultiplyInPlace(Color), buffer.Buffer[y][x], tmpVec));
                        }
                        else
                        {
                            buffer.Buffer[y][x].SetInPlace(RenderFuncs.AlphaTransform(Color, buffer.Buffer[y][x], tmpVec));
                        }
                    }
                }
            }
        }
    }
}
