using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class StaticNoiseShader : IShader
    {
        public string Name { get; } = "Static Noise Shader";

        // ShaderSettings
        public double TimeOffset { get; set; }

        // Source: ChatGPT
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = shaderPixel.Vec2Pool.GetObject().reset();
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            col.y = 1;

            // Offset UV to give different values for adjacent pixels
            uv.AddInPlace(0.123, 0.456);  // Arbitrary offsets

            // Introduce more random "noise" by combining several trigonometric terms
            col.x = Math.Sin((uv.x + frame) * 123.4) * Math.Cos((uv.y + frame) * 432.1);
            col.x += Math.Sin((uv.x - uv.y) * 321.7) * Math.Cos((uv.x + uv.y) * 654.3);
            col.x = (col.x + 2) / 4;  // Normalize to range 0 to 1

            shaderPixel.Vec2Pool.ReturnObject(col);
            return col;
        }

    }
}
