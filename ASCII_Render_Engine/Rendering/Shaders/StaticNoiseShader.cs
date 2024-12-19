using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Rendering.Shaders;

public class StaticNoiseShader : IShader
{
    public string Name { get; } = "Static Noise Shader";

    // ShaderSettings
    public double TimeOffset { get; set; }

    // Source: ChatGPT
    public Vec2 Render(ShaderPixel shaderPixel)
    {
        Vec2 col = new();
        Vec2 uv = shaderPixel.UV;
        double frame = shaderPixel.Frame;

        col.y = 1;

        // Offset UV to give different values for adjacent pixels
        uv += new Vec2(0.123, 0.456);  // Arbitrary offsets

        // Introduce more random "noise" by combining several trigonometric terms
        col.x = Math.Sin((uv.x + frame) * 123.4) * Math.Cos((uv.y + frame) * 432.1);
        col.x += Math.Sin((uv.x - uv.y) * 321.7) * Math.Cos((uv.x + uv.y) * 654.3);
        col.x = (col.x + 2) / 4;  // Normalize to range 0 to 1

        return col;
    }

}
