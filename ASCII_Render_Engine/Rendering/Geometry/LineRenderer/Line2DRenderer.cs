using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Lines;

namespace ASCII_Render_Engine.Rendering.Geometry.LineRenderer;

public class Line2DRenderer : ILine2DRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Line2D obj)
    {
        Vec2 A = obj.A;
        Vec2 B = obj.B;
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
