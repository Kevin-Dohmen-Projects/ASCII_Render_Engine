using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class SpiralShader : IShader
    {
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            double col = 0;
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            // Shift uv to center for a spiral effect
            uv.AddInPlace(-0.5, -0.5);

            // Compute angle and radius from center
            double angle = Math.Atan2(uv.y, uv.x);
            double radius = Math.Sqrt(uv.x * uv.x + uv.y * uv.y);

            // Create a rotating spiral effect
            col = (Math.Sin(10 * radius - frame * 0.1 + angle) + 1) / 2;

            shaderPixel.Col.x = col; // luminance
            shaderPixel.Col.y = 1;   // transparency
            return shaderPixel.Col;
        }

    }
}
