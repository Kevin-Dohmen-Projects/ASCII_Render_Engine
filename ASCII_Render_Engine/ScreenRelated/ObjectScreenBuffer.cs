using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.ScreenRelated
{
    public class ObjectScreenBuffer
    {
        int PosX;
        int PosY;
        int Width;
        int Height;

        List<List<Vec2>> ObjectBuffer;

        public ObjectScreenBuffer()
        {
            ObjectBuffer = new();
            ObjectBuffer[20][50] = new Vec2(2, 3);
        }
    }
}
