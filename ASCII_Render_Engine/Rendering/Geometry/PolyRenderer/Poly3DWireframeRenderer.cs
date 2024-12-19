using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Types.Vectors;
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
            transformedPoly.Vertices[i].Position = Camera.Camera.TranslateToCameraSpace(Vertices[i].Position);
        }

        // calculate the normal
        Vec3 normal = Vec3.Cross(
            transformedPoly.Vertices[1].Position - transformedPoly.Vertices[0].Position,
            transformedPoly.Vertices[2].Position - transformedPoly.Vertices[0].Position
            ).Normalize();

        for (int i = 0; i < Vertices.Length; i++)
        {
            transformedPoly.Vertices[i].Position = Camera.Camera.PerspectiveTransform(transformedPoly.Vertices[i].Position, screenResolution);
        }

        // dont draw behind the camera
        if (transformedPoly.Vertices[0].Position.z < 0 &&
            transformedPoly.Vertices[1].Position.z < 0 &&
            transformedPoly.Vertices[2].Position.z < 0)
            return;

        // backface culling
        if (normal.z > 0)
            return;

        // Draw the wireframe
        for (int i = 0; i < transformedPoly.Vertices.Length; i++)
        {
            Vertex3D vertex = transformedPoly.Vertices[i];
            Vertex3D nextVertex = transformedPoly.Vertices[(i + 1) % transformedPoly.Vertices.Length];

            Vec3 drawVertex = vertex.Position;
            Vec3 drawNextVertex = nextVertex.Position;

            // only draw in from of the camera
            if (drawVertex.z < 0 && drawNextVertex.z < 0)
                continue;

            // only draw the part in view of the camera
            if (drawVertex.x < 0 && drawNextVertex.x < 0)
                continue;
            if (drawVertex.x >= buffer.Width && drawNextVertex.x >= buffer.Width)
                continue;
            if (drawVertex.y < 0 && drawNextVertex.y < 0)
                continue;
            if (drawVertex.y >= buffer.Height && drawNextVertex.y >= buffer.Height)
                continue;

            Vec3 delta = drawNextVertex - drawVertex;
            double length = delta.Length();
            Vec3 step = delta / length;
            for (int j = 0; j < length; j++)
            {
                Vec3 pos = drawVertex + step * j;
                if (pos.x >= 0 && pos.x < buffer.Width && pos.y >= 0 && pos.y < buffer.Height && pos.z > 0)
                {
                    buffer.Buffer[(int)(buffer.Height - 1 - pos.y)][(int)pos.x] = new Vec2(1, 1);
                }
            }
        }
    }
}
