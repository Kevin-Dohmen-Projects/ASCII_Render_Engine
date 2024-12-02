using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public class Poly2D : IRenderable
{
    public Vertex2D[] Vertices { get; set; }
    public IPoly2DRenderer Renderer { get; set; } = new Poly2DWireframeRenderer();

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
       Renderer.Render(buffer, frame, runTime, this);
    }
}
