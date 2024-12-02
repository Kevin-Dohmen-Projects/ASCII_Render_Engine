using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public class Poly2D : IRenderable
{
    public Vertex2D[] Vertices { get; set; }
    public Poly2D(Vertex2D[] vertices)
    {
        Vertices = vertices;
    }
    public Poly2D(int vertexCount)
    {
        Vertices = new Vertex2D[vertexCount];
    }
    public Poly2D(Poly2D poly)
    {
        Vertices = new Vertex2D[poly.Vertices.Length];
        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(poly.Vertices[i]);
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
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
