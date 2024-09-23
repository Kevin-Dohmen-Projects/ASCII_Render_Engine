using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Screen
{
    public class ScreenBuffer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public List<List<Vec2>> Buffer;

        public ScreenBuffer(int width, int height)
        {
            Width = width;
            Height = height;
            Buffer = [];
            Clear();
        }

        public void Clear()
        {
            Buffer = new List<List<Vec2>>();
            for (int i = 0; i < Width; i++)
            {
                Buffer.Add(new List<Vec2>());
                for (int j = 0; j < Height; j++)
                {
                    Buffer[i].Add(new Vec2(0));
                }
            }
        }

        public void Insert(int xPos, int yPos, ScreenBuffer buffer)
        {
            int globalYPos = 0;
            int globalXPos = 0;
            Vec2 bg = new(0);
            Vec2 fg = new(0);
            Vec2 r = new(0);
            for (int y = 0; y < buffer.Height; y++)
            {
                globalYPos = y + yPos;

                if (globalYPos >= 0 && globalYPos < Height)
                {
                    for (int x = 0; x < buffer.Width; x++)
                    {
                        globalXPos = x + xPos;

                        if (globalXPos >= 0 && globalXPos < Width)
                        {
                            // b/w alpha transform (x = b/w, y = alpha)
                            bg = Buffer[globalYPos][globalXPos]; // background
                            fg = buffer.Buffer[y][x]; // foreground
                            if (fg.y >= 1)
                            {
                                r = fg;
                            }
                            else if (fg.y <= 0) 
                            {
                                r = bg;
                            }
                            else
                            {
                                Vec2 vec2 = new(0);
                                r = vec2;
                                r.y = 1 - (1 - fg.y) * (1 - bg.y);
                                r.x = fg.x * fg.y / r.y + bg.x * bg.y * (1 - fg.y) / r.y;
                            }

                            Buffer[globalYPos][globalXPos] = r;
                        }
                    }
                }
            }
        }
    }
}
