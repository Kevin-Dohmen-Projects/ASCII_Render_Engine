using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct NGon3D : IRenderable, IPolygon3D
{
    public Vertex3D[] Vertices { get; set; }
    public IPoly3DRenderer? Renderer { get; set; }
    public CameraConfig? Camera { get; set; }

    public NGon3D(Vertex3D[] vertices, CameraConfig? camera = null, IPoly3DRenderer? renderer = null)
    {
        if (vertices.Length < 3)
        {
            throw new ArgumentException("N-Gons must have at least 3 vertices");
        }
        Vertices = new Vertex3D[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D(vertices[i]);
        }
        Renderer = renderer;
        Camera = camera;
    }

    public NGon3D(NGon3D nGon)
    {
        Vertices = new Vertex3D[nGon.Vertices.Length];
        for (int i = 0; i < nGon.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D(nGon.Vertices[i]);
        }
        Camera = nGon.Camera;
        Renderer = nGon.Renderer;
    }

    public NGon3D(int vertexCount)
    {
        Vertices = new Vertex3D[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            Vertices[i] = new Vertex3D();
        }
        Renderer = null;
    }

    public NGon3D()
    {
        Vertices = new Vertex3D[3];
        for (int i = 0; i < 3; i++)
        {
            Vertices[i] = new Vertex3D();
        }
        Renderer = null;
    }

    public IPolygon3D Copy()
    {
        return new NGon3D(this);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        // turn the NGon into n-2 triangles
        for (int i = 1; i < Vertices.Length - 1; i++)
        {
            Poly3D triangle = new(new Vertex3D[] { Vertices[0], Vertices[i], Vertices[i + 1] }, Camera, null);
            Renderer?.Render(buffer, frame, runTime, triangle);
        }
    }
}
