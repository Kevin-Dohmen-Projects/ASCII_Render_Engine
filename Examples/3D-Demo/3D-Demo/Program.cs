using System;
using ASCII_Render_Engine.Objects.Geometry.Lines;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Core;

using ASCII_Render_Engine.Input.Keyboard;
using System.Numerics;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Mesh;

namespace _3D_Demo;

public static class Program
{
    public static Screen screen = new(200, 100);

    public static void Main(string[] args)
    {
        // screen configuration
        screen.Config.FPSCap = 0;
        screen.Config.ScaleToWindow = true;
        screen.Config.Dithering = true;
        screen.Config.CenterScreen = true;
        //screen.Background = new FullScreenShaderObject(new SpiralShader(), 0.25);

        // camera
        ICamera camera = new PerspectiveCamera3D(new Vec3(0, 0, -20), new Vec3(0, 0, 1), 90, 1, 1000);
        CameraConfig cameraConfig = new(camera);

        // objects
        Cube RightArm = new(new Vec3(7.5, 30, 0), new Vec3(5, 20, 5), cameraConfig);
        RightArm.Origin = new Vec3(-2.5, 7.5, 0);

        Cube LeftArm = new(new Vec3(-7.5, 30, 0), new Vec3(5, 20, 5), cameraConfig);
        LeftArm.Origin = new Vec3(2.5, 7.5, 0);

        Cube Body = new(new Vec3(0, 30, 0), new Vec3(10, 20, 5), cameraConfig);

        Cube Head = new(new Vec3(0, 45, 0), new Vec3(10, 10, 10), cameraConfig);

        Cube RightLeg = new(new Vec3(2.5, 10, 0), new Vec3(5, 20, 5), cameraConfig);
        RightLeg.Origin = new Vec3(-2.5, 10, 0);

        Cube LeftLeg = new(new Vec3(-2.5, 10, 0), new Vec3(5, 20, 5), cameraConfig);
        LeftLeg.Origin = new Vec3(2.5, 10, 0);


        // counters
        int frames = 0;
        double runTime = 0;
        double deltaTime = 0;
        DateTime startTime = DateTime.Now;
        DateTime frameStart = DateTime.Now;
        DateTime lastFPSTime = DateTime.Now;
        DateTime roundStartTime = DateTime.Now;
        double roundTime = 0;

        // key states
        bool isUpArrowPressed = false;
        bool isDownArrowPressed = false;
        bool isLeftArrowPressed = false;
        bool isRightArrowPressed = false;
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

            isUpArrowPressed = keyboardInput.IsKeyPressed(Keys.UpArrow);
            isDownArrowPressed = keyboardInput.IsKeyPressed(Keys.DownArrow);
            isLeftArrowPressed = keyboardInput.IsKeyPressed(Keys.LeftArrow);
            isRightArrowPressed = keyboardInput.IsKeyPressed(Keys.RightArrow);

            RightArm.Rotation = new Vec3(Math.Sin(runTime) * 0.15, 0, Math.Sin(runTime * 1.5) * 0.05 + 0.05);
            LeftArm.Rotation = new Vec3(Math.Sin(runTime + 34) * 0.15, 0, Math.Sin(runTime + 34) * 0.05 - 0.05);
            RightLeg.Rotation = new Vec3(Math.Sin(runTime + 10) * 0.15, 0, Math.Sin((runTime + 10) * 1.5) * 0.05 + 0.05);
            LeftLeg.Rotation = new Vec3(Math.Sin(runTime + 44) * 0.15, 0, Math.Sin(runTime + 44) * 0.05 - 0.05);

            camera.Position = new Vec3(Math.Sin(runTime/5)*30, Math.Sin(runTime / 10) * 20 + 30, Math.Cos(runTime/5)*30);
            camera.Direction = (new Vec3(0, 27.5, 0) - camera.Position).Normalize();

            // drawing
            screen.Clear();
            screen.Draw(RightArm);
            screen.Draw(LeftArm);
            screen.Draw(Body);
            screen.Draw(Head);
            screen.Draw(RightLeg);
            screen.Draw(LeftLeg);

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
        }
    }
}