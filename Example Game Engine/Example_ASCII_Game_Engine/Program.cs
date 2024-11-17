using System;
using ASCII_Render_Engine.Geometry.Lines;
using ASCII_Render_Engine.Geometry.Primitives;
using ASCII_Render_Engine.ScreenRelated;
using ASCII_Render_Engine.Shader;
using ASCII_Render_Engine.Types.Vectors;
using ASCII_Render_Engine.Utils;
using System.Globalization;
using Example_ASCII_Game_Engine.GameObjects;
using ASCII_Render_Engine;
using System.Reflection.Metadata;

using ASCII_Render_Engine.Input.Keyboard;

namespace Example_ASCII_Game_Engine
{
    public static class Program
    {
        public static Screen screen = new(200, 100);

        public static void Main(string[] args)
        {
            // screen configuration
            screen.Config.FPSCap = 30;
            screen.Config.ScaleToWindow = false;
            screen.Config.Dithering = true;
            screen.Background = new FullScreenShaderObject(new SpiralShader(), 0.25);

            // game objects
            Ball ball = new(new Vec2(100, 50), 5);
            Rectangle BarLeft = new(new Vec2(8, 10), new Vec2(4, 20), new Vec2(1, 1));
            Rectangle BarRight = new(new Vec2(188, 10), new Vec2(4, 20), new Vec2(1, 1));

            // physics config
            double ballVelocity = 50; // pixels per second
            Vec2 ballDirection = new Vec2(5, 3).NormalizeInPlace();
            double barVelocity = 50; // pixels per second
            Vec2 ballTempNextPos = new Vec2();

            // game counters
            int frames = 0;
            double runTime = 0;
            double deltaTime = 0;
            DateTime startTime = DateTime.Now;
            DateTime frameStart = DateTime.Now;
            DateTime prevFrameTime = DateTime.Now;
            DateTime lastFPSTime = DateTime.Now;

            // key states
            bool isWPressed = false;
            bool isSPressed = false;
            bool isUpArrowPressed = false;
            bool isDownArrowPressed = false;
            KeyboardInput keyboardInput = new KeyboardInput();

            // game loop
            while (true)
            {
                prevFrameTime = frameStart;
                frameStart = DateTime.Now;
                runTime = (frameStart - startTime).TotalSeconds;
                deltaTime = (frameStart - prevFrameTime).TotalSeconds;
                frames++;

                // update FPS in console title every second
                if ((frameStart - lastFPSTime).TotalSeconds >= 0.5)
                {
                    double fps = 1.0 / deltaTime;
                    Console.Title = $"FPS: {fps:F2}";
                    lastFPSTime = frameStart;
                }

                isWPressed = keyboardInput.IsKeyPressed(Keys.W);
                isSPressed = keyboardInput.IsKeyPressed(Keys.S);
                isUpArrowPressed = keyboardInput.IsKeyPressed(Keys.UpArrow);
                isDownArrowPressed = keyboardInput.IsKeyPressed(Keys.DownArrow);

                // move bars based on key states
                if (isWPressed)
                {
                    BarLeft.Pos.y -= barVelocity * deltaTime;
                }
                if (isSPressed)
                {
                    BarLeft.Pos.y += barVelocity * deltaTime;
                }
                if (isUpArrowPressed)
                {
                    BarRight.Pos.y -= barVelocity * deltaTime;
                }
                if (isDownArrowPressed)
                {
                    BarRight.Pos.y += barVelocity * deltaTime;
                }

                // calculate next ball pos
                ballTempNextPos.SetInPlace(
                    ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime),
                    ball.Pos.y + (ballDirection.y * ballVelocity * deltaTime)
                    );

                // bar Left collision
                if (ballTempNextPos.x < BarLeft.Pos.x + BarLeft.Size.x + ball.Radius &&
                    ballTempNextPos.y > BarLeft.Pos.y &&
                    ballTempNextPos.y < BarLeft.Pos.y + BarLeft.Size.y &&
                    ballDirection.x < 0)
                {
                    ballDirection.x = 0 - ballDirection.x;
                    ballTempNextPos.x = ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime);
                }

                // bar right collision
                if (ballTempNextPos.x > BarRight.Pos.x - ball.Radius &&
                    ballTempNextPos.y > BarRight.Pos.y &&
                    ballTempNextPos.y < BarRight.Pos.y + BarRight.Size.y &&
                    ballDirection.x > 0)
                {
                    ballDirection.x = 0 - ballDirection.x;
                    ballTempNextPos.x = ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime);
                }

                // ball ceiling/floor collision
                if (ballTempNextPos.y + ball.Radius > screen.Height || ballTempNextPos.y - ball.Radius < 0)
                {
                    ballDirection.y = 0 - ballDirection.y;
                    ballTempNextPos.y = ball.Pos.y + (ballDirection.y * ballVelocity * deltaTime);
                }

                // ball wall collision
                if (ballTempNextPos.x + ball.Radius > screen.Width || ballTempNextPos.x - ball.Radius < 0)
                {
                    ballDirection.x = 0 - ballDirection.x;
                    ballTempNextPos.x = ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime);

                    // reset
                    ball.Pos.SetInPlace(100, 50);

                    ballDirection.SetInPlace(
                        Math.Sin(runTime * 500) <= 0 ? 5 : -5,
                        Math.Sin(runTime * 200) * 3).NormalizeInPlace();
                    ballTempNextPos.SetInPlace(
                        ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime),
                        ball.Pos.y + (ballDirection.y * ballVelocity * deltaTime)
                        );
                }

                // apply new pos
                ball.Pos.SetInPlace(ballTempNextPos);

                // drawing
                screen.Clear();
                screen.Draw(ball.ToRenderable());
                screen.Draw(BarLeft);
                screen.Draw(BarRight);

                screen.Render();

                // frame limiter
                double frameTime = (DateTime.Now - frameStart).TotalMilliseconds;
                double targetFrameTime = 1000.0 / screen.Config.FPSCap;
                if (frameTime < targetFrameTime)
                {
                    int sleepTime = (int)(targetFrameTime - frameTime);
                    System.Threading.Thread.Sleep(sleepTime);
                }

                // reset keys
                // key states
                isWPressed = false;
                isSPressed = false;
                isUpArrowPressed = false;
                isDownArrowPressed = false;
            }
        }
    }
}