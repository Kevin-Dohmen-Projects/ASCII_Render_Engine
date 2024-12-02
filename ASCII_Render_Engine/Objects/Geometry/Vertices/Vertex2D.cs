using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.VertexRenderer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Geometry.Vertices
{
    public class Vertex2D : IRenderable
    {
        public Vec2 Position { get; set; }
        public Vec2 UV { get; set; }
        public IVertex2DRenderer Renderer { get; set; } = new Vertex2DRenderer();

        public Vertex2D(Vec2 position, Vec2 uv)
        {
            Position = position;
            UV = uv;
        }

        public Vertex2D()
        {
            Position = new Vec2();
            UV = new Vec2();
        }

        public Vertex2D(Vertex2D vertex)
        {
            Position = new Vec2(vertex.Position);
            UV = new Vec2(vertex.UV);
        }

        public void Render(ScreenBuffer buffer, int frame, double runTime)
        {
            Renderer.Render(buffer, frame, runTime, this);
        }
    }
}
