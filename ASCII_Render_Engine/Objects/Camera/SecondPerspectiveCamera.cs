using ASCII_Render_Engine.MathUtils.Matrixes;
using ASCII_Render_Engine.MathUtils.Transform.Rotation;
using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.Objects.Camera;
public class SecondPerspectiveCamera : ICamera
{
    public Vec3 Position { get; set; }
    public IRotation Rotation { get; set; }
    public double FieldOfView { get; set; } // Field of view in degrees
    public double NearPlane { get; set; }
    public double FarPlane { get; set; }

    public SecondPerspectiveCamera(Vec3 position, IRotation direction, double fieldOfView, double nearPlane, double farPlane)
    {
        Position = position;
        Rotation = direction;
        FieldOfView = fieldOfView;
        NearPlane = nearPlane;
        FarPlane = farPlane;
    }

    public Vec3 PerspectiveTransform(Vec3 point, Vec2 screenResolution, double aspectRatio = 0)
    {
        // Initialize the projection matrix
        double fFar = FarPlane;
        double fNear = NearPlane;

        double fFov = FieldOfView;
        double fAspectRatio = aspectRatio != 0 ? aspectRatio : screenResolution.y / screenResolution.x;
        double fFovRad = 1.0 / Math.Tan(fFov * 0.5 / 180.0 * Math.PI);

        // Translate the point to the camera's local space
        point -= Position;

        // Rotate the point
        point = Rotation.RotateVector(point);

        // Create the perspective matrix
        secondMat4x4 perspectiveMatrix = new secondMat4x4();
        perspectiveMatrix.Matrix[0][0] = fAspectRatio * fFovRad;
        perspectiveMatrix.Matrix[1][1] = fFovRad;
        perspectiveMatrix.Matrix[2][2] = fFar / (fFar - fNear);
        perspectiveMatrix.Matrix[3][2] = (-fFar * fNear) / (fFar - fNear);
        perspectiveMatrix.Matrix[2][3] = 1.0;

        Vec4 point4 = new Vec4(point.x, point.y, point.z, 1);
        Vec4 transformed = secondMat4x4.Multiply(point4, perspectiveMatrix);

        if (transformed.w != 0)
        {
            transformed.x /= transformed.w;
            transformed.y /= transformed.w;
            transformed.z /= transformed.w;
        }

        Vec3 vec3 = new Vec3(transformed.x, transformed.y, transformed.z);
        vec3 += new Vec3(1, 1, 0);
        vec3.x *= 0.5 * screenResolution.x;
        vec3.y *= 0.5 * screenResolution.y;

        return vec3;
    }
}
