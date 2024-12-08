using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Rendering;

public static class RenderFuncs
{
    public static Vec2 AlphaTransform(Vec2 fg, Vec2 bg)
    {
        // alpha transform (x = b/w, y = alpha)
        Vec2 r = new();

        if (fg.y >= 1.0)
        {
            r.x = fg.x;
            r.y = fg.y;
        }
        else if (fg.y <= 0.0)
        {
            r.x = bg.x;
            r.y = bg.y;
        }
        else
        {
            r.y = 1.0 - (1.0 - fg.y) * (1.0 - bg.y);

            if (r.y < 1e-6)
            {
                r.y = 1e-6;
            }

            r.x = fg.x * fg.y / r.y + bg.x * bg.y * (1.0 - fg.y) / r.y;
        }

        return new Vec2(
            Math.Clamp(r.x, 0.0, 1.0),
            Math.Clamp(r.y, 0.0, 1.0)
        );
    }
}
