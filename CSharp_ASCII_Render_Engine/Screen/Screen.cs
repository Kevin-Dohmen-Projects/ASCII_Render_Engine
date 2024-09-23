using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Screen
{
    public class Screen
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ScreenBuffer Buffer { get; private set; }
    }
}
