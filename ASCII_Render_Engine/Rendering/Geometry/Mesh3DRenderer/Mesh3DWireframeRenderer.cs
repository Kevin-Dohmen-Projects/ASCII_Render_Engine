using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Matrixes;
using ASCII_Render_Engine.MathUtils.Matrixes.Transforms;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Mesh;
using ASCII_Render_Engine.Objects.Geometry.Polygons;

namespace ASCII_Render_Engine.Rendering.Geometry.Mesh3DRenderer;

public class Mesh3DWireframeRenderer : IMesh3DRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Mesh3D obj)
    {
        Vec3 Position = obj.Position;
        Vec3 Origin = obj.Origin;
        Vec3 Angle = obj.Angle;
        Vec3 scale = obj.Scale;
        CameraConfig Camera = obj.Camera;
        IPolygon3D[] Polygons = obj.Polygons;

        // transform
        for (int i = 0; i < Polygons.Length; i++)
        {
            IPolygon3D localPoly = Polygons[i];
            IPolygon3D globalPoly = localPoly.Copy();
            for (int j = 0; j < localPoly.Vertices.Length; j++)
            {
                // rotate around local origin
                Vec3 vertex = localPoly.Vertices[j].Position;
                Mat3x3 rotationMatrix = Rotation.Rotate(Angle);
                Vec3 scaledVertex = vertex * scale;
                Vec3 rotatedVertex = rotationMatrix * (scaledVertex - Origin) + Origin;
                Vec3 globalVertex = rotatedVertex + Position;
                globalPoly.Vertices[j].Position = globalVertex;
            }
            globalPoly.Camera = Camera;
            globalPoly.Render(buffer, frame, runTime);
        }
    }
}
