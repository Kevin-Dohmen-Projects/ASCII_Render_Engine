using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Lines;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.Utils.Profiling;

namespace BasicFrameLoop;

public static class Program
{
    public static Screen screen = new(200, 100);

    public static void Main(string[] args)
    {
        // -=-=-=- Screen Config -=-=-=-
        screen.Config.FPSCap = 0;
        screen.Config.ScaleToWindow = true;
        screen.Config.Dithering = true;
        screen.Config.CenterScreen = true;
        screen.Background = new FullScreenShaderObject(new SpiralShader(), 1);

        // -=-=-=- Object Initialisation -=-=-=-
        Rectangle rect1 = new(new Vec2(10, 10), new Vec2(50, 50), new Vec2(0.5, 1));
        Rectangle shaderRect = new(new Vec2(10, 65), new Vec2(50, 50), new SinShader());
        Rectangle rect2 = new(new Vec2(35, 10), new Vec2(50, 105), new Vec2(1, 0.5));
        Line2D line1 = new Line2D(new Vec2(10, 10), new Vec2(85, 115), new Vec2(1, 1));
        Line2D line2 = new Line2D(new Vec2(), new Vec2(), new Vec2(1, 1));

        // -=-=-=- Counters -=-=-=-
        int frames = 0;
        double runTime = 0;
        double deltaTime = 0;
        DateTime startTime = DateTime.Now;
        DateTime frameStart = DateTime.Now;
        DateTime lastFPSTime = DateTime.Now;
        DateTime roundStartTime = DateTime.Now;
        TimeProfiler logicProfiler = new();

        // -=-=-=- Game Loop -=-=-=-
        while (true)
        {
            // update counters
            frameStart = DateTime.Now;
            logicProfiler.Start();
            runTime = (frameStart - startTime).TotalSeconds;
            deltaTime = screen.FrameTimer.ElapsedTime / 1000;
            frames++;

            // update FPS in console title every half second
            if ((frameStart - lastFPSTime).TotalSeconds >= 0.5)
            {
                double fps = 1 / (screen.FrameTimer.ElapsedTime / 1000);
                Console.Title = $"{logicProfiler.ElapsedTime:F1}ms|{screen.RenderTimer.ElapsedTime:F1}ms|{screen.VisualizeTimer.ElapsedTime:F1}ms|{fps:F0}FPS";
                lastFPSTime = frameStart;
            }

            // -=-=-=- Logic -=-=-=-
            // Add your logic here
            Vec2 screenCenter = new Vec2(screen.Width, screen.Height) / 2;
            line2.A = screenCenter;
            line2.B = screenCenter + (new Vec2(Math.Sin(runTime), -Math.Cos(runTime)) * 40);


            logicProfiler.Stop();

            // -=-=-=- Drawing -=-=-=-
            screen.Clear();

            // Draw your objects
            screen.Draw(rect1);
            screen.Draw(shaderRect);
            screen.Draw(rect2);
            screen.Draw(line1);
            screen.Draw(line2);

            // -=-=-=- Rendering -=-=-=-
            screen.Render();

            // frame limiter
            if (screen.Config.FPSCap != 0)
            {
                double frameTime = (DateTime.Now - frameStart).TotalMilliseconds;
                double targetFrameTime = 1000.0 / screen.Config.FPSCap;
                if (frameTime < targetFrameTime)
                {
                    int sleepTime = (int)(targetFrameTime - frameTime);
                    Thread.Sleep(sleepTime);
                }
            }
        }
    }
}