using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Camera
{
    public interface ICamera
    {
        public Vec3 Position { get; set; }
        public Vec3 Direction { get; set; }
        public Vec2 PerspectiveTransform(Vec3 point, Vec2 screenResolution, double aspectRatio = 0);
    }
}
