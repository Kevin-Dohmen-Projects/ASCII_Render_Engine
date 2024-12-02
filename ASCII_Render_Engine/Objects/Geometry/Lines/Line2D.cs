using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering;

namespace ASCII_Render_Engine.Objects.Geometry.Lines
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
            Vec2 delta = B - A;
            double length = delta.Length();
            Vec2 step = delta / length;
            for (int j = 0; j < length; j++)
            {
                Vec2 pos = A + step * j;
                if (pos.x >= 0 && pos.x < buffer.Width && pos.y >= 0 && pos.y < buffer.Height)
                {
                    buffer.Buffer[(int)pos.y][(int)pos.x] = new Vec2(1, 1);
                }
            }
        }
    }
}
