using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Primitives;

namespace ASCII_Render_Engine.Rendering.Geometry.Primitives.CircleRenderer;

public interface ICircleRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Circle2D obj) { }
}
