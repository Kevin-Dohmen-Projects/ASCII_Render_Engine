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
        CameraConfig Camera = obj.Camera;
        Poly3D[] Polygons = obj.Polygons;
        Poly3D[] globalPolygons = obj.globalPolygons;

        // transform
        for (int i = 0; i < Polygons.Length; i++)
        {
            Poly3D localPoly = Polygons[i];
            Poly3D globalPoly = globalPolygons[i];
            globalPoly.Copy(localPoly);
            for (int j = 0; j < localPoly.Vertices.Length; j++)
            {
                // rotate around local origin
                Vec3 vertex = localPoly.Vertices[j].Position;
                Mat3x3 rotationMatrix = Rotation.Rotate(Angle);
                Vec3 rotatedVertex = rotationMatrix * (vertex - Origin) + Origin;
                Vec3 globalVertex = rotatedVertex + Position;
                globalPoly.Vertices[j].Position = globalVertex;
            }
            globalPoly.Camera = Camera;
            globalPoly.Render(buffer, frame, runTime);
        }
    }
}
