using System.Runtime.CompilerServices;

namespace ASCII_Render_Engine.Types.Vectors
{
    
    public struct Vec2
    {
        public double x, y;

        public Vec2(double fx, double fy)
        {
            x = fx;
            y = fy;
        }
        public Vec2(double f)
        {
            x = f;
            y = f;
        }
        public Vec2()
        {
            x = 0;
            y = 0;
        }

        public Vec2 Reset()
        {
            x = 0;
            y = 0;
            return this;
        }

        // -=-=-=-=- math -=-=-=-=-
        // operators
        // vec-vec
        public static Vec2 operator +(Vec2 left, Vec2 right)
        {
            return new Vec2(
                left.x + right.x,
                left.y + right.y
                );
        }
        public static Vec2 operator -(Vec2 left, Vec2 right)
        {
            return new Vec2(
                left.x - right.x,
                left.y - right.y
                );
        }
        public static Vec2 operator *(Vec2 left, Vec2 right)
        {
            return new Vec2(
                left.x * right.x,
                left.y * right.y
                );
        }
        public static Vec2 operator /(Vec2 left, Vec2 right)
        {
            return new Vec2(
                left.x / right.x,
                left.y / right.y
                );
        }
        public static Vec2 operator %(Vec2 left, Vec2 right)
        {
            return new Vec2(
                left.x % right.x,
                left.y % right.y
                );
        }
        // vec-double
        public static Vec2 operator +(Vec2 left, double right)
        {
            return left + new Vec2(right);
        }
        public static Vec2 operator -(Vec2 left, double right)
        {
            return left - new Vec2(right);
        }
        public static Vec2 operator *(Vec2 left, double right)
        {
            return left * new Vec2(right);
        }
        public static Vec2 operator /(Vec2 left, double right)
        {
            return left / new Vec2(right);
        }
        public static Vec2 operator %(Vec2 left, double right)
        {
            return left % new Vec2(right);
        }
        // double-vec
        public static Vec2 operator +(double left, Vec2 right)
        {
            return new Vec2(left) + right;
        }
        public static Vec2 operator -(double left, Vec2 right)
        {
            return new Vec2(left) - right;
        }
        public static Vec2 operator *(double left, Vec2 right)
        {
            return new Vec2(left) * right;
        }
        public static Vec2 operator /(double left, Vec2 right)
        {
            return new Vec2(left) / right;
        }
        public static Vec2 operator %(double left, Vec2 right)
        {
            return new Vec2(left) % right;
        }

        // functions
        public double Length()
        {
            return Math.Sqrt(
                x * x
                + y * y
                );
        }
        public Vec2 Normalize()
        {
            double len = Length();
            return new Vec2(
                x / len,
                y / len
                );
        }
        public static double Dot(Vec2 left, Vec2 right)
        {
            return left.x * right.x
                + left.y * right.y;
        }
        public static double Cross(Vec2 left, Vec2 right)
        {
            return left.x * right.y
                - left.y * right.x;
        }

        // -=-=-=-=- conversion and parse -=-=-=-=-
        public string Stringify()
        {
            return string.Format(
                "({0}, {1})",
                x, y
                );
        }
        public override string ToString()
        {
            return Stringify();
        }

        public static Vec2 Parse(string s)
        {
            var sArr = s
                .Replace("(", "")
                .Replace(")", "")
                .Replace('.', ',')
                .Split(", ");
            return new Vec2(double.Parse(sArr[0]), double.Parse(sArr[1]));
        }
    }
}
