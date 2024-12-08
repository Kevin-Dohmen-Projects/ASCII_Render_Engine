using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Polygons;

namespace ASCII_Render_Engine.Rendering.Geometry.PolyRenderer;

public interface IPoly2DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Poly2D obj) { }
}
