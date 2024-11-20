using ASCII_Render_Engine.Geometry.Primitives;
using ASCII_Render_Engine.Types.Vectors;
using ASCII_Render_Engine.Utils;
using ASCII_Render_Engine.ScreenRelated;

namespace ASCII_Render_Engine
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
        private DateTime StartTime;

        private SemaphoreSlim consoleLock = new SemaphoreSlim(1, 1);
        private Task lastVisualizationTask = Task.CompletedTask; // Initialize as completed task

        private Task lastRenderTask = Task.CompletedTask;
        private readonly SemaphoreSlim renderSemaphore = new SemaphoreSlim(1, 1);

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
            StartTime = DateTime.Now;
        }

        public void SetRes(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = new ScreenBuffer(width, height);
            Display = new Display(width, height);
        }

        public (int, int) ScaleToWindow()
        {
            int newWidth = Console.WindowWidth / 2;
            int newHeight = Console.WindowHeight - 1;
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

        public async Task Render()
        {
            // Ensure only one RenderAsync runs at a time
            await renderSemaphore.WaitAsync();
            try
            {
                if (Config.ScaleToWindow)
                {
                    ScaleToWindow();
                }

                Frame++;
                double runTime = (DateTime.Now - StartTime).TotalSeconds;

                if (Background != null)
                {
                    Background.Render(Buffer, Frame, runTime);
                }

                foreach (var item in Queue.Queue)
                {
                    item.Render(Buffer, Frame, runTime);
                }

                Queue.Clear();

                string fullScreen = Converter.BufferToFullScreen(Buffer, Display, Config).ToString();

                if (Config.VisualizeAsync)
                {
                    // Schedule visualization but track it in lastRenderTask to control the sequence
                    lastRenderTask = VisualizeAsync(fullScreen);
                }
                else
                {
                    Visualize(fullScreen);
                }
            }
            finally
            {
                renderSemaphore.Release(); // Allow the next render to proceed
            }
        }

        public void Visualize(string displayString)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(displayString);
        }

        public async Task VisualizeAsync(string displayString)
        {
            await consoleLock.WaitAsync(); // Wait for exclusive access to the console
            try
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(displayString);
            }
            finally
            {
                consoleLock.Release(); // Allow other threads to proceed
            }
        }
    }

    public struct ScreenConfig
    {
        // Resolution:
        public bool ScaleToWindow;

        // Render settings:
        public bool Dithering;
        public double FPSCap;

        public bool VisualizeAsync;

        public ScreenConfig()
        {
            ScaleToWindow = false;

            Dithering = false;
            FPSCap = 30;

            VisualizeAsync = false;
        }
    }
}
