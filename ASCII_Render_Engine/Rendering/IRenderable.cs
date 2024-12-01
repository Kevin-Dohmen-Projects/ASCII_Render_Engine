using ASCII_Render_Engine.Core;

namespace ASCII_Render_Engine.Rendering;

public interface IRenderable
{
    void Render(ScreenBuffer buffer, int frame, double runTime);
}
