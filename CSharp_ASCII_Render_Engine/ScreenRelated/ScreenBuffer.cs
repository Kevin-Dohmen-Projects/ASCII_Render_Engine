using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public class ScreenBuffer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<List<Vec2>> Buffer;

        public ScreenBuffer(int width, int height)
        {
            Width = width;
            Height = height;
            InitBuffer();
        }

        private void InitBuffer()
        {
            Buffer = new List<List<Vec2>>();
            for (int i = 0; i < Height; i++)
            {
                Buffer.Add(new List<Vec2>());
                for (int j = 0; j < Width; j++)
                {
                    Buffer[i].Add(new Vec2());
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Buffer[i][j].SetInPlace(0, 0);
                }
            }
        }
    }
}
