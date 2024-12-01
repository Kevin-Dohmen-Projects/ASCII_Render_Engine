using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.MathUtils.Matrixes.Transforms
{
    public static class Rotation
    {
        public static Mat3x3 RotateX(double angle)
        {
            return new(
                [
                    [1, 0, 0],
                    [0, Math.Cos(angle), -Math.Sin(angle)],
                    [0, Math.Sin(angle), Math.Cos(angle)]
                ]
            );
        }

        public static Mat3x3 RotateY(double angle)
        {
            return new(
                [
                    [Math.Cos(angle), 0, Math.Sin(angle)],
                    [0, 1, 0],
                    [-Math.Sin(angle), 0, Math.Cos(angle)]
                ]
            );
        }

        public static Mat3x3 RotateZ(double angle)
        {
            return new(
                [
                    [Math.Cos(angle), -Math.Sin(angle), 0],
                    [Math.Sin(angle), Math.Cos(angle), 0],
                    [0, 0, 1]
                ]
            );
        }

        public static Mat3x3 Rotate(double angleX, double angleY, double angleZ)
        {
            return RotateX(angleX) * RotateY(angleY) * RotateZ(angleZ);
        }

        public static Mat3x3 Rotate(Vec3 angles)
        {
            return Rotate(angles.x, angles.y, angles.z);
        }
    }
}
