namespace CSharp_ASCII_Render_Engine.Types.Vectors
{
    public class Vec4
    {
        public double x, y, z, w;

        public Vec4(double fx, double fy, double fz, double fw)
        {
            x = fx;
            y = fy;
            z = fz;
            w = fw;
        }
        public Vec4(Vec2 v1, double fz, double fw)
        {
            x = v1.x;
            y = v1.y;
            z = fz;
            w = fw;
        }
        public Vec4(Vec2 v1, Vec2 v2)
        {
            x = v1.x;
            y = v1.y;
            z = v2.x;
            w = v2.y;
        }
        public Vec4(Vec3 v1, double fw)
        {
            x = v1.x;
            y = v1.y;
            z = v1.z;
            w = fw;
        }
        public Vec4(double f)
        {
            x = f;
            y = f;
            z = f;
            w = f;
        }
        public Vec4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }


        // -=-=-=-=- math -=-=-=-=-
        // operators
        // vec-vec
        public static Vec4 operator +(Vec4 left, Vec4 right)
        {
            return new Vec4(
                left.x + right.x,
                left.y + right.y,
                left.z + right.z,
                left.w + right.w
                );
        }
        public static Vec4 operator -(Vec4 left, Vec4 right)
        {
            return new Vec4(
                left.x - right.x,
                left.y - right.y,
                left.z - right.z,
                left.w - right.w
                );
        }
        public static Vec4 operator *(Vec4 left, Vec4 right)
        {
            return new Vec4(
                left.x * right.x,
                left.y * right.y,
                left.z * right.z,
                left.w * right.w
                );
        }
        public static Vec4 operator /(Vec4 left, Vec4 right)
        {
            return new Vec4(
                left.x / right.x,
                left.y / right.y,
                left.z / right.z,
                left.w / right.w
                );
        }
        public static Vec4 operator %(Vec4 left, Vec4 right)
        {
            return new Vec4(left.x % right.x,
                left.y % right.y,
                left.z % right.y,
                left.w % right.w
                );
        }
        // vec-double
        public static Vec4 operator +(Vec4 left, double right)
        {
            return left + new Vec4(right);
        }
        public static Vec4 operator -(Vec4 left, double right)
        {
            return left - new Vec4(right);
        }
        public static Vec4 operator *(Vec4 left, double right)
        {
            return left * new Vec4(right);
        }
        public static Vec4 operator /(Vec4 left, double right)
        {
            return left / new Vec4(right);
        }
        public static Vec4 operator %(Vec4 left, double right)
        {
            return left % new Vec4(right);
        }
        // double-vec
        public static Vec4 operator +(double left, Vec4 right)
        {
            return new Vec4(left) + right;
        }
        public static Vec4 operator -(double left, Vec4 right)
        {
            return new Vec4(left) - right;
        }
        public static Vec4 operator *(double left, Vec4 right)
        {
            return new Vec4(left) * right;
        }
        public static Vec4 operator /(double left, Vec4 right)
        {
            return new Vec4(left) / right;
        }
        public static Vec4 operator %(double left, Vec4 right)
        {
            return new Vec4(left) % right;
        }

        // functions
        public double Length()
        {
            double len2d = Math.Sqrt(
                x * x
                + y * y
                );
            double len3d = Math.Sqrt(
                len2d * len2d
                + z * z
                );
            return Math.Sqrt(
                len3d * len3d
                + w * w
                );
        }
        public Vec4 Normalize()
        {
            double len = Length();
            return new Vec4(
                x / len,
                y / len,
                z / len,
                w / len
                );
        }
        public static double Dot(Vec4 left, Vec4 right)
        {
            return left.x * right.x
                + left.y * right.y
                + left.z * right.z
                + left.w * right.w;
        }


        // -=-=-=-=- conversion and parse -=-=-=-=-
        public string Stringify()
        {
            return string.Format(
                "({0}, {1}, {2}, {3})",
                x, y, z, w
                );
        }
        public override string ToString()
        {
            return Stringify();
        }
        public static Vec4 Parse(string s)
        {
            var sArr = s
                .Replace("(", "")
                .Replace(")", "")
                .Replace('.', ',')
                .Split(", ");
            return new Vec4(
                double.Parse(sArr[0]),
                double.Parse(sArr[1]),
                double.Parse(sArr[2]),
                double.Parse(sArr[3]));
        }
    }
}
