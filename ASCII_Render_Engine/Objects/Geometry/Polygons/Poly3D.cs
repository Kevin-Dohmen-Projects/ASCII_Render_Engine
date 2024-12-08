using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;

public class Poly3D : IRenderable
{
    public Vertex3D[] Vertices { get; set; }
    public CameraConfig Camera { get; set; }
    public IPoly3DRenderer Renderer { get; set; } = new Poly3DWireframeRenderer();

    // pool
    public Vertex3D[] transformedVertices;

    public Poly3D(Vertex3D[] vertices, CameraConfig cameraConfig = null)
    {
        Vertices = vertices;
        transformedVertices = new Vertex3D[Vertices.Length];
        Camera = cameraConfig;
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i].Camera = Camera;
            transformedVertices[i] = new Vertex3D(vertices[i]);
        }
    }
    public Poly3D(int vertexCount, CameraConfig cameraConfig = null)
    {
        Camera = cameraConfig;
        Vertices = new Vertex3D[vertexCount];
        transformedVertices = new Vertex3D[Vertices.Length];
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D();
            transformedVertices[i] = new Vertex3D();
            Vertices[i].Camera = Camera;
        }
    }
    public Poly3D(Poly3D poly)
    {
        Vertices = new Vertex3D[poly.Vertices.Length];
        transformedVertices = new Vertex3D[Vertices.Length];
        Camera = poly.Camera;
        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D(poly.Vertices[i]);
            transformedVertices[i] = new Vertex3D(poly.transformedVertices[i]);
        }
    }

    public void Copy(Poly3D poly)
    {
        if (Vertices.Length != poly.Vertices.Length)
        {
            Vertices = new Vertex3D[poly.Vertices.Length];
            transformedVertices = new Vertex3D[Vertices.Length];
        }

        Camera = poly.Camera;

        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i].Copy(poly.Vertices[i]);
            transformedVertices[i].Copy(poly.transformedVertices[i]);
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer.Render(buffer, frame, runTime, this);
    }

}
