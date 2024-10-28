using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_ASCII_Render_Engine.ScreenRelated;
using CSharp_ASCII_Render_Engine.Geometry.Primitives;
using CSharp_ASCII_Render_Engine.Types.Vectors;
using System.Data.SqlTypes;
using CSharp_ASCII_Render_Engine.Geometry.Lines;
using System.Xml.Serialization;
using CSharp_ASCII_Render_Engine.Shader;

namespace CSharp_ASCII_Render_Engine
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            Screen screen = new Screen(100, 100);

            //Vec2 A = new Vec2(50, 50);
            //Vec2 B = new Vec2(50, 0);
            //Line2D line1 = new Line2D(A, B, new Vec2(1, 1));

            //Rectangle rect1 = new Rectangle(A, B, new Vec2(.5, 1), false);

            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(100, 100), new Vec2(1, 1), false);

            Rectangle shaderRect = new Rectangle(new Vec2(0), new Vec2(100, 100), new SinShader());

            int frameCount = 0;
            while (true)
            {
                DateTime sTime = DateTime.Now;

                screen.Clear();
                //screen.Draw(rect1);
                //screen.Draw(rect2);


                //B = A + new Vec2(Math.Sin((double)frameCount / 30)*40, Math.Cos((double)frameCount / 30)*40);

                ////A = new Vec2(0);
                ////B = new Vec2(50);
                //line1.B = B;

                //double ax = A.x;
                //double ay = A.y;
                //double bx = B.x;
                //double by = B.y;
                //double xmin = Math.Min(ax, bx);
                //double xmax = Math.Max(ax, bx);
                //double ymin = Math.Min(ay, by);
                //double ymax = Math.Max(ay, by);

                //rect1.Pos = new Vec2(xmin, ymin);
                //rect1.Size = new Vec2(xmax - xmin, ymax - ymin);

                screen.Draw(shaderRect);
                //screen.Draw(rect1);
                //screen.Draw(line1);

                screen.Draw(frame);

                screen.Render();
                DateTime eTime = DateTime.Now;

                Console.WriteLine(1/(eTime-sTime).TotalSeconds);
                Thread.Sleep(200);
                frameCount++;
            }
        }
    }
}
