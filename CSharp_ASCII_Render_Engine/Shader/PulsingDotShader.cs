using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class PulsingDotShader : IShader
    {
        public string Name { get; } = "Pulsing Dot Shaders";

        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = shaderPixel.Vec2Pool.GetObject().reset();
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            col.y = 1;

            // Shift uv to center and compute distance from center
            uv.AddInPlace(-0.5, -0.5);
            double distance = Math.Sqrt(uv.x * uv.x + uv.y * uv.y);

            // Set a pulsing effect by varying the radius with frame
            double radius = 0.25 + 0.1 * Math.Sin(frame * 0.1);
            col.x = distance < radius ? 1.0 : 0.0;
            col.y = col.x;

            shaderPixel.Vec2Pool.ReturnObject(col);
            return col;
        }
    }
}
