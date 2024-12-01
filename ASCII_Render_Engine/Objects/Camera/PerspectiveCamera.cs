using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.MathUtils.Matrixes;
using System;
using System.Diagnostics;

namespace ASCII_Render_Engine.Objects.Camera;

public class PerspectiveCamera3D : ICamera
{
    public Vec3 Position { get; set; }
    public Vec3 Direction { get; set; }
    public double FieldOfView; // Field of view in degrees
    public double NearPlane;
    public double FarPlane;

    public PerspectiveCamera3D(Vec3 position, Vec3 direction, double fieldOfView, double nearPlane, double farPlane)
    {
        Position = position;
        Direction = direction;
        FieldOfView = fieldOfView;
        NearPlane = nearPlane;
        FarPlane = farPlane;
    }

    public PerspectiveCamera3D()
    {
        Position = new Vec3(0, 0, 0);
        Direction = new Vec3(0, 0, 1);
        FieldOfView = 90; // Default FOV
        NearPlane = 0.1;
        FarPlane = 1000;
    }

    public PerspectiveCamera3D(PerspectiveCamera3D camera)
    {
        Position = new Vec3(camera.Position);
        Direction = new Vec3(camera.Direction);
        FieldOfView = camera.FieldOfView;
        NearPlane = camera.NearPlane;
        FarPlane = camera.FarPlane;
    }

    public Vec2 PerspectiveTransform(Vec3 point, Vec2 screenResolution, double aspectRatio = 0)
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
        Mat4x4 viewMatrix = rotation * translation;

        if (aspectRatio == 0)
        {
            aspectRatio = screenResolution.x / screenResolution.y;
        }

        // Compute perspective projection matrix
        double fovRad = Math.Tan(((Math.PI / 180) * FieldOfView) / 2);
        double zRange = FarPlane - NearPlane;

        Mat4x4 projectionMatrix = new(
        [
            [1 / (aspectRatio * fovRad), 0, 0, 0],
            [0, 1 / fovRad, 0, 0],
            [0, 0, -(FarPlane + NearPlane) / zRange, -(2 * FarPlane * NearPlane) / zRange],
            [0, 0, -1, 0]
        ]);

        // Apply the view and projection transformations
        Mat4x4 transform = projectionMatrix * viewMatrix;
        Vec4 transformedPoint = transform * new Vec4(point, 1);

        // Perform perspective divide
        if (transformedPoint.w == 0) throw new DivideByZeroException("Perspective divide failed (w == 0)");
        double x = transformedPoint.x / transformedPoint.w;
        double y = transformedPoint.y / transformedPoint.w;

        // Map to screen resolution (normalized to pixel coordinates)
        double screenX = (1 - x) * 0.5 * screenResolution.x; // Invert x-axis mapping
        double screenY = (1 - y) * 0.5 * screenResolution.y; // Map from [-1, 1] to [0, height] (y inverted for screen space)

        return new Vec2(screenX, screenY);
    }
}
