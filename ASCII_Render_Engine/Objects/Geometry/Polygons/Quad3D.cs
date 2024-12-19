using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;
public struct Quad3D : IRenderable, IPolygon3D
{
    public Vertex3D[] Vertices { get; set; }
    public IPoly3DRenderer? Renderer { get; set; } = new Poly3DWireframeRenderer();
    public CameraConfig? Camera { get; set; }

    public Quad3D(Vertex3D[] vertices, CameraConfig? camera = null, IPoly3DRenderer? renderer = null)
    {
        if (vertices.Length != 4)
        {
            throw new ArgumentException("Quads must have 4 vertices");
        }
        Vertices = new Vertex3D[4];
        for (int i = 0; i < 4; i++)
        {
            Vertices[i] = new Vertex3D(vertices[i]);
        }
        Renderer = renderer;
        Camera = camera;
    }

    public Quad3D(Quad3D quad)
    {
        Vertices = new Vertex3D[4];
        for (int i = 0; i < 4; i++)
        {
            Vertices[i] = new Vertex3D(quad.Vertices[i]);
        }
        Renderer = quad.Renderer;
        Camera = quad.Camera;
    }

    public Quad3D()
    {
        Vertices = new Vertex3D[4];
        for (int i = 0; i < 4; i++)
        {
            Vertices[i] = new Vertex3D();
        }
        Renderer = new Poly3DWireframeRenderer();
    }

    public IPolygon3D Copy()
    {
        return new Quad3D(this);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        // turn the quad into 2 triangles
        Poly3D triangle1 = new([Vertices[0], Vertices[1], Vertices[2]], Camera, null);
        Poly3D triangle2 = new([Vertices[0], Vertices[2], Vertices[3]], Camera, null);
        Renderer?.Render(buffer, frame, runTime, triangle1);
        Renderer?.Render(buffer, frame, runTime, triangle2);
    }
}
