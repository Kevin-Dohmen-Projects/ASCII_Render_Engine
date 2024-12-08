using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Vertices;

public class Vertex3D : IRenderable
{
    public Vec3 Position { get; set; }
    public Vec2 UV { get; set; }
    public CameraConfig Camera { get; set; }
    public IVertex3DRenderer Renderer { get; set; } = new Vertex3DRenderer();

    public Vertex3D(Vec3 position, Vec2 uv, CameraConfig camera = null)
    {
        Position = position;
        UV = uv;
        Camera = camera;
    }

    public Vertex3D(Vec3 position, CameraConfig camera = null)
    {
        Position = position;
        UV = new Vec2();
        Camera = camera;
    }

    public Vertex3D()
    {
        Position = new Vec3();
        UV = new Vec2();
    }

    public Vertex3D(Vertex3D vertex)
    {
        Position = new Vec3(vertex.Position);
        UV = new Vec2(vertex.UV);
    }

    public void Copy(Vertex3D vertex)
    {
        Position = new Vec3(vertex.Position);
        UV = new Vec2(vertex.UV);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer.Render(buffer, frame, runTime, this);
    }
}
