using ASCII_Render_Engine.MathUtils.Matrixes;
using ASCII_Render_Engine.MathUtils.Transform.Rotation;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.Objects.Camera;

public class PerspectiveCamera3D : ICamera
{
    public Vec3 Position { get; set; }
    public IRotation Rotation { get; set; }
    public double FieldOfView; // Field of view in degrees
    public double NearPlane;
    public double FarPlane;

    public PerspectiveCamera3D(Vec3 position, IRotation direction, double fieldOfView, double nearPlane, double farPlane)
    {
        Position = position;
        Rotation = direction;
        FieldOfView = fieldOfView;
        NearPlane = nearPlane;
        FarPlane = farPlane;
    }

    public PerspectiveCamera3D()
    {
        Position = new Vec3(0, 0, 0);
        Rotation = new QuaternionRotation(new Vec3(0, 0, 1), 0);
        FieldOfView = 90; // Default FOV
        NearPlane = 0.1;
        FarPlane = 1000;
    }

    public PerspectiveCamera3D(PerspectiveCamera3D camera)
    {
        Position = new Vec3(camera.Position);
        Rotation = camera.Rotation;
        FieldOfView = camera.FieldOfView;
        NearPlane = camera.NearPlane;
        FarPlane = camera.FarPlane;
    }

    public Vec3 TranslateToCameraSpace(Vec3 point) // Translate the point to the camera's world space
    {
        return Rotation.RotateVector(point - Position);
    }

    public Vec3 PerspectiveTransform(Vec3 point, Vec2 screenResolution, double aspectRatio = 0)
    {
        // Initialize the projection matrix
        double fFar = FarPlane;
        double fNear = NearPlane;

        double fFov = FieldOfView;
        double fAspectRatio = aspectRatio != 0 ? aspectRatio : screenResolution.y / screenResolution.x;
        double fFovRad = 1.0 / Math.Tan(fFov * 0.5 / 180.0 * Math.PI);

        // Create the perspective matrix
        Mat4x4 perspectiveMatrix = Mat4x4.GetFromPool();
        perspectiveMatrix.Matrix[0][0] = fAspectRatio * fFovRad;
        perspectiveMatrix.Matrix[1][1] = fFovRad;
        perspectiveMatrix.Matrix[2][2] = fFar / (fFar - fNear);
        perspectiveMatrix.Matrix[3][2] = (-fFar * fNear) / (fFar - fNear);
        perspectiveMatrix.Matrix[2][3] = 1.0;

        Vec4 point4 = new Vec4(point.x, point.y, point.z, 1);
        Vec4 transformed = perspectiveMatrix * point4;

        // Return the matrix to the pool
        Mat4x4.ReturnToPool(perspectiveMatrix);

        if (transformed.w != 0)
        {
            transformed.x /= transformed.w;
            transformed.y /= transformed.w;
            transformed.z /= transformed.w;
        }

        // If the point is behind the camera, ensure the z value remains negative
        if (point.z < 0)
        {
            transformed.z = -Math.Abs(transformed.z);
        }

        Vec3 vec3 = new Vec3(transformed.x, transformed.y, transformed.z);
        vec3 += new Vec3(1, 1, 0);
        vec3.x *= 0.5 * screenResolution.x;
        vec3.y *= 0.5 * screenResolution.y;

        return vec3;
    }
}
