using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Geometry.Lines
{
    public class Line2D
    {
        public Vec2 A;
        public Vec2 B;
        public Vec2? Color;

        public Line2D(Vec2 a, Vec2 b, Vec2 color)
        {
            A = a;
            B = b;
            Color = color;
        }
        public Line2D(Vec2 a, Vec2 b)
        {
            A = a;
            B = b;
        }
        public Line2D()
        {
            A = new Vec2();
            B = new Vec2();
        }

    }
}
