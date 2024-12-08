using ASCII_Render_Engine.Core;

namespace ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

public interface IVertex2DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj) { }
}
