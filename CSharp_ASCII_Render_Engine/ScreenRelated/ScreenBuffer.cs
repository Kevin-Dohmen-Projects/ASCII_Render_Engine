using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
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
            InitBuffer();
        }

        private void InitBuffer()
        {
            Buffer = new List<List<Vec2>>();
            for (int i = 0; i < Width; i++)
            {
                Buffer.Add(new List<Vec2>());
                for (int j = 0; j < Height; j++)
                {
                    Buffer[i].Add(new Vec2());
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Buffer[i][j].x = 0;
                    Buffer[i][j].y = 0;
                }
            }
        }

        public void Insert(int xPos, int yPos, ScreenBuffer buffer)
        {
            int globalYPos = 0;
            int globalXPos = 0;
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
                            // alpha transform (x = b/w, y = alpha)
                            Vec2 bg = Buffer[globalYPos][globalXPos]; // background
                            Vec2 fg = buffer.Buffer[y][x]; // foreground
                            Vec2 r = new(0.0);

                            if (fg.y >= 1.0)
                            {
                                r = fg;
                            }
                            else if (fg.y <= 0.0) 
                            {
                                r = bg;
                            }
                            else
                            {
                                r.y = 1.0 - (1.0 - fg.y) * (1.0 - bg.y);

                                if (r.y < 1e-6)
                                {
                                    r.y = 1e-6;
                                }

                                r.x = fg.x * fg.y / r.y + bg.x * bg.y * (1.0 - fg.y) / r.y;
                            }

                            r.x = Math.Clamp(r.x, 0.0, 1.0);
                            r.y = Math.Clamp(r.y, 0.0, 1.0);

                            Buffer[globalYPos][globalXPos] = r;
                        }
                    }
                }
            }
        }
    }
}
