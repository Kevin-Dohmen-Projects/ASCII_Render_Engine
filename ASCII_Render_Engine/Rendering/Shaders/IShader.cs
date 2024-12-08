using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Rendering.Shaders;

public interface IShader
{
    public string Name { get; }
    public double TimeOffset { get; set; }
    public Vec2 Render(ShaderPixel shaderPixel);
}
