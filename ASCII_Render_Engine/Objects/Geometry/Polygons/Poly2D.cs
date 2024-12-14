using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public class Poly2D : IRenderable
{
    public Vertex2D[] Vertices { get; set; } = new Vertex2D[3];

    public IPoly2DRenderer Renderer { get; set; } = new Poly2DWireframeRenderer();

    public Poly2D(Vertex2D[] vertices)
    {
        if (vertices.Length != 3)
        {
            throw new System.Exception("Poly3D must have 3 vertices");
        }

        Vertices = vertices;
    }
    public Poly2D()
    {
        Vertices = new Vertex2D[3];
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
