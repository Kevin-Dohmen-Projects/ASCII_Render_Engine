using System;
using ASCII_Render_Engine.Objects.Geometry.Lines;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using Example_ASCII_Game_Engine.GameObjects;
using ASCII_Render_Engine.Core;

using ASCII_Render_Engine.Input.Keyboard;
using System.Numerics;
using ASCII_Render_Engine.Objects.Camera;

namespace Example_ASCII_Game_Engine
{
    public static class Program
    {
        public static Screen screen = new(200, 100);

        public static void Main(string[] args)
        {
            // screen configuration
            screen.Config.FPSCap = 0;
            screen.Config.ScaleToWindow = false;
            screen.Config.Dithering = true;
            screen.Config.CenterScreen = true;
            screen.Background = new FullScreenShaderObject(new SpiralShader(), 0.25);

            Console.CursorVisible = false;

            // game objects
            Ball ball = new(new Vec2(100, 50), 5);
            Rectangle BarLeft = new(new Vec2(8, 10), new Vec2(4, 20), new Vec2(1, 1));
            Rectangle BarRight = new(new Vec2(188, 10), new Vec2(4, 20), new Vec2(1, 1));

            ICamera camera = new PerspectiveCamera3D(new Vec3(), new Vec3(0, 0, 1), 100, 1, 1000);

            CameraConfig cameraConfig = new CameraConfig(camera);

            Vertex3D v1 = new Vertex3D(new Vec3(-5, -5, 5), new Vec2(), cameraConfig);
            Vertex3D v2 = new Vertex3D(new Vec3(5, -5, 5), new Vec2(), cameraConfig);
            Vertex3D v3 = new Vertex3D(new Vec3(5, 5, 5), new Vec2(), cameraConfig);
            Vertex3D v4 = new Vertex3D(new Vec3(-5, 5, 5), new Vec2(), cameraConfig);
            Vertex3D v5 = new Vertex3D(new Vec3(-5, -5, 15), new Vec2(), cameraConfig);
            Vertex3D v6 = new Vertex3D(new Vec3(5, -5, 15), new Vec2(), cameraConfig);
            Vertex3D v7 = new Vertex3D(new Vec3(5, 5, 15), new Vec2(), cameraConfig);
            Vertex3D v8 = new Vertex3D(new Vec3(-5, 5, 15), new Vec2(), cameraConfig);

            Poly3D face1 = new Poly3D(new Vertex3D[] { v1, v2, v3, v4 }); // Front face
            Poly3D face2 = new Poly3D(new Vertex3D[] { v5, v6, v7, v8 }); // Back face
            Poly3D face3 = new Poly3D(new Vertex3D[] { v1, v2, v6, v5 }); // Bottom face
            Poly3D face4 = new Poly3D(new Vertex3D[] { v3, v4, v8, v7 }); // Top face
            Poly3D face5 = new Poly3D(new Vertex3D[] { v1, v4, v8, v5 }); // Left face
            Poly3D face6 = new Poly3D(new Vertex3D[] { v2, v3, v7, v6 }); // Right face


            // physics config
            double ballBaseVelocity = 50; // pixels per second
            double ballVelocityIncrease = 5; // pixels per second ^ 2
            double ballVelocity = ballBaseVelocity;
            Vec2 ballDirection = new Vec2(5, 3).Normalize();
            double barVelocity = 100; // pixels per second
            Vec2 ballTempNextPos = new Vec2();

            // game counters
            int frames = 0;
            double runTime = 0;
            double deltaTime = 0;
            DateTime startTime = DateTime.Now;
            DateTime frameStart = DateTime.Now;
            DateTime lastFPSTime = DateTime.Now;
            DateTime roundStartTime = DateTime.Now;
            double roundTime = 0;

            // key states
            bool isWPressed = false;
            bool isSPressed = false;
            bool isUpArrowPressed = false;
            bool isDownArrowPressed = false;
            KeyboardInput keyboardInput = new KeyboardInput();

            // game loop
            while (true)
            {
                frameStart = DateTime.Now;
                runTime = (frameStart - startTime).TotalSeconds;
                //deltaTime = (frameStart - prevFrameTime).TotalSeconds;
                deltaTime = screen.FrameTimer.ElapsedTime / 1000;
                roundTime = (frameStart - roundStartTime).TotalSeconds;
                frames++;

                // update FPS in console title every second
                if ((frameStart - lastFPSTime).TotalSeconds >= 0.5)
                {
                    double fps = 1 / (screen.FrameTimer.ElapsedTime / 1000);
                    Console.Title = $"RT:{screen.RenderTimer.ElapsedTime:F1}ms|VT:{screen.RenderTimer.ElapsedTime:F1}ms|FPS:{fps:F0}";
                    lastFPSTime = frameStart;
                }

                isWPressed = keyboardInput.IsKeyPressed(Keys.W);
                isSPressed = keyboardInput.IsKeyPressed(Keys.S);
                isUpArrowPressed = keyboardInput.IsKeyPressed(Keys.UpArrow);
                isDownArrowPressed = keyboardInput.IsKeyPressed(Keys.DownArrow);

                // increase ball speed
                ballVelocity = ballBaseVelocity + (ballVelocityIncrease * roundTime);

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
                ballTempNextPos = new Vec2(
                    ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime),
                    ball.Pos.y + (ballDirection.y * ballVelocity * deltaTime)
                    );

                // calculate next ball pos
                ballTempNextPos = new Vec2(
                    ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime),
                    ball.Pos.y + (ballDirection.y * ballVelocity * deltaTime)
                    );

