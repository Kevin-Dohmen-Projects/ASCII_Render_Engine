using ASCII_Render_Engine.Types.Pixels;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Shader
{
    public class PulsingDotShader : IShader
    {
        public string Name { get; } = "Pulsing Dot Shaders";

        // ShaderSettings
        public double TimeOffset { get; set; }

        public PulsingDotShader()
        {
            TimeOffset = 0;
        }

        public PulsingDotShader(double timeOffset)
        {
            TimeOffset = timeOffset;
        }

        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = shaderPixel.Vec2Pool.GetObject().reset();
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;
            double time = shaderPixel.Time + TimeOffset;

            col.y = 1;

            // Shift uv to center and compute distance from center
            uv.AddInPlace(-0.5, -0.5);
            double distance = Math.Sqrt(uv.x * uv.x + uv.y * uv.y);

            // Set a pulsing effect by varying the radius with frame
            double radius = 0.35 + 0.15 * Math.Sin(time);
            col.x = distance < radius ? 1.0 : 0.0;
            col.y = col.x;

            shaderPixel.Vec2Pool.ReturnObject(col);
            return col;
        }
    }
}
