using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.LineRenderer;

namespace ASCII_Render_Engine.Objects.Geometry.Lines;

public class Line2D : IRenderable
{
    public Vec2 A;
    public Vec2 B;
    public Vec2 Color;
    ILine2DRenderer Renderer = new Line2DRenderer();

    public Line2D(Vec2 a, Vec2 b, Vec2 color)
    {
        A = a;
        B = b;
        Color = color;
    }

    //public Line2D(Vec2 a, Vec2 b)
    //{
    //    A = a;
    //    B = b;
    //}

    //public Line2D()
    //{
    //    A = new Vec2();
    //    B = new Vec2();
    //}

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Renderer.Render(buffer, frame, runTime, this);
    }
}
