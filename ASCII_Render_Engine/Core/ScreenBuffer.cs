using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Core;

public class ScreenBuffer
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Vec2[][] Buffer;

    public ScreenBuffer(int width, int height)
    {
        Width = width;
        Height = height;
        InitBuffer();
    }

    private void InitBuffer()
    {
        Buffer = new Vec2[Height][];
        for (int i = 0; i < Height; i++)
        {
            Buffer[i] = (new Vec2[Width]);
            for (int j = 0; j < Width; j++)
            {
                Buffer[i][j] = new Vec2(0, 1);
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                Buffer[i][j] = new Vec2(0, 1);
            }
        }
    }
}
