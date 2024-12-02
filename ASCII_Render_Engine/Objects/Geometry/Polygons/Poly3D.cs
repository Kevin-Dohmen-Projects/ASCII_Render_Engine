using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;

public class Poly3D : IRenderable
{
    public Vertex3D[] Vertices { get; set; }
    public CameraConfig Camera { get; set; }

    // pool
    private Vertex3D[] transformedVertices;

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
        Vec2 screenResolution = new Vec2(buffer.Width, buffer.Height);

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i].Camera = Camera;
            transformedVertices[i].Position = Vertices[i].PerspectiveTransform(screenResolution);
        }

        for (int i = 0; i < transformedVertices.Length; i++)
        {
            Vertex3D vertex = transformedVertices[i];
            Vertex3D nextVertex = transformedVertices[(i + 1) % transformedVertices.Length];
            Vec3 delta = nextVertex.Position - vertex.Position;
            double length = delta.Length();
            Vec3 step = delta / length;
            for (int j = 0; j < length; j++)
            {
                Vec3 pos = vertex.Position + step * j;
                if (pos.x >= 0 && pos.x < buffer.Width && pos.y >= 0 && pos.y < buffer.Height && pos.z > 0)
                {
                    buffer.Buffer[(int)(buffer.Height - pos.y)][(int)pos.x] = new Vec2(1, 1);
                }
            }
        }
    }

}
