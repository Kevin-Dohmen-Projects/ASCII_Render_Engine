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
        public static Screen screen = new(150, 150);

        private static DateTime sTime = new();
        private static DateTime eTime = new();

        public static void Main(string[] args)
        {
            screen.Background = new FullScreenShaderObject(new SinShader());

            // screen config
            screen.Config.Dithering = true;
            screen.Config.FPSCap = 30;
            screen.Config.ScaleToWindow = false;
            

            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(screen.Width, screen.Height), new Vec2(1, 1), false);

            int frameCount = 0;
            while (true)
            {
                sTime = DateTime.Now;
                frameCount = screen.Frame;

                if (screen.Config.ScaleToWindow)
                {
                    screen.ScaleToWindow();
                    frame.Size.SetInPlace(screen.Width, screen.Height);
                }

                screen.Clear();

                // api-injected objects
                foreach (ShapeData shape in ShapeStore.GetShapes())
                {
                    screen.Draw(shape.shape);
                }

                screen.Draw(frame);

                // render
                screen.Render();

                eTime = DateTime.Now;

                // frame cap
                double elapsedTime = (eTime - sTime).TotalSeconds;

                Console.WriteLine(1 / elapsedTime);

                double sleepTime = (1 / screen.Config.FPSCap) - elapsedTime;
                if (sleepTime > 0)
                {
                    Thread.Sleep((int)(sleepTime * 1000));
                }
            }
        }
    }
}
