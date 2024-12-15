using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct Quad2D : IRenderable
{
    Vertex2D[] Vertices { get; set; }
    public IPoly2DRenderer? Renderer { get; set; } = new Poly2DWireframeRenderer();

    public Quad2D(Vertex2D[] vertices)
    {
        if (vertices.Length != 4)
        {
            throw new ArgumentException("Quads must have 4 vertices");
        }
        Vertices = new Vertex2D[4];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(vertices[i]);
        }
    }

    public Quad2D(Quad2D quad)
    {
        Vertices = new Vertex2D[4];
        for (int i = 0; i < 4; i++)
        {
            Vertices[i] = new Vertex2D(quad.Vertices[i]);
        }
    }

    public Quad2D()
    {
        Vertices = new Vertex2D[4];
        for (int i = 0; i < 4; i++)
        {
            Vertices[i] = new Vertex2D();
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        // turn the quad into 2 triangles
        Poly2D triangle1 = new(new Vertex2D[] { Vertices[0], Vertices[1], Vertices[2] });
        Poly2D triangle2 = new(new Vertex2D[] { Vertices[0], Vertices[2], Vertices[3] });
        Renderer?.Render(buffer, frame, runTime, triangle1);
        Renderer?.Render(buffer, frame, runTime, triangle2);
    }
}
