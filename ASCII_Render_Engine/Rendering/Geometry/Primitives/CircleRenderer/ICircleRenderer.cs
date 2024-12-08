using ASCII_Render_Engine.Core;

namespace ASCII_Render_Engine.Rendering.Geometry.Primitives.CircleRenderer;

public interface ICircleRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj) { }
}
