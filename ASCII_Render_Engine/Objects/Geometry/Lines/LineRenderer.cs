using ASCII_Render_Engine.Core;

namespace ASCII_Render_Engine.Objects.Geometry.Lines
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
