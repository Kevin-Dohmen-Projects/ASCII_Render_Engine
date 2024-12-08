using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Lines;

namespace ASCII_Render_Engine.Rendering.Geometry.LineRenderer;

public interface ILine2DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Line2D obj) { }
}
