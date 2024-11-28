using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Utils;

namespace ASCII_Render_Engine.Rendering
{
    public class ShaderPixel
    {
        public Vec2 ScreenPos;
        public Vec2 ScreenRes;
        public Vec2 UV;
        public int Frame; // frame count
        public double Time; // time in seconds

        public ShaderPixel()
        {
            ScreenPos = new Vec2();
            ScreenRes = new Vec2();
            UV = new Vec2();
        }

        public ShaderPixel(Vec2 screenPos, Vec2 screenRes, int frame)
        {
            ScreenPos = screenPos;
            ScreenRes = screenRes;
            Frame = frame;
            UV = screenPos / screenRes;
        }
    }
}
