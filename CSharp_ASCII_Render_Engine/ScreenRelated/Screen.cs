using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public class Screen
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public RenderQueue Queue { get; private set; }
        public ScreenBuffer Buffer { get; private set; }
        private ASCIIConverter Converter = new();

        public Screen (int width, int height)
        {
            Width = width;
            Height = height;
            Queue = new RenderQueue();
            Buffer = new ScreenBuffer(width, height);
        }

        public void Draw(IRenderable item)
        {
            Queue.Add(item);
        }

        public void Render()
        {
            foreach (var item in Queue.Queue)
            {
                item.Render(Buffer);
            }

            string fullScreen = Converter.BufferToFullScreen(Buffer);
            Console.WriteLine(fullScreen);
        }
    }
}
