using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

public class Vertex3DRenderer : IVertex3DRenderer
{
    public static Vec3 PerspectiveTransform(Vec2 screenResolution, Vertex3D vertex)
    {
        return vertex.Camera.Camera.PerspectiveTransform(vertex.Position, screenResolution);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime, Vertex3D obj)
    {
        Vec3 screenPos = PerspectiveTransform(new Vec2(buffer.Width, buffer.Height), obj);
        if (screenPos.x >= 0 && screenPos.x < buffer.Width && screenPos.y >= 0 && screenPos.y < buffer.Height && screenPos.z > 0)
        {
            buffer.Buffer[(int)(buffer.Height - screenPos.y)][(int)screenPos.x] = new Vec2(1, 1);
        }
    }
}
