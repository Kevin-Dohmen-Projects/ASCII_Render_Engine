using ASCII_Render_Engine.MathUtils.Noise.Perlin;
using ASCII_Render_Engine.Types.Vectors;

namespace ASCII_Render_Engine.MathUtils.Noise;

public static class Noise
{
    public static double FractalPerlinNoise1D(Vec3 vector, double scale, int detail, double roughness)
    {
        return FractalPerlinNoise.FractalPerlin(vector, scale, detail, roughness);
    }
}