                // bar Left collision
                if (ballTempNextPos.x < BarLeft.Pos.x + BarLeft.Size.x + ball.Radius &&
                    ballTempNextPos.y > BarLeft.Pos.y &&
                    ballTempNextPos.y < BarLeft.Pos.y + BarLeft.Size.y &&
                    ballDirection.x < 0)
                {
                    ballDirection.x = -ballDirection.x;
                    ballTempNextPos.x = BarLeft.Pos.x + BarLeft.Size.x + ball.Radius;
                }

                // bar right collision
                if (ballTempNextPos.x > BarRight.Pos.x - ball.Radius &&
                    ballTempNextPos.y > BarRight.Pos.y &&
                    ballTempNextPos.y < BarRight.Pos.y + BarRight.Size.y &&
                    ballDirection.x > 0)
                {
                    ballDirection.x = -ballDirection.x;
                    ballTempNextPos.x = BarRight.Pos.x - ball.Radius;
                }

                // ball ceiling/floor collision
                if (ballTempNextPos.y + ball.Radius > screen.Height)
                {
                    ballDirection.y = -ballDirection.y;
                    ballTempNextPos.y = screen.Height - ball.Radius;
                }
                else if (ballTempNextPos.y - ball.Radius < 0)
                {
                    ballDirection.y = -ballDirection.y;
                    ballTempNextPos.y = ball.Radius;
                }

                // ball wall collision
                if (ballTempNextPos.x + ball.Radius > screen.Width || ballTempNextPos.x - ball.Radius < 0)
                {
                    ballDirection.x = 0 - ballDirection.x;
                    ballTempNextPos.x = ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime);

                    // reset
                    ball.Pos = new Vec2(100, 50);

                    ballDirection = new Vec2(
                        Math.Sin(runTime * 500) <= 0 ? 5 : -5,
                        Math.Sin(runTime * 200) * 3).Normalize();
                    ballTempNextPos = new Vec2(
                        ball.Pos.x + (ballDirection.x * ballVelocity * deltaTime),
                        ball.Pos.y + (ballDirection.y * ballVelocity * deltaTime)
                        );

                    roundStartTime = DateTime.Now;
                }

                // apply new pos
                ball.Pos = ballTempNextPos;

                //camera.Position = new Vec3(Math.Sin(runTime) * 10, 0, Math.Cos(runTime) * 10);
                //// point to the center
                //camera.Direction = (new Vec3(0, 0, 0) - camera.Position).Normalize();

                // drawing
                screen.Clear();
                screen.Draw(ball.ToRenderable());
                screen.Draw(BarLeft);
                screen.Draw(BarRight);

                screen.Draw(face1);
                screen.Draw(face2);
                screen.Draw(face3);
                screen.Draw(face4);
                screen.Draw(face5);
                screen.Draw(face6);

                screen.Render();

                // frame limiter
                if (screen.Config.FPSCap != 0)
                {
                    double frameTime = (DateTime.Now - frameStart).TotalMilliseconds;
                    double targetFrameTime = 1000.0 / screen.Config.FPSCap;
                    if (frameTime < targetFrameTime)
                    {
                        int sleepTime = (int)(targetFrameTime - frameTime);
                        System.Threading.Thread.Sleep(sleepTime);
                    }
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