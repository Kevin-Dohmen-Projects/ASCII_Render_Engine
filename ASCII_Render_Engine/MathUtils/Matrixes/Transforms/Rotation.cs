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
        private static Mat3x3 mat3X3Tempx = new();
        private static Mat3x3 mat3X3Tempy = new();
        private static Mat3x3 mat3X3Tempz = new();

        public static Mat3x3 RotateX(double angle)
        {
            mat3X3Tempx.Matrix = new double[][]
            {
                    new double[] { 1, 0, 0 },
                    new double[] { 0, Math.Cos(angle), -Math.Sin(angle) },
                    new double[] { 0, Math.Sin(angle), Math.Cos(angle) }
            };
            return mat3X3Tempx;
        }

        public static Mat3x3 RotateY(double angle)
        {
            mat3X3Tempy.Matrix = new double[][]
            {
                    new double[] { Math.Cos(angle), 0, Math.Sin(angle) },
                    new double[] { 0, 1, 0 },
                    new double[] { -Math.Sin(angle), 0, Math.Cos(angle) }
            };
            return mat3X3Tempy;
        }

        public static Mat3x3 RotateZ(double angle)
        {
            mat3X3Tempz.Matrix = new double[][]
            {
                    new double[] { Math.Cos(angle), -Math.Sin(angle), 0 },
                    new double[] { Math.Sin(angle), Math.Cos(angle), 0 },
                    new double[] { 0, 0, 1 }
            };
            return mat3X3Tempz;
        }

        public static Mat3x3 Rotate(double angleX, double angleY, double angleZ)
        {
            return RotateX(angleX).MultiplyInPlace(RotateY(angleY)).MultiplyInPlace(RotateZ(angleZ));
        }

        public static Mat3x3 Rotate(Vec3 angles)
        {
            return Rotate(angles.x, angles.y, angles.z);
        }
    }
}
