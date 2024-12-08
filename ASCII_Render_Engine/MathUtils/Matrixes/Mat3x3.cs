using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.MathUtils.Matrixes;

public class Mat3x3
{
    public double[][] Matrix { get; set; }
    private double[][] tmpMatrix;

    public Mat3x3()
    {
        Matrix = new double[3][];
        tmpMatrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrix[i] = new double[3];
            tmpMatrix[i] = new double[3];
        }
    }

    public Mat3x3(Mat3x3 mat)
    {
        Matrix = new double[3][];
        tmpMatrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            Matrix[i] = new double[3];
            tmpMatrix[i] = new double[3];
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
        Matrix = matrix;
        tmpMatrix = new double[3][];
        for (int i = 0; i < 3; i++)
        {
            tmpMatrix[i] = new double[3];
        }
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

    public Mat3x3 MultiplyInPlace(Mat3x3 b)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tmpMatrix[i][j] = Matrix[i][0] * b.Matrix[0][j] +
                                  Matrix[i][1] * b.Matrix[1][j] +
                                  Matrix[i][2] * b.Matrix[2][j];
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Matrix[i][j] = tmpMatrix[i][j];
            }
        }

        return this;
    }
}
