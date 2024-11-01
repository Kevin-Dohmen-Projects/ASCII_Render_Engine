using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class CheckerboardShader : IShader
    {
        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            double col = 0;
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            // Checkerboard pattern using the floor of uv coordinates
            col = ((Math.Floor(uv.x * 10) + Math.Floor(uv.y * 10)) % 2 == 0) ? 1.0 : 0.0;

            shaderPixel.Col.x = col; // luminance
            shaderPixel.Col.y = 1;   // transparency
            return shaderPixel.Col;
        }
    }
}
