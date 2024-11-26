﻿using ASCII_Render_Engine.ScreenRelated;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Geometry.Lines
{
    public class Line2D : IRenderable
    {
        public Vec2 A;
        public Vec2 B;
        public Vec2 Color;

        public Line2D(Vec2 a, Vec2 b, Vec2 color)
        {
            A = a;
            B = b;
            Color = color;
        }

        //public Line2D(Vec2 a, Vec2 b)
        //{
        //    A = a;
        //    B = b;
        //}

        //public Line2D()
        //{
        //    A = new Vec2();
        //    B = new Vec2();
        //}

        public void Render(ScreenBuffer buffer, int frame, double runTime)
        {
            Vec2 deltaVec = B - A - 1;

            Vec2 min = new Vec2(Math.Min(this.A.x, this.B.x), Math.Min(this.A.y, this.B.y));
            Vec2 max = new Vec2(Math.Max(this.A.x, this.B.x), Math.Max(this.A.y, this.B.y));
            int xMin = (int)Math.Floor(Math.Max(min.x, 0));
            int yMin = (int)Math.Floor(Math.Max(min.y, 0));
            int xMax = (int)Math.Ceiling(Math.Min(max.x, buffer.Width)); //NOTE: the +1 is the laziest way of fixing THE problem
            int yMax = (int)Math.Ceiling(Math.Min(max.y, buffer.Height));
            bool isCol = false;

            double slope = deltaVec.y / deltaVec.x;

            bool isHorizontal = deltaVec.y == 0f;
            bool isVertical = deltaVec.x == 0f;
            bool isDot = deltaVec.x == 0 && deltaVec.y == 0f;
            bool isSideways = slope <= 1 && slope >= -1;

            //Console.WriteLine("{0} {1} {2} {3}", isHorizontal, isVertical, isDot, isSideways);

            if (isHorizontal)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    buffer.Buffer[yMin][x] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[yMin][x]);
                }
            }
            else if (isVertical)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    buffer.Buffer[y][xMin] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[y][xMin]);
                }
            }
            else if (isDot)
            {
                buffer.Buffer[yMin][xMin] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[yMin][xMin]);
            }
            else if (isSideways)
            {
                for (int y = yMin; y < yMax; y++)
                {
                    for (int x = xMin; x < xMax; x++)
                    {
                        isCol = false;
                        double f = -slope * (x - this.A.x) + (y - this.A.y);

                        if (f >= 0 && f < 1)
                        {
                            isCol = true;
                        }

                        if (isCol)
                        {
                            buffer.Buffer[y][x] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[y][x]);
                        }
                    }
                }
            }
            else
            {
                for (int y = yMin; y < yMax; y++)
                {
                    for (int x = xMin; x < xMax; x++)
                    {
                        isCol = false;
                        double f = -(1 / slope) * (y - this.A.y) + (x - this.A.x);
                        //double f = -(slope) * (x - this.A.x) + (y - this.A.y);

                        if (f >= 0 && f < 1)
                        {
                            isCol = true;
                        }

                        if (isCol)
                        {
                            buffer.Buffer[y][x] = RenderFuncs.AlphaTransform(Color, buffer.Buffer[y][x]);
                        }
                    }
                }
            }
        }

    }
}
