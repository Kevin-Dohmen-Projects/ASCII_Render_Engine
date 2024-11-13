using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public interface IShader
    {
        public string Name { get; }
        public double TimeOffset { get; set; }
        public Vec2 Render(ShaderPixel shaderPixel);
    }
}
