using ASCII_Render_Engine.Types.Vectors;
using ASCII_Render_Engine.Utils;
using ASCII_Render_Engine.Objects.Geometry.Primitives;

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

        // Render Time tracking
        private DateTime renderStartTime = DateTime.Now;
        private DateTime prevRenderStartTime = DateTime.Now;
        private bool rendering = false;
        private DateTime renderEndTime = DateTime.Now;
        public double RenderTime // Time taken to render the last frame in milliseconds
        {
            get
            {
                return rendering
                    ? (DateTime.Now - renderStartTime).TotalMilliseconds
                    : (renderEndTime - prevRenderStartTime).TotalMilliseconds;
            }
        }

        // Visualize Time tracking
        private DateTime visualizeStartTime = DateTime.Now;
        private DateTime prevVisualizeStartTime = DateTime.Now;
        private bool visualizing = false;
        private DateTime visualizeEndTime = DateTime.Now;
        public double VisualizeTime // Time taken to visualize the last frame in milliseconds
        {
            get
            {
                return visualizing
                    ? (DateTime.Now - visualizeStartTime).TotalMilliseconds
                    : (visualizeEndTime - prevVisualizeStartTime).TotalMilliseconds;
            }
        }


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
                renderStartTime = DateTime.Now;
                rendering = true;

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

                renderEndTime = DateTime.Now;
                prevRenderStartTime = renderStartTime;
                rendering = false;

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
            visualizeStartTime = DateTime.Now;
            visualizing = true;

            if (ClearBeforeVisualize)
            {
                Console.Clear();
                ClearBeforeVisualize = false;
            }

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(displayString);

            visualizeEndTime = DateTime.Now;
            prevVisualizeStartTime = visualizeStartTime;
            visualizing = false;
        }

        public async Task VisualizeAsync(string displayString)
        {
            visualizeStartTime = DateTime.Now;
            visualizing = true;

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

            visualizeEndTime = DateTime.Now;
            prevVisualizeStartTime = visualizeStartTime;
            visualizing = false;
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
