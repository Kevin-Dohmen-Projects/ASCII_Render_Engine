using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Rendering.Shaders
{
    public class PulsingDotShader : IShader
    {
        public string Name { get; } = "Pulsing Dot Shader";

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
            Vec2 col = new Vec2();
            Vec2 uv = shaderPixel.UV;
            double frame = shaderPixel.Frame;
            double time = shaderPixel.Time + TimeOffset;

            col.y = 1;

            // Shift uv to center and compute distance from center
            uv += -0.5;
            double distance = Math.Sqrt(uv.x * uv.x + uv.y * uv.y);

            // Set a pulsing effect by varying the radius with frame
            double radius = 0.35 + 0.15 * Math.Sin(time);
            col.x = distance < radius ? 1.0 : 0.0;
            col.y = col.x;

            return col;
        }
    }
}
