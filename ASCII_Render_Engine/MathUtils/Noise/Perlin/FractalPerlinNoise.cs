using ASCII_Render_Engine.MathUtils.Vectors;

namespace ASCII_Render_Engine.MathUtils.Noise.Perlin;

public static class FractalPerlinNoise
{
    public static double FractalPerlin(Vec3 vector, double scale, int detail, double roughness)
    {
        double total = 0.0;
        double frequency = scale;
        double amplitude = 1.0;
        double maxValue = 0.0; // Used for normalization

        for (int i = 0; i < detail; i++)
        {
            total += PerlinNoise(vector * frequency) * amplitude;
            maxValue += amplitude;

            amplitude *= roughness;
            frequency *= 2.0;
        }

        return total / maxValue; // Normalize to range [0, 1]
    }

    private static double PerlinNoise(Vec3 point)
    {
        // Simple implementation of 3D Perlin Noise
        int xi = (int)Math.Floor(point.x) & 255;
        int yi = (int)Math.Floor(point.y) & 255;
        int zi = (int)Math.Floor(point.z) & 255;

        double xf = point.x - Math.Floor(point.x);
        double yf = point.y - Math.Floor(point.y);
        double zf = point.z - Math.Floor(point.z);

        double u = Fade(xf);
        double v = Fade(yf);
        double w = Fade(zf);

        int a = Permutation[xi] + yi;
        int aa = Permutation[a] + zi;
        int ab = Permutation[a + 1] + zi;
        int b = Permutation[xi + 1] + yi;
        int ba = Permutation[b] + zi;
        int bb = Permutation[b + 1] + zi;

        double x1, x2, y1, y2;
        x1 = Lerp(Grad(Permutation[aa], xf, yf, zf),
                  Grad(Permutation[ba], xf - 1, yf, zf), u);
        x2 = Lerp(Grad(Permutation[ab], xf, yf - 1, zf),
                  Grad(Permutation[bb], xf - 1, yf - 1, zf), u);
        y1 = Lerp(x1, x2, v);

        x1 = Lerp(Grad(Permutation[aa + 1], xf, yf, zf - 1),
                  Grad(Permutation[ba + 1], xf - 1, yf, zf - 1), u);
        x2 = Lerp(Grad(Permutation[ab + 1], xf, yf - 1, zf - 1),
                  Grad(Permutation[bb + 1], xf - 1, yf - 1, zf - 1), u);
        y2 = Lerp(x1, x2, v);

        return (Lerp(y1, y2, w) + 1) / 2; // Normalize to range [0, 1]
    }

    private static double Fade(double t) =>
        t * t * t * (t * (t * 6 - 15) + 10);

    private static double Lerp(double a, double b, double t) =>
        a + t * (b - a);

    private static double Grad(int hash, double x, double y, double z)
    {
        int h = hash & 15;
        double u = h < 8 ? x : y;
        double v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }

    private static readonly int[] Permutation = new int[512];

    static FractalPerlinNoise()
    {
        Random rand = new Random(42); // Seed for reproducibility
        int[] p = new int[256];
        for (int i = 0; i < 256; i++) p[i] = i;
        for (int i = 0; i < 256; i++)
        {
            int j = rand.Next(256);
            (p[i], p[j]) = (p[j], p[i]);
        }
        for (int i = 0; i < 512; i++) Permutation[i] = p[i % 256];
    }
}
