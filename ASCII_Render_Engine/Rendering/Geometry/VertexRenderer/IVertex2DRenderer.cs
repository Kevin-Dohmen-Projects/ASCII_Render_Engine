using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;

namespace ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

public interface IVertex2DRenderer : IGeometryRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Vertex2D obj) { }
}
