using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Objects.Geometry.Polygons;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.Mesh3DRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Mesh;

public class Mesh3D : IRenderable
{
    public Poly3D[] Polygons { get; set; }
    public Vec3 Position { get; set; }
    public Vec3 Origin { get; set; }
    public Vec3 Angle { get; set; }
    public CameraConfig Camera { get; set; }
    public IMesh3DRenderer Renderer { get; set; } = new Mesh3DWireframeRenderer();

    // pool
    public Poly3D[] globalPolygons { get; set; }

    public Mesh3D(Poly3D[] polygons, CameraConfig camera)
    {
        Polygons = new Poly3D[polygons.Length];
        globalPolygons = new Poly3D[polygons.Length];
        for (int i = 0; i < polygons.Length; i++)
        {
            Polygons[i] = new Poly3D(polygons[i]);
            globalPolygons[i] = new Poly3D(Polygons[i]);
        }

        Position = new Vec3();
        Origin = new Vec3();
        Angle = new Vec3();
        Camera = camera;
    }
    public Mesh3D(int polygonCount, CameraConfig camera)
    {
        Polygons = new Poly3D[polygonCount];
        globalPolygons = new Poly3D[polygonCount];
        for (int i = 0; i < Polygons.Length; i++)
        {
            Polygons[i] = new Poly3D(3, null);
            globalPolygons[i] = new Poly3D(3, null);
        }

        Position = new Vec3();
        Origin = new Vec3();
        Angle = new Vec3();
        Camera = camera;
    }
    public Mesh3D(Mesh3D mesh)
    {
        Polygons = new Poly3D[mesh.Polygons.Length];
        globalPolygons = new Poly3D[mesh.Polygons.Length];
        for (int i = 0; i < mesh.Polygons.Length; i++)
        {
            Polygons[i] = new Poly3D(mesh.Polygons[i]);
            globalPolygons[i] = new Poly3D(mesh.Polygons[i]);
        }

        Position = mesh.Position;
        Origin = mesh.Origin;
        Angle = mesh.Angle;
        Camera = mesh.Camera;
    }

    public void Copy(Mesh3D mesh)
    {
        if (Polygons.Length != mesh.Polygons.Length)
        {
            Polygons = new Poly3D[mesh.Polygons.Length];
        }
        for (int i = 0; i < mesh.Polygons.Length; i++)
        {
            Polygons[i].Copy(mesh.Polygons[i]);
        }
        Position = mesh.Position;
        Origin = mesh.Origin;
        Angle = mesh.Angle;
    }

    public void Add(Poly3D poly)
    {
        Poly3D[] newPolygons = new Poly3D[Polygons.Length + 1];
        for (int i = 0; i < Polygons.Length; i++)
        {
            newPolygons[i] = Polygons[i];
        }
        newPolygons[Polygons.Length] = poly;
        Polygons = newPolygons;
    }
    public void Remove(int index)
    {
        Poly3D[] newPolygons = new Poly3D[Polygons.Length - 1];
        for (int i = 0; i < index; i++)
        {
            newPolygons[i] = Polygons[i];
        }
        for (int i = index + 1; i < Polygons.Length; i++)
        {
            newPolygons[i - 1] = Polygons[i];
        }
        Polygons = newPolygons;
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer.Render(buffer, frame, runTime, this);
    }
}
