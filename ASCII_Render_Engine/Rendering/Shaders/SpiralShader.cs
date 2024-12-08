using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Rendering.Shaders;

public class SpiralShader : IShader
{
    public string Name { get; } = "Spiral Shader";

    // ShaderSettings
    public double TimeOffset { get; set; }

    public SpiralShader()
    {
        TimeOffset = 0;
    }

    public SpiralShader(double timeOffset)
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

        // Shift uv to center for a spiral effect
        uv += -0.5;

        // Compute angle and radius from center
        double angle = Math.Atan2(uv.y, uv.x);
        double radius = Math.Sqrt(uv.x * uv.x + uv.y * uv.y);

        // Create a rotating spiral effect
        col.x = (Math.Sin(10 * radius - time * 0.2 + angle) + 1) / 2;

        return col;
    }

}
