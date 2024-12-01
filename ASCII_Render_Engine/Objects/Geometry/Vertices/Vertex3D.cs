using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Camera;
using ASCII_Render_Engine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Vertices;

public class Vertex3D : IRenderable
{
    public Vec3 Position { get; set; }
    public Vec2 UV { get; set; }
    public CameraConfig Camera { get; set; }

    public Vertex3D(Vec3 position, Vec2 uv, CameraConfig camera = null)
    {
        Position = position;
        UV = uv;
        Camera = camera;
    }

    public Vertex3D()
    {
        Position = new Vec3();
        UV = new Vec2();
    }

    public Vertex3D(Vertex3D vertex)
    {
        Position = new Vec3(vertex.Position);
        UV = new Vec2(vertex.UV);
    }

    public void Copy(Vertex3D vertex)
    {
        Position = new Vec3(vertex.Position);
        UV = new Vec2(vertex.UV);
    }

    public Vec2 PerspectiveTransform(Vec2 screenResolution)
    {
        return Camera.Camera.PerspectiveTransform(Position, screenResolution);
    }

    public void Render(ScreenBuffer buffer, int frame, double runTime)
    {
        Vec2 screenPos = PerspectiveTransform(new Vec2(buffer.Width, buffer.Height));
        if (screenPos.x >= 0 && screenPos.x < buffer.Width && screenPos.y >= 0 && screenPos.y < buffer.Height)
        {
            buffer.Buffer[(int)screenPos.y][(int)screenPos.x] = new Vec2(1, 1);
        }
    }
}
