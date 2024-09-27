using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Types.Vectors
{
        public class Vec1
        {
            public double x;

            public Vec1(double f)
            {
                x = f;
            }
            public Vec1()
            {
                x = 0;
            }

            // -=-=-=-=- math -=-=-=-=-
            // operators
            // vec-vec
            public static Vec1 operator +(Vec1 left, Vec1 right)
            {
                return new Vec1(
                    left.x + right.x
                    );
            }
            public static Vec1 operator -(Vec1 left, Vec1 right)
            {
                return new Vec1(
                    left.x - right.x
                    );
            }
            public static Vec1 operator *(Vec1 left, Vec1 right)
            {
                return new Vec1(
                    left.x * right.x
                    );
            }
            public static Vec1 operator /(Vec1 left, Vec1 right)
            {
                return new Vec1(
                    left.x / right.x
                    );
            }
            public static Vec1 operator %(Vec1 left, Vec1 right)
            {
                return new Vec1(
                    left.x % right.x
                    );
            }
            // vec-double
            public static Vec1 operator +(Vec1 left, double right)
            {
                return left + new Vec1(right);
            }
            public static Vec1 operator -(Vec1 left, double right)
            {
                return left - new Vec1(right);
            }
            public static Vec1 operator *(Vec1 left, double right)
            {
                return left * new Vec1(right);
            }
            public static Vec1 operator /(Vec1 left, double right)
            {
                return left / new Vec1(right);
            }
            public static Vec1 operator %(Vec1 left, double right)
            {
                return left % new Vec1(right);
            }
            // double-vec
            public static Vec1 operator +(double left, Vec1 right)
            {
                return new Vec1(left) + right;
            }
            public static Vec1 operator -(double left, Vec1 right)
            {
                return new Vec1(left) - right;
            }
            public static Vec1 operator *(double left, Vec1 right)
            {
                return new Vec1(left) * right;
            }
            public static Vec1 operator /(double left, Vec1 right)
            {
                return new Vec1(left) / right;
            }
            public static Vec1 operator %(double left, Vec1 right)
            {
                return new Vec1(left) % right;
            }

            // functions
            public double Length()
            {
                return x;
            }
            public Vec1 Normalize()
            {
                return new Vec1(1);
            }


            // -=-=-=-=- conversion and parse -=-=-=-=-
            public string Stringify()
            {
                return string.Format(
                    "({0})",
                    x
                    );
            }
            public override string ToString()
            {
                return Stringify();
            }

            public static Vec1 Parse(string s)
            {
                var sArr = s
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace('.', ',')
                    .Split(", ");
                return new Vec1(double.Parse(sArr[0]));
            }
        }
    }
