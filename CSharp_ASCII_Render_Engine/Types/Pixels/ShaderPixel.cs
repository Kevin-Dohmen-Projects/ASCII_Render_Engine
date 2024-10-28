using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.Types.Pixels
{
    public class ShaderPixel
    {
        public Vec2 ScreenPos;
        public Vec2 ScreenRes;
        public Vec2 UV;
        public int Frame;

        // pool
        public Vec2 Col;

        public ShaderPixel()
        {
            ScreenPos = new Vec2();
            ScreenRes = new Vec2();
            UV = new Vec2();
            Col = new Vec2();
        }

        public ShaderPixel(Vec2 screenPos, Vec2 screenRes, int frame)
        {
            ScreenPos = screenPos;
            ScreenRes = screenRes;
            Frame = frame;
            UV = screenPos.DivideInPlace(screenRes);
            Col = new Vec2(0, 0);
        }
    }
}
