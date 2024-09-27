using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public class ASCIIConverter
    {
        char[] chars = [' ', '.', ':', ';', '+', '=', 'x', 'X', '$'];

        public string BufferToFullScreen(ScreenBuffer screenBuffer)
        {
            string FullScreen = "";

            int width = screenBuffer.Width;
            int height = screenBuffer.Height;

            for (int y = 0; y < height; y++)
            {

                List<char> row = new();
                for (int x = 0; x < width; x++)
                {
                    char tmpChar = CharFromColor(screenBuffer.Buffer[y][x]);
                    row.Add(tmpChar);
                    row.Add(tmpChar);
                }
                FullScreen += "\n" + string.Join("", row);
            }
            return FullScreen;
        }

        public char CharFromColor(Vec2 color)
        {
            double lum = Math.Clamp(color.x * color.y, 0, 1);
            return chars[(int)Math.Round(lum * 8)];
        }
    }
}
