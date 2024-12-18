﻿using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct NGon2D : IRenderable
{
    Vertex2D[] Vertices { get; set; }
    public IPoly2DRenderer? Renderer { get; set; }

    // pool
    

    public NGon2D(Vertex2D[] vertices, IPoly2DRenderer? renderer = null)
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
        Renderer = renderer;
    }

    public NGon2D(NGon2D nGon)
    {
        Vertices = new Vertex2D[nGon.Vertices.Length];
        for (int i = 0; i < nGon.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex2D(nGon.Vertices[i]);
        }
        Renderer = nGon.Renderer;
    }

    public NGon2D()
    {
        Vertices = new Vertex2D[3];
        for (int i = 0; i < 3; i++)
        {
            Vertices[i] = new Vertex2D();
        }
        Renderer = null;
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        // turn the n-gon into n-2 triangles
        for (int i = 1; i < Vertices.Length - 1; i++)
        {
            Poly2D triangle = new([ Vertices[0], Vertices[i], Vertices[i + 1] ]);
            Renderer?.Render(buffer, frame, runTime, triangle);
        }
    }
}
