using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Transform.Rotation;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Mesh;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.Mesh3DRenderer;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Objects.Geometry.Primitives;

public class Cube : IRenderable
{
    public Vec3 Pos;
    public Vec3 Size;
    public Vec3 Origin;
    public IRotation Rotation;
    public Mesh3D Mesh;
    public CameraConfig? Camera;
    public IMesh3DRenderer? Renderer { get; set; }

    public Cube(Vec3 pos, Vec3 size, CameraConfig? camera = null, IMesh3DRenderer? renderer = null)
    {
        Pos = pos;
        Size = size;
        Origin = new Vec3();
        Rotation = new EulerRotation(new Vec3(0, 0, 0));
        Camera = camera;
        Renderer = renderer;
        Mesh = InitCube(size);
    }

    private Mesh3D InitCube(Vec3 size)
    {
        Vertex3D v1 = new(new Vec3(-size.x / 2, size.y / 2, -size.z / 2));
        Vertex3D v2 = new(new Vec3(size.x / 2, size.y / 2, -size.z / 2));
        Vertex3D v3 = new(new Vec3(size.x / 2, -size.y / 2, -size.z / 2));
        Vertex3D v4 = new(new Vec3(-size.x / 2, -size.y / 2, -size.z / 2));
        Vertex3D v5 = new(new Vec3(-size.x / 2, size.y / 2, size.z / 2));
        Vertex3D v6 = new(new Vec3(size.x / 2, size.y / 2, size.z / 2));
        Vertex3D v7 = new(new Vec3(size.x / 2, -size.y / 2, size.z / 2));
        Vertex3D v8 = new(new Vec3(-size.x / 2, -size.y / 2, size.z / 2));

        Quad3D p1 = new([v1, v2, v3, v4]); // south
        Quad3D p2 = new([v2, v6, v7, v3]); // east
        Quad3D p3 = new([v6, v5, v8, v7]); // north
        Quad3D p4 = new([v5, v1, v4, v8]); // west
        Quad3D p5 = new([v5, v6, v2, v1]); // top
        Quad3D p6 = new([v4, v3, v7, v8]); // bottom

        return new Mesh3D([p1, p2, p3, p4, p5, p6], Camera, Renderer);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Mesh.Origin = Origin;
        Mesh.Position = Pos;
        Mesh.Rotation = Rotation;
        Mesh.Camera = Camera;
        Mesh.Render(buffer, frame, runTime);
    }
}
