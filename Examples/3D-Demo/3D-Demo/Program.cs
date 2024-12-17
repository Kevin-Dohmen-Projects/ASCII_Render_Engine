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
//using ASCII_Render_Engine.Objects.Geometry.Mesh;
using ASCII_Render_Engine.MathUtils.Noise;
using ASCII_Render_Engine.Utils.Profiling;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Objects.Geometry.Mesh;
using ASCII_Render_Engine.MathUtils.Transform.Rotation;

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
        PerspectiveCamera3D camera = new PerspectiveCamera3D(new Vec3(0, 0, -20), new QuaternionRotation(new Vec3(0, 0, 1), 0), 90, 1, 1000);
        //SecondPerspectiveCamera camera = new SecondPerspectiveCamera(new Vec3(0, 30, -30), new DirecitonVectorRotation(new Vec3(0, 0, 1)), 90, 1, 1000);
        CameraConfig cameraConfig = new(camera);

        //objects
        Cube RightArm = new(new Vec3(7.5, 30, 0), new Vec3(5, 20, 5), cameraConfig);
        RightArm.Origin = new Vec3(-2.5, 7.5, 0);

        Cube LeftArm = new(new Vec3(-7.5, 30, 0), new Vec3(5, 20, 5), cameraConfig);
        LeftArm.Origin = new Vec3(2.5, 7.5, 0);

        Cube Body = new(new Vec3(0, 30, 0), new Vec3(10, 20, 5), cameraConfig);

        Cube Head = new(new Vec3(0, 45, 0), new Vec3(10, 10, 10), cameraConfig);
        Head.Origin = new Vec3(0, -10, 0);

        Cube RightLeg = new(new Vec3(2.5, 10, 0), new Vec3(5, 20, 5), cameraConfig);
        RightLeg.Origin = new Vec3(-2.5, 10, 0);

        Cube LeftLeg = new(new Vec3(-2.5, 10, 0), new Vec3(5, 20, 5), cameraConfig);
        LeftLeg.Origin = new Vec3(2.5, 10, 0);

        Vertex3D CameraTarget = new(new Vec3(), cameraConfig);

        Mesh3D Floor = new Mesh3D(
        [
            new NGon3D(
            [
                new Vertex3D(new Vec3(50, 0, 0)),
                new Vertex3D(new Vec3(25, 0, 43.3)),
                new Vertex3D(new Vec3(-25, 0, 43.3)),
                new Vertex3D(new Vec3(-50, 0, 0)),
                new Vertex3D(new Vec3(-25, 0, -43.3)),
                new Vertex3D(new Vec3(25, 0, -43.3))
            ])
        ], cameraConfig);

        Cube testCube = new Cube(new Vec3(), new Vec3(5), cameraConfig);

        // counters
        int frames = 0;
        double runTime = 0;
        double deltaTime = 0;
        DateTime startTime = DateTime.Now;
        DateTime frameStart = DateTime.Now;
        DateTime lastFPSTime = DateTime.Now;
        DateTime roundStartTime = DateTime.Now;
        TimeProfiler logicProfiler = new();
        double roundTime = 0;

        // key states
        bool isUpArrowPressed = false;
        bool isDownArrowPressed = false;
        bool isLeftArrowPressed = false;
        bool isRightArrowPressed = false;
        KeyboardInput keyboardInput = new KeyboardInput();

        double lrRot = 0;
        double udRot = 0;

        // game loop
        while (true)
        {
            frameStart = DateTime.Now;
            logicProfiler.Start();
            runTime = (frameStart - startTime).TotalSeconds;
            //deltaTime = (frameStart - prevFrameTime).TotalSeconds;
            deltaTime = screen.FrameTimer.ElapsedTime / 1000;
            roundTime = (frameStart - roundStartTime).TotalSeconds;
            frames++;

            // update FPS in console title every second
            if ((frameStart - lastFPSTime).TotalSeconds >= 0.5)
            {
                double fps = 1 / (screen.FrameTimer.ElapsedTime / 1000);
                Console.Title = $"{logicProfiler.ElapsedTime:F1}ms|{screen.RenderTimer.ElapsedTime:F1}ms|{screen.VisualizeTimer.ElapsedTime:F1}ms|{fps:F0}FPS";
                lastFPSTime = frameStart;
            }

            isUpArrowPressed = keyboardInput.IsKeyPressed(Keys.UpArrow);
            isDownArrowPressed = keyboardInput.IsKeyPressed(Keys.DownArrow);
            isLeftArrowPressed = keyboardInput.IsKeyPressed(Keys.LeftArrow);
            isRightArrowPressed = keyboardInput.IsKeyPressed(Keys.RightArrow);

            if (isUpArrowPressed)
            {
                udRot += 0.5 * deltaTime;
            }
            if (isDownArrowPressed)
            {
                udRot -= 0.5 * deltaTime;
            }
            if (isLeftArrowPressed)
            {
                lrRot += 0.5 * deltaTime;
            }
            if (isRightArrowPressed)
            {
                lrRot -= 0.5 * deltaTime;
            }


            RightArm.Rotation = new EulerRotation(new Vec3(Math.Sin(runTime) * 0.15, 0, Math.Sin(runTime * 1.5) * 0.05 + 0.05));
            LeftArm.Rotation = new EulerRotation(new Vec3(Math.Sin(runTime + 34) * 0.15, 0, Math.Sin(runTime + 34) * 0.05 - 0.05));
            RightLeg.Rotation = new EulerRotation(new Vec3(Math.Sin(runTime + 10) * 0.05, 0, Math.Sin((runTime + 10) * 1.5) * 0.01 + 0.01));
            LeftLeg.Rotation = new EulerRotation(new Vec3(Math.Sin(runTime + 44) * 0.05, 0, Math.Sin(runTime + 44) * 0.01 - 0.01));
            Head.Rotation = new EulerRotation(new Vec3(Math.Sin(runTime / 4) * 0.2, (Math.Sin(runTime / 2) * 0.5 + Math.Sin(runTime) * 0.2) * Math.Sin(runTime / 5), 0));

            CameraTarget.Position = new Vec3(Math.Sin(runTime / 4) * 5, 27.5 + Math.Sin(runTime / 7) * 10, Math.Sin(runTime / 3) * 2);

            camera.Position = new Vec3(Math.Sin(runTime / 5) * 30, Math.Sin(runTime / 10) * 20 + 30, Math.Cos(runTime / 5) * 30);

            QuaternionRotation yRot = new(new Vec3(0, 1, 0), lrRot);
            QuaternionRotation xRot = new(new Vec3(1, 0, 0), udRot); 
            camera.Rotation = yRot * xRot; // note: i dont think this works as its supposed to...

            camera.FieldOfView = 80 + Math.Sin(runTime / 3) * 10;

            logicProfiler.Stop();

            // drawing
            screen.Clear();
            screen.Draw(RightArm);
            screen.Draw(LeftArm);
            screen.Draw(Body);
            screen.Draw(Head);
            screen.Draw(RightLeg);
            screen.Draw(LeftLeg);
            screen.Draw(CameraTarget);
            screen.Draw(Floor);
            screen.Draw(testCube);

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
