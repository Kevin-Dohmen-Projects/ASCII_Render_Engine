using CSharp_ASCII_Render_Engine.Geometry.Lines;
using CSharp_ASCII_Render_Engine.Geometry.Primitives;
using CSharp_ASCII_Render_Engine.ScreenRelated;
using CSharp_ASCII_Render_Engine.Shader;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using CSharp_ASCII_Render_Engine.Utils;
using CSharp_ASCII_Render_Engine.Demo.Animation;

namespace CSharp_ASCII_Render_Engine
{
    public static class Program
    {
        public static Screen screen = new Screen(150, 150);

        public static void Main(string[] args)
        {
            screen.Background = new FullScreenShaderObject(new SinShader());

            // screen config
            screen.Config.Dithering = true;

            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new Vec2(1, 1), false);

            //Rectangle shaderRect = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new StaticNoiseShader());

            Rectangle rect1 = new Rectangle(new Vec2(60, 10), new Vec2(50, 50), new SpiralShader(), 1);
            Rectangle rect2 = new Rectangle(new Vec2(80, 20), new Vec2(40, 30), new Vec2(.8, .6));
            Rectangle rect2Frame = new Rectangle(new Vec2(80, 20), new Vec2(40, 30), new Vec2(1, 1), false);

            Line2D line1 = new Line2D(new Vec2(5, 30), new Vec2(145, 120), new Vec2(1, 1));
            Line2D line2 = new Line2D(new Vec2(5, 120), new Vec2(145, 30), new Vec2(0, 1));

            Rectangle rect3 = new Rectangle(new Vec2(0, 0), new Vec2(screen.Width/3, screen.Height), new Vec2(1, 0.5));
            Rectangle rect4 = new Rectangle(new Vec2(screen.Width/3*2, 0), new Vec2(screen.Width/3, screen.Height), new Vec2(0, 0.5));

            TestAnimation animation = new();

            int frameCount = 0;
            while (true)
            {
                DateTime sTime = DateTime.Now;
                frameCount = screen.Frame;
                
                // animation
                Vec2 animPos = animation.Animate(((double)frameCount) / 10);

                rect1.Pos = animPos * new Vec2(screen.Width, screen.Height)-(rect1.Size/2);

                // drawing objects
                screen.Clear();

                //screen.Draw(shaderRect);
                screen.Draw(line1);
                screen.Draw(rect1);
                screen.Draw(rect2);
                screen.Draw(rect2Frame);
                screen.Draw(line2);
                screen.Draw(rect3);
                screen.Draw(rect4);

                // api-injected objects
                foreach (ShapeData shape in ShapeStore.GetShapes())
                {
                    screen.Draw(shape.shape);
                }

                screen.Draw(frame);

                // render
                screen.Render();
                DateTime eTime = DateTime.Now;

                Console.WriteLine(1 / (eTime - sTime).TotalSeconds);
                Thread.Sleep(200);
            }
        }
    }
}
