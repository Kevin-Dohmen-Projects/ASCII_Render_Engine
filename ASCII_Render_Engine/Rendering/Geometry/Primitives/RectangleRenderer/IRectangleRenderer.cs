using ASCII_Render_Engine.Core;

namespace ASCII_Render_Engine.Rendering.Geometry.Primitives.RectangleRenderer;

public interface IRectangleRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj) { }
}
