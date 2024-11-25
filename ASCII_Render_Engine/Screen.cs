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
        private int DisplayOffsetX = 0;
        private int DisplayOffsetY = 0;

        private bool ClearBeforeVisualize = true; // resets to false after each render

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

            Console.CursorVisible = false;
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
                ClearBeforeVisualize = true;
            }
            return (newWidth, newHeight);
        }

        public (int, int) CenterScreen()
        {
            int offsetX = ((Console.WindowWidth / 4) - (Width / 2)) * 2;
            int offsetY = ((Console.WindowHeight - 1) / 2) - Height / 2;
            if (offsetX != DisplayOffsetX || offsetY != DisplayOffsetY)
            {

                DisplayOffsetX = offsetX;
                DisplayOffsetY = offsetY;
                Display.SetOffset(offsetX, offsetY);
                ClearBeforeVisualize = true;
            }
            return (offsetX, offsetY);
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
                else if (Config.CenterScreen)
                {
                    CenterScreen();
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
            if (ClearBeforeVisualize)
            {
                Console.Clear();
                ClearBeforeVisualize = false;
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(displayString);
        }

        public async Task VisualizeAsync(string displayString)
        {
            await consoleLock.WaitAsync(); // Wait for exclusive access to the console

            if (ClearBeforeVisualize)
            {
                Console.Clear();
                ClearBeforeVisualize = false;
            }

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
        public bool CenterScreen;

        public bool VisualizeAsync;

        public ScreenConfig()
        {
            ScaleToWindow = false;

            Dithering = false;
            FPSCap = 30;
            CenterScreen = false;

            VisualizeAsync = false;
        }
    }
}
