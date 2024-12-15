using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Vertices;

public struct Vertex3D : IRenderable
{
    public Vec3 Position { get; set; }
    public Vec2 UV { get; set; }
    public Vec3 Normal { get; set; }
    public CameraConfig? Camera { get; set; }
    public IVertex3DRenderer? Renderer { get; set; } = new Vertex3DRenderer();

    public Vertex3D(Vec3 position, Vec2 uv, Vec3 normal, CameraConfig camera = null)
    {
        Position = position;
        UV = uv;
        Normal = normal;
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
        Normal = new Vec3();
        Camera = null;
    }

    public Vertex3D(Vertex3D vertex)
    {
        Position = vertex.Position;
        UV = vertex.UV;
        Normal = vertex.Normal;
        Camera = vertex.Camera;
        Renderer = vertex.Renderer;
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer?.Render(buffer, frame, runTime, this);
    }
}
