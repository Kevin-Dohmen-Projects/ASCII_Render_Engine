using ASCII_Render_Engine.Types.Pixels;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Shader
{
    public class SinShader : IShader
    {
        public string Name { get; } = "Sinus Shader";

        // ShaderSettings
        public double TimeOffset { get; set; }

        public SinShader()
        {
            TimeOffset = 0;
        }

        public SinShader(double timeOffset)
        {
            TimeOffset = timeOffset;
        }

        public Vec2 Render(ShaderPixel shaderPixel)
        {
            Vec2 col = shaderPixel.Vec2Pool.GetObject().reset();
            Vec2 uv = shaderPixel.UV;
            double frame = (double)shaderPixel.Frame;
            double time = shaderPixel.Time + TimeOffset;

            col.y = 1;
            
            // shader
            col.x = (Math.Sin(uv.x * 20 + time * 2) + Math.Sin(uv.y * 20 + time)) / 4 + .5;

            shaderPixel.Vec2Pool.ReturnObject(col);
            return col;
        }
    }
}
