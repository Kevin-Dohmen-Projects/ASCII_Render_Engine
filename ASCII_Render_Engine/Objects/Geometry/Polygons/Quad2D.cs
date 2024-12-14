using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public class Quad2D : IRenderable
{
    public Vertex2D[] Vertices { get; set; } = new Vertex2D[4];
    public IPoly2DRenderer Renderer { get; set; } = new Poly2DWireframeRenderer();

    // pool
    private Poly2D[] polys;

    public Quad2D(Vertex2D[] vertices)
    {
        if (vertices.Length != 4)
        {
            throw new System.Exception("Quad2D must have 4 vertices");
        }

        Vertices = vertices;
        polys = new Poly2D[Vertices.Length - 2];
        for (int i = 0; i < polys.Length; i++)
        {
            polys[i] = new Poly2D();
        }
    }
    public Quad2D()
    {
        Vertices = new Vertex2D[4];
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D();
        }

        polys = new Poly2D[Vertices.Length - 2];
        for (int i = 0; i < polys.Length; i++)
        {
            polys[i] = new Poly2D();
        }
    }
    public Quad2D(Quad2D quad)
    {
        Vertices = new Vertex2D[quad.Vertices.Length];
        for (int i = 0; i < quad.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(quad.Vertices[i]);
        }
        polys = new Poly2D[Vertices.Length - 2];
        for (int i = 0; i < polys.Length; i++)
        {
            polys[i] = new Poly2D();
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        // trun n-gon into a polygon
        for (int i = 0; i < polys.Length; i++)
        {
            polys[i].Vertices[0] = Vertices[0];
            polys[i].Vertices[1] = Vertices[i + 1];
            polys[i].Vertices[2] = Vertices[i + 2];
        }

        for (int i = 0; i < polys.Length; i++)
        {
            polys[i].Render(buffer, frame, runTime);
        }
    }
}
