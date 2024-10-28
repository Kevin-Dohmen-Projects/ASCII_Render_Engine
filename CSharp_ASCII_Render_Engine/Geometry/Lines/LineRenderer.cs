using CSharp_ASCII_Render_Engine.ScreenRelated;

namespace CSharp_ASCII_Render_Engine.Geometry.Lines
{
    internal class LineRenderer
    {
    }

    public interface ILineRenderer
    {
        public void Draw();
        public ScreenBuffer Render();
    }
}
