using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Transform;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Objects.Geometry.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;

public class Vertex2DRenderer : IVertex2DRenderer
{
    public void Render(ScreenBuffer buffer, int frame, double runTime, object obj)
    {
        if (obj is not Vertex2D vertex)
        {
            throw new ArgumentException("Object is not a Vertex2D");
        }

        Vec2 Position = vertex.Position;

        if (Position.x >= 0 && Position.x < buffer.Width && Position.y >= 0 && Position.y < buffer.Height)
        {
            buffer.Buffer[(int)Position.y][(int)Position.x] = new Vec2(1, 1);
        }
    }
}
