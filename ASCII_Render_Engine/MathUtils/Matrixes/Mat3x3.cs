using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.MathUtils.Matrixes;

public class Mat3x3
{
    double[][] Matrix { get; set; }

    public Mat3x3()
    {
        Matrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrix[i] = new double[3];
        }
    }

    public Mat3x3(double[][] matrix)
    {
        if (matrix.Length != 3 || matrix.Any(row => row.Length != 3))
        {
            throw new ArgumentException("Matrix must be 3x3.");
        }
        Matrix = matrix;
    }

    public static Mat3x3 operator *(Mat3x3 a, Mat3x3 b)
    {
        Mat3x3 result = new Mat3x3();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                result.Matrix[i][j] = a.Matrix[i][0] * b.Matrix[0][j] +
                                      a.Matrix[i][1] * b.Matrix[1][j] +
                                      a.Matrix[i][2] * b.Matrix[2][j];
            }
        }
        return result;
    }

    public static Vec3 operator *(Mat3x3 a, Vec3 b)
    {
        Vec3 result = new Vec3(
            a.Matrix[0][0] * b.x + a.Matrix[0][1] * b.y + a.Matrix[0][2] * b.z,
            a.Matrix[1][0] * b.x + a.Matrix[1][1] * b.y + a.Matrix[1][2] * b.z,
            a.Matrix[2][0] * b.x + a.Matrix[2][1] * b.y + a.Matrix[2][2] * b.z
        );
        return result;
    }
}
