using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

public class Vertex2DRenderer : IVertex2DRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, Vertex2D obj)
    {
        Vec2 Position = obj.Position;

        if (Position.x >= 0 && Position.x < buffer.Width && Position.y >= 0 && Position.y < buffer.Height)
        {
            buffer.Buffer[(int)Position.y][(int)Position.x] = new Vec2(1, 1);
        }
    }
}
