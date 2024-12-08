using ASCII_Render_Engine.MathUtils.Matrixes;
using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.Objects.Camera;

public class OrthographicCamera3D : ICamera
{
    public Vec3 Position { get; set; }
    public Vec3 Direction { get; set; }
    public double OrthographicScale;

    public OrthographicCamera3D(Vec3 position, Vec3 direction, double orthographicScale)
    {
        Position = position;
        Direction = direction;
        OrthographicScale = orthographicScale;
    }

    public OrthographicCamera3D()
    {
        Position = new Vec3(0, 0, 0);
        Direction = new Vec3(0, 0, 1);
        OrthographicScale = 1; // Default scale should not be zero
    }

    public OrthographicCamera3D(OrthographicCamera3D camera)
    {
        Position = new Vec3(camera.Position);
        Direction = new Vec3(camera.Direction);
        OrthographicScale = camera.OrthographicScale;
    }

    public Vec3 PerspectiveTransform(Vec3 point, Vec2 screenResolution, double aspectRatio = 0)
    {
        // Normalize the forward direction
        Vec3 forward = Direction.Normalize();

        // Handle edge case for cross product (if forward is parallel to up)
        Vec3 globalUp = new Vec3(0, 1, 0);
        Vec3 right = Vec3.Cross(globalUp, forward);
        if (right.Length() == 0) // If parallel, use a fallback
        {
            globalUp = new Vec3(0, 0, 1);
            right = Vec3.Cross(globalUp, forward);
        }
        right = right.Normalize();

        // Compute the up vector
        Vec3 up = Vec3.Cross(forward, right).Normalize();

        // Create a rotation matrix
        Mat4x4 rotation = new(
        [
            [right.x, right.y, right.z, 0],
            [up.x, up.y, up.z, 0],
            [forward.x, forward.y, forward.z, 0],
            [0, 0, 0, 1]
        ]);

        // Create a translation matrix
        Mat4x4 translation = new(
        [
            [1, 0, 0, -Position.x],
            [0, 1, 0, -Position.y],
            [0, 0, 1, -Position.z],
            [0, 0, 0, 1]
        ]);

        // Combine rotation and translation
        Mat4x4 transform = rotation * translation;

        // Apply the transformation to the point
        Vec4 transformedPoint = transform * new Vec4(point, 1);

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
