using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using System.Diagnostics;

namespace ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

public class Poly3DWireframeRenderer : IPoly3DRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Poly3D obj)
    {

        Vertex3D[] Vertices = obj.Vertices;
        CameraConfig Camera = obj.Camera;
        Poly3D transformedPoly = new Poly3D(obj);


        Vec2 screenResolution = new Vec2(buffer.Width, buffer.Height);

        for (int i = 0; i < Vertices.Length; i++)
        {
            transformedPoly.Vertices[i].Position = Camera.Camera.PerspectiveTransform(Vertices[i].Position, screenResolution);
        }

        for (int i = 0; i < transformedPoly.Vertices.Length; i++)
        {
            Vertex3D vertex = transformedPoly.Vertices[i];
            Vertex3D nextVertex = transformedPoly.Vertices[(i + 1) % transformedPoly.Vertices.Length];
            Vec3 delta = nextVertex.Position - vertex.Position;
            double length = delta.Length();
            Vec3 step = delta / length;
            for (int j = 0; j < length; j++)
            {
                Vec3 pos = vertex.Position + step * j;
                if (pos.x >= 0 && pos.x < buffer.Width && pos.y >= 0 && pos.y < buffer.Height && pos.z > 0)
                {
                    buffer.Buffer[(int)(buffer.Height - 1 - pos.y)][(int)pos.x] = new Vec2(1, 1);
                }
            }
        }
    }
}
