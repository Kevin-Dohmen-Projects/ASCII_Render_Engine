using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.Utils;

namespace ASCII_Render_Engine.MathUtils.Matrixes;

public struct Mat3x3
{
    public double[][] Matrix { get; set; }

    // pool
    private static readonly ObjectPool<Mat3x3> _pool = new ObjectPool<Mat3x3>(() => new Mat3x3(), m => m.Clear(), 100);

    public Mat3x3()
    {
        Matrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrix[i] = new double[3];
        }
    }

    public Mat3x3(Mat3x3 mat)
    {
        Matrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrix[i] = new double[3];
            for (int j = 0; j < 3; j++)
            {
                Matrix[i][j] = mat.Matrix[i][j];
            }
        }
    }

    public Mat3x3(double[][] matrix)
    {
        if (matrix.Length != 3 || matrix.Any(row => row.Length != 3))
        {
            throw new ArgumentException("Matrix must be 3x3.");
        }
        Matrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrix[i] = new double[3];
            for (int j = 0; j < 3; j++)
            {
                Matrix[i][j] = matrix[i][j];
            }
        }
    }

    public Mat3x3 Clear()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Matrix[i][j] = 0;
            }
        }
        return this;
    }

    public static Mat3x3 GetFromPool()
    {
        return _pool.GetObject();
    }

    public static void ReturnToPool(Mat3x3 mat)
    {
        _pool.ReturnObject(mat);
    }

    public static Mat3x3 Multiply(Mat3x3 a, Mat3x3 b)
    {
        Mat3x3 result = GetFromPool();
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

    public static Vec3 Multiply(Mat3x3 matrix, Vec3 vector)
    {
        Vec3 result = new Vec3(
            matrix.Matrix[0][0] * vector.x + matrix.Matrix[0][1] * vector.y + matrix.Matrix[0][2] * vector.z,
            matrix.Matrix[1][0] * vector.x + matrix.Matrix[1][1] * vector.y + matrix.Matrix[1][2] * vector.z,
            matrix.Matrix[2][0] * vector.x + matrix.Matrix[2][1] * vector.y + matrix.Matrix[2][2] * vector.z
        );
        return result;
    }

    public Mat3x3 MultiplyInPlace(Mat3x3 mat) // inefficient but might be useful (reference ping-pong)
    {
        Mat3x3 tmp = this * mat;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Matrix[i][j] = tmp.Matrix[i][j];
            }
        }
        ReturnToPool(tmp);
        return this;
    }

    public static Mat3x3 operator *(Mat3x3 a, Mat3x3 b)
    {
        return Multiply(a, b);
    }

    public static Vec3 operator *(Mat3x3 a, Vec3 b)
    {
        return Multiply(a, b);
    }
}
