using ASCII_Render_Engine.MathUtils.Vectors;
using System;

namespace ASCII_Render_Engine.MathUtils.Transform.Rotation
{
    public struct QuaternionRotation : IRotation
    {
        public Vec4 Rotation { get; set; }

        public QuaternionRotation(Vec4 rotation)
        {
            Rotation = rotation.Normalize();
        }

        public QuaternionRotation(Vec3 rotation)
        {
            Rotation = new Vec4(rotation.x, rotation.y, rotation.z, 0).Normalize();
        }

        public QuaternionRotation(Vec3 axes, double angle)
        {
            double halfAngle = angle / 2;
            double sinHalfAngle = Math.Sin(halfAngle);
            Rotation = new Vec4(axes.x * sinHalfAngle, axes.y * sinHalfAngle, axes.z * sinHalfAngle, Math.Cos(halfAngle)).Normalize();
        }

        public Vec3 RotateVector(Vec3 vector)
        {
            Vec4 vectorQuaternion = new Vec4(vector.x, vector.y, vector.z, 0);
            Vec4 rotationConjugate = new Vec4(-Rotation.x, -Rotation.y, -Rotation.z, Rotation.w);
            Vec4 result = vectorMultiplication(vectorMultiplication(Rotation, vectorQuaternion), rotationConjugate);
            return new Vec3(result.x, result.y, result.z);
        }

        public Vec3 InverseRotateVector(Vec3 vector)
        {
            Vec4 vectorQuaternion = new Vec4(vector.x, vector.y, vector.z, 0);
            Vec4 rotationConjugate = new Vec4(-Rotation.x, -Rotation.y, -Rotation.z, Rotation.w);
            Vec4 result = vectorMultiplication(vectorMultiplication(rotationConjugate, vectorQuaternion), Rotation);
            return new Vec3(result.x, result.y, result.z);
        }

        public IRotation Inverse()
        {
            Vec4 conjugate = new Vec4(-Rotation.x, -Rotation.y, -Rotation.z, Rotation.w);
            double normSquared = Rotation.Length() * Rotation.Length();
            return new QuaternionRotation(conjugate / normSquared);
        }

        public IRotation Combine(IRotation rotation)
        {
            if (!(rotation is QuaternionRotation))
            {
                throw new ArgumentException("Rotation must be QuaternionRotation.");
            }
            QuaternionRotation rotationq = (QuaternionRotation)rotation;
            Vec4 result = Rotation * rotationq.Rotation;
            return new QuaternionRotation(result.Normalize());
        }

        public IRotation Clone()
        {
            return new QuaternionRotation(Rotation);
        }

        public QuaternionRotation Multiply(QuaternionRotation rotation)
        {
            Vec4 left = Rotation;
            Vec4 right = rotation.Rotation;
            Vec4 result = new Vec4(
                left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y,
                left.w * right.y - left.x * right.z + left.y * right.w + left.z * right.x,
                left.w * right.z + left.x * right.y - left.y * right.x + left.z * right.w,
                left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z
            );
            return new QuaternionRotation(result.Normalize());
        }

        public static Vec4 vectorMultiplication(Vec4 left, Vec4 right)
        {
            return new Vec4(
                left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y,
                left.w * right.y - left.x * right.z + left.y * right.w + left.z * right.x,
                left.w * right.z + left.x * right.y - left.y * right.x + left.z * right.w,
                left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z
            );
        }

        public static QuaternionRotation operator *(QuaternionRotation left, QuaternionRotation right)
        {
            return left.Multiply(right);
        }
    }
}
