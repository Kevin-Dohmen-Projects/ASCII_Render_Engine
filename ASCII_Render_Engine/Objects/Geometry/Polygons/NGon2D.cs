using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public class NGon2D : IRenderable, IPoly
{
    public Vertex2D[] Vertices { get; set; }
    public IPoly2DRenderer Renderer { get; set; } = new Poly2DWireframeRenderer();

    // pool
    private Poly2D[] polys;

    public NGon2D(Vertex2D[] vertices)
    {
        Vertices = vertices;
        polys = new Poly2D[Vertices.Length - 2];
        for (int i = 0; i < polys.Length; i++)
        {
            polys[i] = new Poly2D();
        }
    }
    public NGon2D(int vertexCount)
    {
        Vertices = new Vertex2D[vertexCount];
        polys = new Poly2D[Vertices.Length - 2];
        for (int i = 0; i < polys.Length; i++)
        {
            polys[i] = new Poly2D();
        }
    }
    public NGon2D(NGon2D poly)
    {
        Vertices = new Vertex2D[poly.Vertices.Length];
        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(poly.Vertices[i]);
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
