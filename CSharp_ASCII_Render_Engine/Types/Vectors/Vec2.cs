namespace CSharp_ASCII_Render_Engine.Types.Vectors
{
    public class Vec2
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

        // In-place
        public Vec2 AddInPlace(Vec2 other)
        {
            x += other.x;
            y += other.y;
            return this;
        }
        public Vec2 AddInPlace(double other)
        {
            x += other;
            y += other;
            return this;
        }
        public Vec2 AddInPlace(double otherX, double otherY)
        {
            x += otherX;
            y += otherY;
            return this;
        }
        public Vec2 MultiplyInPlace(Vec2 other)
        {
            x *= other.x;
            y *= other.y;
            return this;
        }
        public Vec2 MultiplyInPlace(double scalar)
        {
            x *= scalar;
            y *= scalar;
            return this;
        }
        public Vec2 DivideInPlace(Vec2 other)
        {
            x /= other.x;
            y /= other.y;
            return this;
        }
        public Vec2 DivideInPlace(double scalar)
        {
            x /= scalar;
            y /= scalar;
            return this;
        }
        public Vec2 DivideInPlace(Vec2 left, Vec2 right)
        {
            this.x = left.x / right.x;
            this.y = left.y / right.y;
            return this;
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
        public Vec2 NormalizeInPlace()
        {
            double len = Length();
            x /= len;
            y /= len;
            return this;
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
