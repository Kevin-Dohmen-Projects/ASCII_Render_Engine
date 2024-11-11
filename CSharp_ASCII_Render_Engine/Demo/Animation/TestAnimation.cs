using CSharp_ASCII_Render_Engine.Types.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Demo.Animation
{
    public class TestAnimation : ITransformAnimation
    {
        double centerx = 0.5;
        double centery = 0.5;
        public TestAnimation() {}
        public Vec2 Animate(double time)
        {
            Vec2 pos = new Vec2();
            pos.x = centerx + (Math.Sin(time) / 4);
            pos.y = centery + (Math.Cos(time) / 4);
            return pos;
        }
    }
}
