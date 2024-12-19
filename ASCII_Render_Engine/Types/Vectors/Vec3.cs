namespace ASCII_Render_Engine.Types.Vectors;

public struct Vec3
{
    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }

    public Vec2 xy
    {
        get => new Vec2(x, y);
        set
        {
            x = value.x;
            y = value.y;
        }
    }
    public Vec2 xz
    {
        get => new Vec2(x, z);
        set
        {
            x = value.x;
            z = value.y;
        }
    }
    public Vec2 yz
    {
        get => new Vec2(y, z);
        set
        {
            y = value.x;
            z = value.y;
        }
    }


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
    public Vec3(Vec3 vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
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
        return len == 0 ?
            new Vec3() :
            new Vec3(
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

    public static Vec3 Cross(Vec3 left, Vec3 right)
    {
        return new Vec3
        (
            left.y * right.z - left.z * right.y,
            left.z * right.x - left.x * right.z,
            left.x * right.y - left.y * right.x
        );
    }

    public static double Angle(Vec3 left, Vec3 right)
    {
        double dotProduct = Dot(left, right);
        double magnitudeProduct = left.Length() * right.Length();
        return Math.Acos(dotProduct / magnitudeProduct);
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
