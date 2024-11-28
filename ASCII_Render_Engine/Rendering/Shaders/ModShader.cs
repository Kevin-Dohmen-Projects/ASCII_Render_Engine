using ASCII_Render_Engine.MathUtils.Vector;

namespace ASCII_Render_Engine.Rendering.Shaders
{
    public class ModShader : IShader
    {
        public string Name { get; } = "Modulo Shader";

        // Shader Settings
        public double TimeOffset { get; set; }

        public ModShader()
        {
            TimeOffset = 0;
        }

        public ModShader(double timeOffset)
        {
            TimeOffset = timeOffset;
        }

        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = new Vec2();
            Vec2 uv = shaderPixel.UV;
            double frame = shaderPixel.Frame;
            double time = shaderPixel.Time + TimeOffset;

            col.y = 1;

            // shader
            col.x = (uv.x + time * 0.1) % 0.25 * 4;

            return col;
        }
    }
}
