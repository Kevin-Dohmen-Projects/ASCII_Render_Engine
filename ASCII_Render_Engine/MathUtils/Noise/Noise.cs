using ASCII_Render_Engine.MathUtils.Vectors;
using ASCII_Render_Engine.MathUtils.Noise.Perlin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Render_Engine.MathUtils.Noise
{
    public static class Noise
    {
        public static double FractalPerlinNoise1D(Vec3 vector, double scale, int detail, double roughness)
        {
            return FractalPerlinNoise.FractalPerlin(vector, scale, detail, roughness);
        }
    }
}
