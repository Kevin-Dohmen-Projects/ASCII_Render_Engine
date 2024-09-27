using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_ASCII_Render_Engine.ScreenRelated;

namespace CSharp_ASCII_Render_Engine
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Screen screen = new Screen(50, 50);
            
            screen.Render();
        }
    }
}
