using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Objects.Geometry.Primitives;

public class FullScreenShaderObject
{
    Vec2 Color;
    public IShader? Shader;

    // pool
    ShaderPixel shaderPixel = new ShaderPixel();

    public FullScreenShaderObject(IShader? shader, double Alpha = 1)
    {
        Shader = shader;
        Color = new(1, Alpha);
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
