using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class StaticNoiseShader : IShader
    {
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            // Offset UV to give different values for adjacent pixels
            uv.AddInPlace(0.123, 0.456);  // Arbitrary offsets

            // Introduce more random "noise" by combining several trigonometric terms
            double col = Math.Sin((uv.x + frame) * 123.4) * Math.Cos((uv.y + frame) * 432.1);
            col += Math.Sin((uv.x - uv.y) * 321.7) * Math.Cos((uv.x + uv.y) * 654.3);
            col = (col + 2) / 4;  // Normalize to range 0 to 1

            shaderPixel.Col.x = col;  // luminance
            shaderPixel.Col.y = 1;    // transparency
            return shaderPixel.Col;
        }

    }
}
