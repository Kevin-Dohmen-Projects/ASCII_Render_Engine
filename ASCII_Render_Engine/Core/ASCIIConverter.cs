using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Core
{
    public class ASCIIConverter
    {
        char[] chars = [' ', '.', ':', ';', '+', '=', 'x', 'X', '$'];
        //char[] chars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '&', '$' };

        private readonly double[,] ditherMatrix = new double[,]
        {
            { -0.25, 0.25 },
            { 0.5, -0.5 }
        };

        public Display BufferToFullScreen(ScreenBuffer screenBuffer, Display display, ScreenConfig config)
        {
            int width = screenBuffer.Width;
            int height = screenBuffer.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char tmpChar;
                    if (config.Dithering)
                    {
                        tmpChar = CharFromColorDither(screenBuffer.Buffer[height - y - 1][x], x, y);
                    }
                    else
                    {
                        tmpChar = CharFromColor(screenBuffer.Buffer[y][x]);
                    }
                    display.SetChar(x, y, tmpChar);
                }
            }

            return display;
        }

        public char CharFromColor(Vec2 color)
        {
            double lum = Math.Clamp(color.x * color.y, 0, 1);
            return chars[(int)Math.Round(lum * (chars.Length - 1))];
        }

        public char CharFromColorDither(Vec2 color, int x, int y)
        {
            double lum = Math.Clamp(color.x * color.y, 0, 1);

            // Calculate the step size based on the color depth (number of characters)
            double lumStep = 1.0 / (chars.Length - 1);

            // Scale the dither value by a fraction of lumStep for smooth transitions
            double threshold = ditherMatrix[y % 2, x % 2] * (lumStep / 2.0);

            // Apply the scaled threshold for adaptive dithering
            lum = Math.Clamp(lum + threshold, 0, 1);

            // Map adjusted luminance to the closest character
            return chars[(int)Math.Round(lum * (chars.Length - 1))];
        }
    }
}
