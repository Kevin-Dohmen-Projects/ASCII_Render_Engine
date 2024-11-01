using CSharp_ASCII_Render_Engine.Geometry.Lines;
using CSharp_ASCII_Render_Engine.Geometry.Primitives;
using CSharp_ASCII_Render_Engine.ScreenRelated;
using CSharp_ASCII_Render_Engine.Shader;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using CSharp_ASCII_Render_Engine.Utils;

namespace CSharp_ASCII_Render_Engine
{
    public static class Program
    {
        public static Screen screen = new Screen(100, 100);

        public static void Main(string[] args)
        {
            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new Vec2(1, 1), false);

            Rectangle shaderRect = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new SpiralShader());

            Rectangle rect1 = new Rectangle(new Vec2(60, 10), new Vec2(20, 30), new Vec2(1, 1));
            Rectangle rect2 = new Rectangle(new Vec2(70, 20), new Vec2(20, 30), new Vec2(.8, .6));
            Rectangle rect2Frame = new Rectangle(new Vec2(70, 20), new Vec2(20, 30), new Vec2(1, 1), false);

            Line2D line1 = new Line2D(new Vec2(5, 30), new Vec2(95, 70), new Vec2(1, 1));

            int frameCount = 0;
            while (true)
            {
                DateTime sTime = DateTime.Now;
                frameCount = screen.Frame;
                
                // animation


                // drawing objects
                screen.Clear();

                screen.Draw(shaderRect);
                screen.Draw(line1);
                screen.Draw(rect1);
                screen.Draw(rect2);
                screen.Draw(rect2Frame);

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
