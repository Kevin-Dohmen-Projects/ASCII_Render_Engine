using ASCII_Render_Engine.MathUtils.Transform.Rotation;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Objects.Camera
{
    public interface ICamera
    {
        public Vec3 Position { get; set; }
        public IRotation Rotation { get; set; }
        public Vec3 TranslateToCameraSpace(Vec3 point);
        public Vec3 PerspectiveTransform(Vec3 worldPoint, Vec2 screenResolution, double aspectRatio = 0);
    }
}
