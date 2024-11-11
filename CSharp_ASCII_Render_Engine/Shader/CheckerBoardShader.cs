using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class CheckerboardShader : IShader
    {
        public string Name { get; } = "Checkerboard Shader";

        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = shaderPixel.Vec2Pool.GetObject().reset();
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            col.y = 1;

            // Checkerboard pattern using the floor of uv coordinates
            col.x = ((Math.Floor(uv.x * 10) + Math.Floor(uv.y * 10)) % 2 == 0) ? 1.0 : 0.0;

            shaderPixel.Vec2Pool.ReturnObject(col);
            return col;
        }
    }
}
