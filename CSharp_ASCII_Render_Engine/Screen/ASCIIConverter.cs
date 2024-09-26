using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Screen
{
    public class ASCIIConverter
    {
        char[] chars = [' ', '.', ':', ';', '+', '=', 'x', 'X', '$'];
        List<List<char>> charBuffer;

        public string FullScreenFromBuffer(ScreenBuffer screenBuffer)
        {
            string FullScreen = "";
            charBuffer = new List<List<char>>();

            return FullScreen;
        }
    }
}
