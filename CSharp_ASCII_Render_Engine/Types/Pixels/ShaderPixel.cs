using CSharp_ASCII_Render_Engine.Types.Vectors;
using CSharp_ASCII_Render_Engine.Utils;

namespace CSharp_ASCII_Render_Engine.Types.Pixels
{
    public class ShaderPixel
    {
        public Vec2 ScreenPos;
        public Vec2 ScreenRes;
        public Vec2 UV;
        public int Frame;

        // pool
        public ObjectPool<Vec2> Vec2Pool;

        public ShaderPixel()
        {
            ScreenPos = new Vec2();
            ScreenRes = new Vec2();
            UV = new Vec2();
            Vec2Pool = new ObjectPool<Vec2>(100);
        }

        public ShaderPixel(Vec2 screenPos, Vec2 screenRes, int frame)
        {
            ScreenPos = screenPos;
            ScreenRes = screenRes;
            Frame = frame;
            UV = screenPos.DivideInPlace(screenRes);
            Vec2Pool = new ObjectPool<Vec2>(100);
        }
    }
}
