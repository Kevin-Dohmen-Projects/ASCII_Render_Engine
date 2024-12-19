using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Rendering.Shaders;

public interface IShader
{
    public string Name { get; }
    public double TimeOffset { get; set; }
    public Vec2 Render(ShaderPixel shaderPixel);
}
