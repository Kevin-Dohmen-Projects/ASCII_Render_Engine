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

        private Display Display;

        public FullScreenShaderObject? Background;

        public ScreenConfig Config;

        // pools
        ObjectPool<Vec2> Vec2Pool = new(100_000);

        public Screen(int width, int height)
        {
            Width = width;
            Height = height;
            Queue = new RenderQueue();
            Buffer = new ScreenBuffer(width, height);
            Display = new Display(width, height);
            Config = new();
        }

        public void SetRes(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = new ScreenBuffer(width, height);
            Display = new Display(width, height);
            Config = new();
        }

        public (int, int) ScaleToWindow()
        {
            int newWidth = Console.WindowWidth / 2;
            int newHeight = Console.WindowHeight - 4;
            if (Height != newHeight || Width != newWidth)
            {
                this.SetRes(newWidth, newHeight);
                Console.Clear();
            }
            return (newWidth, newHeight);
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

            string fullScreen = Converter.BufferToFullScreen(Buffer, Display, Config).ToString();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Display.fullScreenString);
        }
    }

    public struct ScreenConfig
    {
        // Resolution:
        public bool ScaleToWindow;

        // Render settings:
        public bool Dithering;
        public double FPSCap;

        public ScreenConfig()
        {
            ScaleToWindow = false;

            Dithering = false;
            FPSCap = 30;
        }
    }
}
