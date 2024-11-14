using ASCII_Render_Engine.Geometry.Lines;
using ASCII_Render_Engine.Geometry.Primitives;
using ASCII_Render_Engine.ScreenRelated;
using ASCII_Render_Engine.Shader;
using ASCII_Render_Engine.Types.Vectors;
using ASCII_Render_Engine.Utils;
using ASCII_Render_Engine.Demo.Animation;
using System.Globalization;
using Example_ASCII_Game_Engine.GameObjects;

namespace ASCII_Render_Engine
{
    public static class Program
    {
        public static Screen screen = new(200, 100);

        public static void Main(string[] args)
        {
            screen.Background = new FullScreenShaderObject(new SpiralShader(), 0.25);

            // screen config
            screen.Config.Dithering = true;
            screen.Config.FPSCap = 30;
            screen.Config.ScaleToWindow = true;
            screen.Config.VisualizeAsync = true;

            //Console.SetWindowSize(screen.Width * 2, screen.Height);
            //Console.d

            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new Vec2(1, 1), false);

            DateTime fpsStartTime = DateTime.Now;
            int frameCounter = 0;
            double currentFPS = 0;

            DateTime frameStartTime = new();
            DateTime frameEndTime = new();

            // game
            double deltaTime = 0;
            DateTime gFrameEndTime = new();
            DateTime gFrameStartTime = new();

            // ball
            Ball ball = new Ball(new Vec2(10, 10));
            double ballVel = 100;
            Vec2 ballDir = new Vec2(5, 2).NormalizeInPlace();
            Vec2 ballNextPos = new Vec2();

            // paddles
            double paddleSpeed = 5;
            double paddleBorderDistance = 10;
            double paddleWidth = 4;
            double paddleHeight = 10;

            // paddleLeft
            Rectangle paddleLeft = new Rectangle(new Vec2(), new Vec2(paddleWidth, paddleHeight), new Vec2(1, 1));


            gFrameStartTime = DateTime.Now;
            while (true)
            {
                frameStartTime = DateTime.Now;
                gFrameEndTime = DateTime.Now;

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

                frame.Size.SetInPlace(screen.Width, screen.Height);

                deltaTime = (gFrameEndTime - gFrameStartTime).TotalSeconds;

                ballNextPos.x = ball.Pos.x + ballDir.x * ballVel * deltaTime;
                if (ballNextPos.x + ball.Radius > screen.Width || ballNextPos.x - ball.Radius < 0)
                {
                    ballDir.x = -ballDir.x;
                    ballNextPos.x = ball.Pos.x + ballDir.x * ballVel * deltaTime;
                }

                ballNextPos.y = ball.Pos.y + ballDir.y * ballVel * deltaTime;
                if (ballNextPos.y + ball.Radius > screen.Height || ballNextPos.y - ball.Radius < 0)
                {
                    ballDir.y = -ballDir.y;
                    ballNextPos.y = ball.Pos.y + ballDir.y * ballVel * deltaTime;
                }

                ball.Pos.SetInPlace(ballNextPos);
                ball.Radius = 7;

                screen.Clear();

                screen.Draw(ball.ToRenderable());

                screen.Draw(frame);

                // Render (fire-and-forget)
                screen.Render();

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
                gFrameStartTime = gFrameEndTime;
            }
        }
    }
}
