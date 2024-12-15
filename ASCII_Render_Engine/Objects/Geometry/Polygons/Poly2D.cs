using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct Poly2D : IRenderable
{
    public Vertex2D[] Vertices { get; set; }
    public IPoly2DRenderer Renderer { get; set; } = new Poly2DWireframeRenderer();

    public Poly2D(Vertex2D[] vertices)
    {
        if (vertices.Length != 3)
        {
            throw new ArgumentException("Polygons must have at least 3 vertices");
        }

        Vertices = new Vertex2D[3];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(vertices[i]);
        }
    }

    public Poly2D()
    {
        Vertices = new Vertex2D[3];
        for (int i = 0; i < 3; i++)
        {
            Vertices[i] = new Vertex2D();
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer.Render(buffer, frame, runTime, this);
    }
}
