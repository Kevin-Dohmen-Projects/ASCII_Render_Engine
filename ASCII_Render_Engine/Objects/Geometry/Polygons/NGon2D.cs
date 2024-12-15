using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct NGon2D : IRenderable
{
    Vertex2D[] Vertices { get; set; }
    public IPoly2DRenderer? Renderer { get; set; } = new Poly2DWireframeRenderer();

    public NGon2D(Vertex2D[] vertices)
    {
        if (vertices.Length < 3)
        {
            throw new ArgumentException("NGons must have at least 3 vertices");
        }
        Vertices = new Vertex2D[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(vertices[i]);
        }
    }

    public NGon2D(NGon2D nGon)
    {
        Vertices = new Vertex2D[nGon.Vertices.Length];
        for (int i = 0; i < nGon.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(nGon.Vertices[i]);
        }
    }

    public NGon2D()
    {
        Vertices = new Vertex2D[3];
        for (int i = 0; i < 3; i++)
        {
            Vertices[i] = new Vertex2D();
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        // turn the n-gon into n-2 triangles
        for (int i = 1; i < Vertices.Length - 1; i++)
        {
            Poly2D triangle = new(new Vertex2D[] { Vertices[0], Vertices[i], Vertices[i + 1] });
            Renderer?.Render(buffer, frame, runTime, triangle);
        }
    }
}
