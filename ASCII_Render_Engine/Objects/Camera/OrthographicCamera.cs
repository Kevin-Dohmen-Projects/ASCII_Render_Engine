using ASCII_Render_Engine.MathUtils.Matrixes;
using ASCII_Render_Engine.MathUtils.Transform.Rotation;
using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Objects.Camera;

public class OrthographicCamera3D : ICamera
{
    public Vec3 Position { get; set; }
    public IRotation Rotation { get; set; }
    public double OrthographicScale;

    public OrthographicCamera3D(Vec3 position, IRotation rotation, double orthographicScale)
    {
        Position = position;
        Rotation = rotation;
        OrthographicScale = orthographicScale;
    }

    public OrthographicCamera3D()
    {
        Position = new Vec3(0, 0, 0);
        Rotation = new QuaternionRotation(new Vec3(0, 0, 1), 0);
        OrthographicScale = 1; // Default scale should not be zero
    }

    public OrthographicCamera3D(OrthographicCamera3D camera)
    {
        Position = new Vec3(camera.Position);
        Rotation = camera.Rotation;
        OrthographicScale = camera.OrthographicScale;
    }

    public Vec3 PerspectiveTransform(Vec3 point, Vec2 screenResolution, double aspectRatio = 0)
    {
        point -= Position;
        Vec3 transformedPoint = Rotation.RotateVector(point);

        // Apply orthographic scaling
        double x = transformedPoint.x / OrthographicScale;
        double y = transformedPoint.y / OrthographicScale;
        double z = transformedPoint.z / OrthographicScale;

        if (aspectRatio == 0)
        {
            aspectRatio = screenResolution.x / screenResolution.y;
        }

        // Adjust for aspect ratio
        x /= aspectRatio;

        // Map to screen resolution (normalized to pixel coordinates)
        double screenX = (x + 1) * 0.5 * screenResolution.x; // Map from [-1, 1] to [0, width]
        double screenY = (y + 1) * 0.5 * screenResolution.y;
        double ZLevel = z;

        return new Vec3(screenX, screenY, ZLevel);
    }

}
