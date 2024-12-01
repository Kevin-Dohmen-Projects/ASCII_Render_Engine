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
using ASCII_Render_Engine.Objects.Geometry.Mesh;

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

            ICamera camera = new PerspectiveCamera3D(new Vec3(0, 0, -15), new Vec3(0, 0, 1), 75, 1, 1000);
            //ICamera camera = new OrthographicCamera3D(new Vec3(0, 0, -15), new Vec3(0, 0, 1), 10);

            CameraConfig cameraConfig = new CameraConfig(camera);


            // -=-=-=- Cube1 Start -=-=-=-
            Vertex3D c1v1 = new(new Vec3(-5, -5, -5), new Vec2());
            Vertex3D c1v2 = new(new Vec3(5, -5, -5), new Vec2());
            Vertex3D c1v3 = new(new Vec3(5, 5, -5), new Vec2());
            Vertex3D c1v4 = new(new Vec3(-5, 5, -5), new Vec2());
            Vertex3D c1v5 = new(new Vec3(-5, -5, 5), new Vec2());
            Vertex3D c1v6 = new(new Vec3(5, -5, 5), new Vec2());
            Vertex3D c1v7 = new(new Vec3(5, 5, 5), new Vec2());
            Vertex3D c1v8 = new(new Vec3(-5, 5, 5), new Vec2());

            Poly3D c1face1 = new([c1v1, c1v2, c1v3, c1v4]); // Front face
            Poly3D c1face2 = new([c1v5, c1v6, c1v7, c1v8]); // Back face
            Poly3D c1face3 = new([c1v1, c1v2, c1v6, c1v5]); // Bottom face
            Poly3D c1face4 = new([c1v3, c1v4, c1v8, c1v7]); // Top face
            Poly3D c1face5 = new([c1v1, c1v4, c1v8, c1v5]); // Left face
            Poly3D c1face6 = new([c1v2, c1v3, c1v7, c1v6]); // Right face

            Mesh3D Cube1 = new([c1face1, c1face2, c1face3, c1face4, c1face5, c1face6], cameraConfig);
            // -=-=-=- Cube1 End -=-=-=-


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
                    Console.Title = $"RT:{screen.RenderTimer.ElapsedTime:F1}ms|VT:{screen.VisualizeTimer.ElapsedTime:F1}ms|FPS:{fps:F0}";
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

                Cube1.Angle = new Vec3(runTime/5, runTime, runTime/2);

                // drawing
                screen.Clear();
                screen.Draw(ball.ToRenderable());
                screen.Draw(BarLeft);
                screen.Draw(BarRight);

                screen.Draw(Cube1);

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