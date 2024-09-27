using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_ASCII_Render_Engine.ScreenRelated;
using CSharp_ASCII_Render_Engine.Geometry.Primitives;
using CSharp_ASCII_Render_Engine.Types.Vectors;

namespace CSharp_ASCII_Render_Engine
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Screen screen = new Screen(50, 50);

            Rectangle rect1 = new Rectangle(new Vec2(5, 10), new Vec2(10, 5), new Vec2(1, 1));

            screen.Draw(rect1);
            
            screen.Render();
        }
    }
}
