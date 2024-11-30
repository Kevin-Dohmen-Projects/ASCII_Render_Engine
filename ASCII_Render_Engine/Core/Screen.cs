using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Utils;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.Utils.Profiling;

namespace ASCII_Render_Engine.Core
{
    public class Screen
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public RenderQueue Queue { get; private set; }
        public ScreenBuffer Buffer { get; private set; }

        // -=-=-=- counters -=-=-=-
        public int Frame { get; private set; }

        // Profiling
        public readonly TimeProfiler RenderTimer = new();
        public readonly TimeProfiler VisualizeTimer = new();
        public readonly TimeProfiler FrameTimer = new();


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
                SetRes(newWidth, newHeight);
                Console.Clear();
                ClearBeforeVisualize = true;
            }
            return (newWidth, newHeight);
        }

        public (int, int) CenterScreen()
        {
            int offsetX = (Console.WindowWidth / 4 - Width / 2) * 2;
            int offsetY = (Console.WindowHeight - 1) / 2 - Height / 2;
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
                RenderTimer.Start();
                FrameTimer.Lap();

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

                RenderTimer.Stop();

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
            VisualizeTimer.Start();

            if (ClearBeforeVisualize)
            {
                Console.Clear();
                ClearBeforeVisualize = false;
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(displayString);

            VisualizeTimer.Stop();
        }

        public async Task VisualizeAsync(string displayString)
        {
            VisualizeTimer.Start();

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

            VisualizeTimer.Stop();
        }
    }
}
