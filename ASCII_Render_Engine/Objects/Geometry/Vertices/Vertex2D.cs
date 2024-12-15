using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Vertices;

public struct Vertex2D : IRenderable
{
    public Vec2 Position { get; set; }
    public Vec2 UV { get; set; }
    public Vec2 Normal { get; set; }
    public IVertex2DRenderer? Renderer { get; set; }

    public Vertex2D(Vertex2D vertex)
    {
        Position = vertex.Position;
        UV = vertex.UV;
        Normal = vertex.Normal;
        Renderer = vertex.Renderer;
    }

    public Vertex2D(Vec2 position, Vec2 uv, Vec2 normal)
    {
        Position = position;
        UV = uv;
        Normal = normal;
        Renderer = new Vertex2DRenderer();
    }

    public Vertex2D(Vec2 position)
    {
        Position = position;
        UV = new Vec2();
        Normal = new Vec2();
        Renderer = new Vertex2DRenderer();
    }

    public Vertex2D(double x, double y)
    {
        Position = new Vec2(x, y);
        UV = new Vec2();
        Normal = new Vec2();
        Renderer = new Vertex2DRenderer();
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer?.Render(buffer, frame, runTime, this);
    }
}
