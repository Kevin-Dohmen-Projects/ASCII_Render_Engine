using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Lines;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.Input.Keyboard;
using System.Drawing;
using System.Runtime.Versioning;
using WindowPhysics.Objects;

namespace WindowPhysics;

public class Program
{
    public static void Main(string[] args)
    {
        // screen config
        Screen screen = new Screen(200, 100);
        screen.Background = new FullScreenShaderObject(new SpiralShader(), 0.25);
        screen.Config.ScaleToWindow = true;
        screen.Config.Dithering = true;
        screen.Config.VisualizeAsync = true;

        // input
        KeyboardInput keyboardInput = new KeyboardInput();
        bool keyUpPressed = false;
        bool keyDownPressed = false;
        bool keyLeftPressed = false;
        bool keyRightPressed = false;

        // physics
        double G = 50;
        Vec2 GVec = new Vec2(0, 1).Normalize() * G;
        double wallbouncyness = 0.9;
        double drag = 0.1;

        Vec2 forceVec = new();
        Vec2 effectorStrength = new(50, 100);

        // objects
        Ball ball = new(new Vec2(10, 10), 5);
        Vec2 BallVelVec = new Vec2(100, 0);

        Line2D vecLine = new(new Vec2(), new Vec2(), new Vec2(0.5, 1));

        // vars
        double DeltaTime;

        // counters
        int frame = 0;
        DateTime frameStart = DateTime.Now;
        DateTime lastFPSTime = DateTime.Now;

        while (true)
        {
            frame++;
            frameStart = DateTime.Now;
            // vars
            DeltaTime = screen.FrameTimer.ElapsedTime / 1000;

            // keyboard input:
            keyUpPressed = keyboardInput.IsKeyPressed(Keys.UpArrow);
            keyDownPressed = keyboardInput.IsKeyPressed(Keys.DownArrow);
            keyLeftPressed = keyboardInput.IsKeyPressed(Keys.LeftArrow);
            keyRightPressed = keyboardInput.IsKeyPressed(Keys.RightArrow);

            // calculate effector
            forceVec = new(0, 0);
            forceVec = keyUpPressed ? forceVec + new Vec2(0, -1) : forceVec;
            forceVec = keyDownPressed ? forceVec + new Vec2(0, 1) : forceVec;
            forceVec = keyLeftPressed ? forceVec + new Vec2(-1, 0) : forceVec;
            forceVec = keyRightPressed ? forceVec + new Vec2(1, 0) : forceVec;

            forceVec = forceVec.Normalize();

            // physics
            Vec2 tmpBallPos = ball.Pos;
            Vec2 tmpBallVelVec = BallVelVec;

            // apply effector
            tmpBallVelVec += (forceVec * DeltaTime * effectorStrength);

            // Apply gravity
            tmpBallVelVec += (GVec * DeltaTime);

            // Apply drag
            double speed = tmpBallVelVec.Length()/100; // 100 pix is one unit
            Vec2 dragForce = tmpBallVelVec.Normalize() * (-drag * speed * speed);
            tmpBallVelVec += dragForce * DeltaTime;

            // Update position
            tmpBallPos += tmpBallVelVec * DeltaTime;

            // collision
            bool recalc = false;
            if (tmpBallPos.y + ball.Radius >= screen.Height)
            {
                double overlap = (tmpBallPos.y + ball.Radius) - screen.Height;
                tmpBallPos.y = screen.Height - ball.Radius - overlap;
                tmpBallVelVec.y *= -wallbouncyness;
                recalc = true;
            }

            if (tmpBallPos.y - ball.Radius <= 0)
            {
                double overlap = ball.Radius - tmpBallPos.y;
                tmpBallPos.y = ball.Radius + overlap;
                tmpBallVelVec.y *= -wallbouncyness;
                recalc = true;
            }

            if (tmpBallPos.x + ball.Radius >= screen.Width)
            {
                double overlap = (tmpBallPos.x + ball.Radius) - screen.Width;
                tmpBallPos.x = screen.Width - ball.Radius - overlap;
                tmpBallVelVec.x *= -wallbouncyness;
                recalc = true;
            }

            if (tmpBallPos.x - ball.Radius <= 0)
            {
                double overlap = ball.Radius - tmpBallPos.x;
                tmpBallPos.x = ball.Radius + overlap;
                tmpBallVelVec.x *= -wallbouncyness;
                recalc = true;
            }

            if (recalc)
            {
                //tmpBallPos += tmpBallVelVec * DeltaTime;
            }

            BallVelVec = tmpBallVelVec;
            ball.Pos = tmpBallPos;

            // calc pos of VecLine
            vecLine.A = ball.Pos;
            vecLine.B = ball.Pos + BallVelVec / 5;

            // update FPS in console title every second
            if ((frameStart - lastFPSTime).TotalSeconds >= 0.5)
            {
                double fps = 1 / (screen.FrameTimer.ElapsedTime / 1000);
                Console.Title = $"RT:{screen.RenderTimer.ElapsedTime:F1}ms|VT:{screen.RenderTimer.ElapsedTime:F1}ms|FPS:{fps:F0}";
                lastFPSTime = frameStart;
            }

            // rendering
            screen.Clear();
            screen.Draw(ball.ToRenderable());
            screen.Draw(vecLine);
            screen.Render();


        }
    }
}