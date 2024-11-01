using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class PulsingDotShader : IShader
    {
        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            double col = 0;
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            // Shift uv to center and compute distance from center
            uv.AddInPlace(-0.5, -0.5);
            double distance = Math.Sqrt(uv.x * uv.x + uv.y * uv.y);

            // Set a pulsing effect by varying the radius with frame
            double radius = 0.25 + 0.1 * Math.Sin(frame * 0.1);
            col = distance < radius ? 1.0 : 0.0;

            shaderPixel.Col.x = col; // luminance
            shaderPixel.Col.y = 1;   // transparency
            return shaderPixel.Col;
        }
    }
}
