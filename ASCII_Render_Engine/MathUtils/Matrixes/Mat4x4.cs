using ASCII_Render_Engine.Types.Vectors;
using ASCII_Render_Engine.Utils;

namespace ASCII_Render_Engine.MathUtils.Matrixes;

public struct Mat4x4
{
    public double[][] Matrix { get; }

    // pool
    private static readonly ObjectPool<Mat4x4> _pool = new ObjectPool<Mat4x4>(() => new Mat4x4(), m => m.Clear(), 100);

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
        if (matrix.Length != 4 || matrix.Any(row => row.Length != 4))
        {
            throw new ArgumentException("Matrix must be 4x4.");
        }
        Matrix = matrix;
    }

    public Mat4x4 Clear()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Matrix[i][j] = 0;
            }
        }
        return this;
    }

    public static Mat4x4 Multiply(Mat4x4 a, Mat4x4 b)
    {
        Mat4x4 newMat = GetFromPool();
        double[][] result = newMat.Matrix;
        for (int i = 0; i < 4; i++)
        {
            result[i] = new double[4];
            for (int j = 0; j < 4; j++)
            {
                result[i][j] = a.Matrix[i][0] * b.Matrix[0][j] +
                              a.Matrix[i][1] * b.Matrix[1][j] +
                              a.Matrix[i][2] * b.Matrix[2][j] +
                              a.Matrix[i][3] * b.Matrix[3][j];
            }
        }
        return newMat;
    }

    public static Vec4 Multiply(in Mat4x4 matrix, in Vec4 vector)
    {
        double x = vector.x * matrix.Matrix[0][0] + vector.y * matrix.Matrix[1][0] + vector.z * matrix.Matrix[2][0] + vector.w * matrix.Matrix[3][0];
        double y = vector.x * matrix.Matrix[0][1] + vector.y * matrix.Matrix[1][1] + vector.z * matrix.Matrix[2][1] + vector.w * matrix.Matrix[3][1];
        double z = vector.x * matrix.Matrix[0][2] + vector.y * matrix.Matrix[1][2] + vector.z * matrix.Matrix[2][2] + vector.w * matrix.Matrix[3][2];
        double w = vector.x * matrix.Matrix[0][3] + vector.y * matrix.Matrix[1][3] + vector.z * matrix.Matrix[2][3] + vector.w * matrix.Matrix[3][3];
        return new Vec4(x, y, z, w);
    }

    public static Mat4x4 operator *(in Mat4x4 a, in Mat4x4 b)
    {
        return Multiply(a, b);
    }

    public static Vec4 operator *(in Mat4x4 a, in Vec4 b)
    {
        return Multiply(a, b);
    }

    public static Mat4x4 GetFromPool()
    {
        return _pool.GetObject();
    }

    public static void ReturnToPool(Mat4x4 matrix)
    {
        matrix.Clear();
        _pool.ReturnObject(matrix);
    }
}
