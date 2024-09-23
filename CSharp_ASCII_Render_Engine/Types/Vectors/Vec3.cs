using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Types.Vectors
{
    public class Vec3
    {
        public double x, y, z;

        public Vec3(double fx, double fy, double fz)
        {
            x = fx;
            y = fy;
            z = fz;
        }
        public Vec3(Vec2 v2, double fz)
        {
            x = v2.x;
            y = v2.y;
            z = fz;
        }
        public Vec3(double f)
        {
            x = f;
            y = f;
            z = f;
        }
        public Vec3()
        {
            x = 0;
            y = 0;
            z = 0;
        }


        // -=-=-=-=- math -=-=-=-=-
        // operators
        // vec-vec
        public static Vec3 operator +(Vec3 left, Vec3 right)
        {
            return new Vec3(
                left.x + right.x,
                left.y + right.y,
                left.z + right.z
                );
        }
        public static Vec3 operator -(Vec3 left, Vec3 right)
        {
            return new Vec3(
                left.x - right.x,
                left.y - right.y,
                left.z - right.z
                );
        }
        public static Vec3 operator *(Vec3 left, Vec3 right)
        {
            return new Vec3(
                left.x * right.x,
                left.y * right.y,
                left.z * right.z
                );
        }
        public static Vec3 operator /(Vec3 left, Vec3 right)
        {
            return new Vec3(
                left.x / right.x,
                left.y / right.y,
                left.z / right.z
                );
        }
        public static Vec3 operator %(Vec3 left, Vec3 right)
        {
            return new Vec3(
                left.x % right.x,
                left.y % right.y,
                left.z % right.z
                );
        }
        // vec-double
        public static Vec3 operator +(Vec3 left, double right)
        {
            return left + new Vec3(right);
        }
        public static Vec3 operator -(Vec3 left, double right)
        {
            return left - new Vec3(right);
        }
        public static Vec3 operator *(Vec3 left, double right)
        {
            return left * new Vec3(right);
        }
        public static Vec3 operator /(Vec3 left, double right)
        {
            return left / new Vec3(right);
        }
        public static Vec3 operator %(Vec3 left, double right)
        {
            return left % new Vec3(right);
        }
        // double-vec
        public static Vec3 operator +(double left, Vec3 right)
        {
            return new Vec3(left) + right;
        }
        public static Vec3 operator -(double left, Vec3 right)
        {
            return new Vec3(left) - right;
        }
        public static Vec3 operator *(double left, Vec3 right)
        {
            return new Vec3(left) * right;
        }
        public static Vec3 operator /(double left, Vec3 right)
        {
            return new Vec3(left) / right;
        }
        public static Vec3 operator %(double left, Vec3 right)
        {
            return new Vec3(left) % right;
        }

        // functions
        public double Length()
        {
            double len2d = Math.Sqrt(
                x * x
                + y * y
                );
            return Math.Sqrt(
                len2d * len2d
                + z * z
                );
        }
        public Vec3 Normalize()
        {
            double len = Length();
            return new Vec3(
                x / len,
                y / len,
                z / len
                );
        }
        public static double Dot(Vec3 left, Vec3 right)
        {
            return left.x * right.x
                + left.y * right.y
                + left.z * right.z;
        }


        // -=-=-=-=- conversion and parse -=-=-=-=-
        public string Stringify()
        {
            return string.Format(
                "({0}, {1}, {2})",
                x, y, z
                );
        }
        public override string ToString()
        {
            return Stringify();
        }
        public static Vec3 Parse(string s)
        {
            var sArr = s
                .Replace("(", "")
                .Replace(")", "")
                .Replace('.', ',')
                .Split(", ");
            return new Vec3(
                double.Parse(sArr[0]),
                double.Parse(sArr[1]),
                double.Parse(sArr[2])
                );
        }
    }
}
