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
using CSharp_ASCII_Render_Engine.Utils;

namespace CSharp_ASCII_Render_Engine
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Screen screen = new Screen(100, 100);

            Rectangle frame = new Rectangle(new Vec2(0), new Vec2(100, 100), new Vec2(1, 1), false);

            Rectangle shaderRect = new Rectangle(new Vec2(0), new Vec2(100, 100), new SinShader());

            int frameCount = 0;
            while (true)
            {
                DateTime sTime = DateTime.Now;

                screen.Clear();

                screen.Draw(shaderRect);

                // sum stuff
                foreach (ShapeData shape in ShapeStore.GetShapes())
                {
                    screen.Draw(shape.shape);
                }

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
