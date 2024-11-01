using CSharp_ASCII_Render_Engine.Geometry.Primitives;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using CSharp_ASCII_Render_Engine.Utils;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public class Screen
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int Frame { get; private set; }

        public RenderQueue Queue { get; private set; }
        public ScreenBuffer Buffer { get; private set; }
        private ASCIIConverter Converter = new();

        public FullScreenShaderObject? Background;


        // pools
        ObjectPool<Vec2> Vec2Pool = new(100_000);

        public Screen(int width, int height)
        {
            Width = width;
            Height = height;
            Queue = new RenderQueue();
            Buffer = new ScreenBuffer(width, height);
        }

        public void Clear()
        {
            Queue.Clear();
            Buffer.Clear();
        }

        public void Draw(IRenderable item)
        {
            Queue.Add(item);
        }

        public void Render()
        {
            Frame++;

            if (Background != null)
            {
                Background.Render(Buffer, Frame);
            }

            foreach (var item in Queue.Queue)
            {
                item.Render(Buffer, Frame);
            }
            Queue.Clear();

            string fullScreen = Converter.BufferToFullScreen(Buffer);
            Console.WriteLine(fullScreen);
        }
    }
}
