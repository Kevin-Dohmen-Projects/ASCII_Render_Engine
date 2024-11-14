using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public class Display
    {
        private char[,] screenBuffer; // Assume this holds all screen chars
        private int width;
        private int height;
        private readonly StringBuilder sb; // Reusable StringBuilder instance

        public string fullScreenString;

        public Display(int width, int height)
        {
            this.width = width;
            this.height = height;
            screenBuffer = new char[height, width];
            sb = new StringBuilder(width * height);
        }

        public void SetChar(int x, int y, char c)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                screenBuffer[y, x] = c;
            }
        }

        public override string ToString()
        {
            sb.Clear(); // Clear the StringBuilder for reuse

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char currentChar = screenBuffer[y, x];
                    sb.Append(currentChar); // Add character twice
                    sb.Append(currentChar);
                }
                if (y < height - 1)
                {
                    sb.AppendLine(); // Add newline after each row
                }
            }

            fullScreenString = sb.ToString();
            return fullScreenString;
        }
    }
}
