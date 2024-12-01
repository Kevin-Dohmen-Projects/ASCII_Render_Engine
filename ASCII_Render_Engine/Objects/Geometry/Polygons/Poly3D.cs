using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Polygons;

public class Poly3D : IRenderable
{
    public Vertex3D[] Vertices { get; set; }
    public CameraConfig Camera { get; set; }

    public Poly3D(Vertex3D[] vertices, CameraConfig cameraConfig = null)
    {
        Vertices = vertices;
        Camera = cameraConfig;
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i].Camera = Camera;
        }
    }
    public Poly3D(int vertexCount, CameraConfig cameraConfig = null)
    {
        Camera = cameraConfig;
        Vertices = new Vertex3D[vertexCount];
        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D();
            Vertices[i].Camera = Camera;
        }
    }
    public Poly3D(Poly3D poly)
    {
        Vertices = new Vertex3D[poly.Vertices.Length];
        Camera = poly.Camera;
        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i] = new Vertex3D(poly.Vertices[i]);
        }
    }

    public void Copy(Poly3D poly)
    {
        if (Vertices.Length != poly.Vertices.Length)
        {
            Vertices = new Vertex3D[poly.Vertices.Length];
        }

        Camera = poly.Camera;

        for (int i = 0; i < poly.Vertices.Length; i++)
        {
            Vertices[i].Copy(poly.Vertices[i]);
        }
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Vec2 screenResolution = new Vec2(buffer.Width, buffer.Height);

        Vertex2D[] vertices2D = new Vertex2D[Vertices.Length];

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertex3D vertex = Vertices[i];
            vertex.Camera = Camera; // NOTE: possibly redundant but just in case
            Vec2 screenPos = vertex.PerspectiveTransform(screenResolution);
            vertices2D[i] = new Vertex2D(screenPos, new Vec2()); // Populate the array
        }

        for (int i = 0; i < Vertices.Length; i++)
        {
            Vertex2D vertex = vertices2D[i];
            Vertex2D nextVertex = vertices2D[(i + 1) % vertices2D.Length];
            Vec2 delta = nextVertex.Position - vertex.Position;
            double length = delta.Length();
            Vec2 step = delta / length;
            for (int j = 0; j < length; j++)
            {
                Vec2 pos = vertex.Position + step * j;
                if (pos.x >= 0 && pos.x < buffer.Width && pos.y >= 0 && pos.y < buffer.Height)
                {
                    buffer.Buffer[(int)pos.y][(int)pos.x] = new Vec2(1, 1);
                }
            }
        }
    }

}
