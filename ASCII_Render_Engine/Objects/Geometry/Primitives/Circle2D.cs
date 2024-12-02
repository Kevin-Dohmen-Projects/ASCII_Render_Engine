using ASCII_Render_Engine.Core;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Rendering.Geometry.Primitives.CircleRenderer;
using ASCII_Render_Engine.Rendering.Shaders;

namespace ASCII_Render_Engine.Objects.Geometry.Primitives
{
    public class Circle2D : IRenderable
    {
        public Vec2 Pos;
        public Vec2 Size;
        public Vec2 Color;
        public IShader? Shader;
        public ICircleRenderer Renderer = new CircleRenderer();

        // object pool
        private ShaderPixel shaderPixel = new();
        private Vec2 shaderPixelScreenRes = new();

        public Circle2D(Vec2 pos, Vec2 size, Vec2 color)
        {
            Pos = pos;
            Size = size;
            Color = new Vec2(color.x, color.y);
        }

        public Circle2D(Vec2 pos, Vec2 size, IShader shader, double alpha = 1)
        {
            Pos = pos;
            Size = size;
            Shader = shader;
            Color = new Vec2(1, alpha);
        }

        public void Render(ScreenBuffer buffer, int frame, double runTime)
        {
            Renderer.Render(buffer, frame, runTime, this);
        }
    }
}
