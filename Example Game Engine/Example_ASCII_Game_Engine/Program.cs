using ASCII_Render_Engine.Geometry.Lines;
using ASCII_Render_Engine.Geometry.Primitives;
using ASCII_Render_Engine.ScreenRelated;
using ASCII_Render_Engine.Shader;
using ASCII_Render_Engine.Types.Vectors;
using ASCII_Render_Engine.Utils;
using ASCII_Render_Engine.Demo.Animation;
using System.Globalization;

namespace ASCII_Render_Engine
{
    public static class Program
    {
        public static Screen screen = new(150, 150);

        public static void Main(string[] args)
        {
            screen.Background = new FullScreenShaderObject(new SinShader());

            // screen config
            screen.Config.Dithering = true;
            screen.Config.FPSCap = 20;
            screen.Config.ScaleToWindow = true;
            screen.Config.VisualizeAsync = true;

            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new Vec2(1, 1), false);

            DateTime fpsStartTime = DateTime.Now;
            int frameCounter = 0;
            double currentFPS = 0;

            DateTime frameStartTime = new();
            DateTime frameEndTime = new();

            while (true)
            {
                frameStartTime = DateTime.Now;

                // Frame counting
                frameCounter++;

                // If one second has passed, calculate the FPS and reset counter
                if ((DateTime.Now - fpsStartTime).TotalSeconds >= 1)
                {
                    currentFPS = frameCounter / (DateTime.Now - fpsStartTime).TotalSeconds;
                    fpsStartTime = DateTime.Now;
                    frameCounter = 0;

                    // Display the FPS in the console (optional)
                    Console.Title = $"FPS: {currentFPS:F2}"; // Update console title with FPS
                }

                if (screen.Config.ScaleToWindow)
                {
                    screen.ScaleToWindow();
                    frame.Size.SetInPlace(screen.Width, screen.Height);
                }

                screen.Clear();

                // api-injected objects
                foreach (ShapeData shape in ShapeStore.GetShapes())
                {
                    screen.Draw(shape.shape);
                }

                screen.Draw(frame);

                // Render (fire-and-forget)
                screen.RenderAsync();

                frameEndTime = DateTime.Now;

                // Frame cap: calculate time taken for rendering
                double elapsedTime = (frameEndTime - frameStartTime).TotalSeconds;
                double targetFrameTime = 1.0 / screen.Config.FPSCap; // Target frame time for 30 FPS is around 33.33ms

                // Calculate the remaining time to maintain the FPS cap
                double sleepTime = targetFrameTime - elapsedTime;
                if (sleepTime > 0)
                {
                    // Sleep for the remaining time to maintain FPS
                    Thread.Sleep((int)(sleepTime * 1000)); // Convert seconds to milliseconds
                }
            }
        }

    }
}
