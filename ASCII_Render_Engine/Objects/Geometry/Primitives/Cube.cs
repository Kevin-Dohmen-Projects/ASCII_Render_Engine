using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Mesh;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;

namespace ASCII_Render_Engine.Objects.Geometry.Primitives;

public class Cube : IRenderable
{
    public Vec3 Pos;
    public Vec3 Size;
    public Vec3 Origin;
    public Vec3 Rotation;
    public Mesh3D Mesh;
    public CameraConfig Camera;

    public Cube(Vec3 pos, Vec3 size, CameraConfig camera)
    {
        Pos = pos;
        Size = size;
        Origin = new Vec3();
        Rotation = new Vec3();
        Camera = camera;
        Mesh = InitCube(size);
    }

    public static Mesh3D InitCube(Vec3 size)
    {
        Vertex3D v1 = new(new Vec3(-size.x / 2, -size.y / 2, -size.z / 2));
        Vertex3D v2 = new(new Vec3(size.x / 2, -size.y / 2, -size.z / 2));
        Vertex3D v3 = new(new Vec3(size.x / 2, size.y / 2, -size.z / 2));
        Vertex3D v4 = new(new Vec3(-size.x / 2, size.y / 2, -size.z / 2));
        Vertex3D v5 = new(new Vec3(-size.x / 2, -size.y / 2, size.z / 2));
        Vertex3D v6 = new(new Vec3(size.x / 2, -size.y / 2, size.z / 2));
        Vertex3D v7 = new(new Vec3(size.x / 2, size.y / 2, size.z / 2));
        Vertex3D v8 = new(new Vec3(-size.x / 2, size.y / 2, size.z / 2));

        Quad3D p1 = new([v1, v2, v3, v4]);
        Quad3D p2 = new([v5, v6, v7, v8]);
        Quad3D p3 = new([v1, v5, v6, v2]);
        Quad3D p4 = new([v2, v6, v7, v3]);
        Quad3D p5 = new([v3, v7, v8, v4]);
        Quad3D p6 = new([v4, v8, v5, v1]);

        return new Mesh3D([p1, p2, p3, p4, p5, p6], null);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Mesh.Origin = Origin;
        Mesh.Position = Pos;
        Mesh.Angle = Rotation;
        Mesh.Camera = Camera;
        Mesh.Render(buffer, frame, runTime);
    }
}
