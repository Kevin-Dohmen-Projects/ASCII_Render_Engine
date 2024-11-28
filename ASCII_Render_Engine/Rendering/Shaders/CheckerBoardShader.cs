using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Rendering.Shaders
{
    public class CheckerboardShader : IShader
    {
        public string Name { get; } = "Checkerboard Shader";

        // ShaderSettings
        public double TimeOffset { get; set; }

        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = new Vec2();
            Vec2 uv = shaderPixel.UV;
            double frame = shaderPixel.Frame;

            col.y = 1;

            // Checkerboard pattern using the floor of uv coordinates
            col.x = (Math.Floor(uv.x * 10) + Math.Floor(uv.y * 10)) % 2 == 0 ? 1.0 : 0.0;

            return col;
        }
    }
}
