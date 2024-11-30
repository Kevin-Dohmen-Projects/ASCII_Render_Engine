using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.MathUtils.Matrixes
{
    public class Mat4x4
    {
        double[][] Matrix { get; set; }

        public Mat4x4()
        {
            Matrix = new double[4][];
            for (int i = 0; i < 4; i++)
            {
                Matrix[i] = new double[4];
            }
        }

        public Mat4x4(double[][] matrix)
        {
            Matrix = matrix;
        }

        public static Mat4x4 operator *(Mat4x4 a, Mat4x4 b)
        {
            Mat4x4 result = new Mat4x4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result.Matrix[i][j] = a.Matrix[i][0] * b.Matrix[0][j] +
                                          a.Matrix[i][1] * b.Matrix[1][j] +
                                          a.Matrix[i][2] * b.Matrix[2][j] +
                                          a.Matrix[i][3] * b.Matrix[3][j];
                }
            }
            return result;
        }

        public static Mat4x4 operator *(Mat4x4 a, Vec4 b)
        {
            Mat4x4 result = new Mat4x4();
            for (int i = 0; i < 4; i++)
            {
                result.Matrix[i][0] = a.Matrix[i][0] * b.x;
                result.Matrix[i][1] = a.Matrix[i][1] * b.y;
                result.Matrix[i][2] = a.Matrix[i][2] * b.z;
                result.Matrix[i][3] = a.Matrix[i][3] * b.w;
            }
            return result;
        }

        public Vec4 Solve(Vec4 v)
        {
            double x = Matrix[0][0] * v.x + Matrix[0][1] * v.y + Matrix[0][2] * v.z + Matrix[0][3] * v.w;
            double y = Matrix[1][0] * v.x + Matrix[1][1] * v.y + Matrix[1][2] * v.z + Matrix[1][3] * v.w;
            double z = Matrix[2][0] * v.x + Matrix[2][1] * v.y + Matrix[2][2] * v.z + Matrix[2][3] * v.w;
            double w = Matrix[3][0] * v.x + Matrix[3][1] * v.y + Matrix[3][2] * v.z + Matrix[3][3] * v.w;

            return new Vec4(x, y, z, w);
        }

        public Vec4 Solve(Vec3 v, double wi)
        {
            double x = Matrix[0][0] * v.x + Matrix[0][1] * v.y + Matrix[0][2] * v.z + Matrix[0][3] * wi;
            double y = Matrix[1][0] * v.x + Matrix[1][1] * v.y + Matrix[1][2] * v.z + Matrix[1][3] * wi;
            double z = Matrix[2][0] * v.x + Matrix[2][1] * v.y + Matrix[2][2] * v.z + Matrix[2][3] * wi;
            double w = Matrix[3][0] * v.x + Matrix[3][1] * v.y + Matrix[3][2] * v.z + Matrix[3][3] * wi;

            return new Vec4(x, y, z, w);
        }
    }
}
