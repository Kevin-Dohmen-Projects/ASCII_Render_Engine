using System.Text;

namespace ASCII_Render_Engine.Core
{
    public class Display
    {
        private char[,] screenBuffer; // Assume this holds all screen chars
        private int width;
        private int height;
        private readonly StringBuilder sb; // Reusable StringBuilder instance

        public string fullScreenString;

        bool UseOffset = false;
        private int offsetX;
        private int offsetY;

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
                if (UseOffset)
                {
                    sb.Append($"\x1b[{offsetY + y};{offsetX}H");
                }
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

        public void SetOffset(int x, int y)
        {
            offsetX = x;
            offsetY = y;
            UseOffset = true;
        }
    }
}
