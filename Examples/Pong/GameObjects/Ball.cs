using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASCII_Render_Engine.Objects.Geometry.Primitives;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Rendering.Shaders;
using ASCII_Render_Engine.Rendering;
using ASCII_Render_Engine.Core;

namespace Example_ASCII_Game_Engine.GameObjects
{
    internal class Ball
    {
        public Vec2 Pos;
        private Circle BallObject;
        public double Radius;

        public Ball(Vec2 pos, double radius)
        {
            BallObject = new Circle(new Vec2(0, 0), new Vec2(10, 10), new Vec2(1, 1));
            Radius = radius;
            Pos = pos;
        }

        public Ball(Vec2 pos, IShader shader)
        {
            BallObject = new Circle(new Vec2(0, 0), new Vec2(10, 10), shader);
            Radius = 1;
            Pos = pos;
        }

        public IRenderable ToRenderable()
        {
            BallObject.Pos = new Vec2(Pos.x - Radius, Pos.y - Radius);
            BallObject.Size = new Vec2(Radius * 2, Radius * 2);
            return BallObject;
        }
    }
}
