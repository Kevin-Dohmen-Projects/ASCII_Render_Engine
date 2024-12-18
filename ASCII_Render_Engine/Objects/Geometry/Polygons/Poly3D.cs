using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct Poly3D : IRenderable, IPolygon3D
{
    public Vertex3D[] Vertices { get; set; }
    public IPoly3DRenderer? Renderer { get; set; }
    public CameraConfig? Camera { get; set; }

    public Poly3D(Vertex3D[] vertices, CameraConfig? camera = null, IPoly3DRenderer? renderer = null)
    {
        if (vertices.Length != 3)
        {
            throw new ArgumentException("Polygons must have 3 vertices");
        }
        Vertices = new Vertex3D[3];
        for (int i = 0; i < 3; i++)
        {
            Vertices[i] = new Vertex3D(vertices[i]);
        }
        Renderer = renderer;
        Camera = camera;
    }

    public Poly3D(Poly3D poly)
    {
        Vertices = new Vertex3D[poly.Vertices.Length];
        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D(poly.Vertices[i]);
        }
        Renderer = poly.Renderer;
        Camera = poly.Camera;
    }

    public Poly3D()
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
        return new Poly3D(this);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer?.Render(buffer, frame, runTime, this);
    }
}
