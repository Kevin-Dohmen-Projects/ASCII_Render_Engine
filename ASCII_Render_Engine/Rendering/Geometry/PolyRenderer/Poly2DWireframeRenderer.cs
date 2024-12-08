using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;

namespace ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

public class Poly2DWireframeRenderer : IPoly2DRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Poly2D obj)
    {

        Vertex2D[] Vertices = obj.Vertices;

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertex2D vertex = Vertices[i];
            Vertex2D nextVertex = Vertices[(i + 1) % Vertices.Length];
            Vec2 delta = nextVertex.Position - vertex.Position;
            double length = delta.Length();
            Vec2 step = delta / length;
            for (int j = 0; j < length; j++)
            {
                Vec2 pos = vertex.Position + step * j;
                if (pos.x >= 0 && pos.x < buffer.Width && pos.y >= 0 && pos.y < buffer.Height)
                {
                    buffer.Buffer[(int)pos.y][(int)pos.x] = new Vec2(1, 1);
                }
            }
        }
    }
}
