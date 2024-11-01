using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class SinShader : IShader
    {
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = shaderPixel.Vec2Pool.GetObject().reset();
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            col.y = 1;
            
            // shader
            col.x = (Math.Sin(uv.x * 20 + frame / 2) + Math.Sin(uv.y * 20 + frame / 4)) / 4 + .5;

            shaderPixel.Vec2Pool.ReturnObject(col);
            return col;
        }
    }
}
