using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.ScreenRelated
{
    public static class RenderFuncs
    {
        public static Vec2 AlphaTransform(Vec2 fg, Vec2 bg)
        {
            // alpha transform (x = b/w, y = alpha)
            Vec2 r = new(0.0);

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

            r.x = Math.Clamp(r.x, 0.0, 1.0);
            r.y = Math.Clamp(r.y, 0.0, 1.0);

            return r;
        }
        public static Vec2 AlphaTransform(Vec2 fg, Vec2 bg, Vec2 tmpVec)
        {
            // alpha transform (x = b/w, y = alpha)
            Vec2 r = tmpVec;
            r.x = 0;
            r.y = 0;

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

            r.x = Math.Clamp(r.x, 0.0, 1.0);
            r.y = Math.Clamp(r.y, 0.0, 1.0);

            return r;
        }
    }
}
