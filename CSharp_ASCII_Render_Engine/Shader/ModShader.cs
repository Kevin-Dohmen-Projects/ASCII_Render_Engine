using CSharp_ASCII_Render_Engine.Types.Pixels;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Shader
{
    public class ModShader : IShader
    {
        public Vec2 Render(ShaderPixel shaderPixel)
        {
            double col;
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;

            col = ((uv.x+frame/50)%0.25)*4;


            shaderPixel.Col.x = col;
            shaderPixel.Col.y = 1;

            return shaderPixel.Col;
        }
    }
}
