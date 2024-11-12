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

            int frameCount = 0;
            while (true)
            {
                DateTime sTime = DateTime.Now;
                frameCount = screen.Frame;

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
                Thread.Sleep(50);
            }
        }
    }
}
