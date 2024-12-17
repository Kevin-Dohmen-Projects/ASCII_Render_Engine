using ASCII_Render_Engine.MathUtils.Matrixes;
using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Utils;
using System.Reflection.Metadata.Ecma335;

namespace ASCII_Render_Engine.MathUtils.Transform.Rotation;
public struct EulerRotation : IRotation
{
    public Vec3 Rotation { get; set; }

    public EulerRotation(Vec3 rotation)
    {
        Rotation = rotation;
    }

    public Vec3 RotateVector(Vec3 vector)
    {
        // init rotation matrixes (from pool)
        Mat4x4 rotationMatrixX = Mat4x4.GetFromPool();
        rotationMatrixX.Matrix[0][0] = 1;
        rotationMatrixX.Matrix[1][1] = Math.Cos(Rotation.x);
        rotationMatrixX.Matrix[1][2] = Math.Sin(Rotation.x);
        rotationMatrixX.Matrix[2][1] = -Math.Sin(Rotation.x);
        rotationMatrixX.Matrix[2][2] = Math.Cos(Rotation.x);
        rotationMatrixX.Matrix[3][3] = 1;

        Mat4x4 rotationMatrixY = Mat4x4.GetFromPool();
        rotationMatrixY.Matrix[0][0] = Math.Cos(Rotation.y);
        rotationMatrixY.Matrix[0][2] = Math.Sin(Rotation.y);
        rotationMatrixY.Matrix[2][0] = -Math.Sin(Rotation.y);
        rotationMatrixY.Matrix[1][1] = 1;
        rotationMatrixY.Matrix[2][2] = Math.Cos(Rotation.y);
        rotationMatrixY.Matrix[3][3] = 1;

        Mat4x4 rotationMatrixZ = Mat4x4.GetFromPool();
        rotationMatrixZ.Matrix[0][0] = Math.Cos(Rotation.z);
        rotationMatrixZ.Matrix[0][1] = Math.Sin(Rotation.z);
        rotationMatrixZ.Matrix[1][0] = -Math.Sin(Rotation.z);
        rotationMatrixZ.Matrix[1][1] = Math.Cos(Rotation.z);
        rotationMatrixZ.Matrix[2][2] = 1;
        rotationMatrixZ.Matrix[3][3] = 1;

        Mat4x4 rotationMatrix = rotationMatrixX * rotationMatrixY * rotationMatrixZ;

        Vec4 result = rotationMatrix * new Vec4(vector.x, vector.y, vector.z, 1);

        // return to respective pools
        Mat4x4.ReturnToPool(rotationMatrixX);
        Mat4x4.ReturnToPool(rotationMatrixY);
        Mat4x4.ReturnToPool(rotationMatrixZ);
        Mat4x4.ReturnToPool(rotationMatrix);


        return new Vec3(result.x, result.y, result.z);
    }

    public Vec3 InverseRotateVector(Vec3 vector)
    {
        EulerRotation tmp = new EulerRotation(0 - Rotation);
        return tmp.RotateVector(vector);
    }

    public IRotation Clone()
    {
        return new EulerRotation(Rotation);
    }

    public IRotation Combine(IRotation rotation)
    {
        if (rotation is EulerRotation eulerRotation)
        {
            return new EulerRotation(Rotation + eulerRotation.Rotation);
        }
        throw new ArgumentException("Rotation must be of type EulerRotation");
    }

    public IRotation Inverse()
    {
        return new EulerRotation(0 - Rotation);
    }
}
