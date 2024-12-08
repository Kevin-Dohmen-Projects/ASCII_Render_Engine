using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Primitives;

namespace ASCII_Render_Engine.Rendering.Geometry.Primitives.RectangleRenderer;

public interface IRectangleRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Rectangle2D obj) { }
}
