using ASCII_Render_Engine.ScreenRelated;
using ASCII_Render_Engine.Shader;
using ASCII_Render_Engine.Types.Pixels;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Geometry.Primitives
{
    public class FullScreenShaderObject
    {
        Vec2 Color;
        public IShader? Shader;

        // pool
        Vec2 tmpVec = new();
        ShaderPixel shaderPixel = new ShaderPixel();

        public FullScreenShaderObject(IShader? shader, double Alpha = 1)
        {
            Shader = shader;
            Color = new();
            Color.y = Alpha;
            Color.x = 1;
        }
        public FullScreenShaderObject(Vec2 color)
        {
            Color = color;
            Shader = null;
        }

        public void Render(ScreenBuffer buffer, int frame, double time)
        {
            ShaderPixel pix = shaderPixel;
            shaderPixel.ScreenRes.x = buffer.Width;
            shaderPixel.ScreenRes.y = buffer.Height;

            pix.Frame = frame;
            pix.Time = time;

            for (int y = 0; y < buffer.Height; y++)
            {
                for (int x = 0; x < buffer.Width; x++)
                {
                    if (Shader != null)
                    {
                        pix.ScreenPos.x = x;
                        pix.ScreenPos.y = y;
                        pix.UV.DivideInPlace(pix.ScreenPos, pix.ScreenRes);

                        Vec2 col = Shader.Render(pix);
                        buffer.Buffer[y][x].SetInPlace(RenderFuncs.AlphaTransform(col.MultiplyInPlace(Color), buffer.Buffer[y][x], tmpVec));
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
