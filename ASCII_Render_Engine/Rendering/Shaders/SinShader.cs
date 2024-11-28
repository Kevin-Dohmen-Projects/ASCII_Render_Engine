using ASCII_Render_Engine.MathUtils.Vector;

namespace ASCII_Render_Engine.Rendering.Shaders
{
    public class SinShader : IShader
    {
        public string Name { get; } = "Sinus Shader";

        // ShaderSettings
        public double TimeOffset { get; set; }

        public SinShader()
        {
            TimeOffset = 0;
        }

        public SinShader(double timeOffset)
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
            col.x = (Math.Sin(uv.x * 20 + time * 2) + Math.Sin(uv.y * 20 + time)) / 4 + .5;

            return col;
        }
    }
}
