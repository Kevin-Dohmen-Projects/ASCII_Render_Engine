using ASCII_Render_Engine.MathUtils.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.MathUtils.Matrixes;

public class Mat4x4
{
    double[][] Matrix { get; set; }
    private double[][] tmpMatrix;

    public Mat4x4()
    {
        Matrix = new double[4][];
        tmpMatrix = new double[4][];
        for (int i = 0; i < 4; i++)
        {
            Matrix[i] = new double[4];
            tmpMatrix[i] = new double[4];
        }
    }

    public Mat4x4(Mat4x4 mat)
    {
        Matrix = new double[4][];
        tmpMatrix = new double[4][];
        for (int i = 0; i < 4; i++)
        {
            Matrix[i] = new double[4];
            tmpMatrix[i] = new double[4];
            for (int j = 0; j < 4; j++)
            {
                Matrix[i][j] = mat.Matrix[i][j];

            }
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

    public static Vec4 operator *(Mat4x4 a, Vec4 b)
    {
        Vec4 result = new Vec4(
            a.Matrix[0][0] * b.x + a.Matrix[0][1] * b.y + a.Matrix[0][2] * b.z + a.Matrix[0][3] * b.w,
            a.Matrix[1][0] * b.x + a.Matrix[1][1] * b.y + a.Matrix[1][2] * b.z + a.Matrix[1][3] * b.w,
            a.Matrix[2][0] * b.x + a.Matrix[2][1] * b.y + a.Matrix[2][2] * b.z + a.Matrix[2][3] * b.w,
            a.Matrix[3][0] * b.x + a.Matrix[3][1] * b.y + a.Matrix[3][2] * b.z + a.Matrix[3][3] * b.w
            );
        return result;
    }

    public Mat4x4 MultiplyInPlace(Mat4x4 b)
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                tmpMatrix[i][j] = Matrix[i][0] * b.Matrix[0][j] +
                                  Matrix[i][1] * b.Matrix[1][j] +
                                  Matrix[i][2] * b.Matrix[2][j] +
                                  Matrix[i][3] * b.Matrix[3][j];
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Matrix[i][j] = tmpMatrix[i][j];
            }
        }
        return this;
    }
}
