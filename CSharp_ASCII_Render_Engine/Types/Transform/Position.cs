using CSharp_ASCII_Render_Engine.ScreenRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_ASCII_Render_Engine.Types.Transform
{
    public class Position
    {
        public double x;
        public double y;
        public PositionType Type;

        public Position()
        {
            x = 0;
            y = 0;
            Type = PositionType.Pixels;
        }

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
            Type = PositionType.Pixels;
        }

        public Position(double x, double y, PositionType type)
        {
            this.x = x;
            this.y = y;
            Type = type;
        }

        public Position ToPixelsInPlace(Screen screen)
        {
            double width = screen.Width;
            double height = screen.Height;

            switch (Type)
            {
                case PositionType.Pixels:
                    return this;
                case PositionType.RelPercentage:
                    return RelPercentageToPixelsInPlace(width, height);
                case PositionType.RelFraction:
                    return NaN;
                default:
                    throw new NotImplementedException();
            }
        }

        public Position RelPercentageToPixelsInPlace(double screenWidth, double screenHeight)
        {
            this.x = screenWidth * (x / 100);
            this.y = screenHeight * (y / 100);
            this.Type = PositionType.Pixels;
            return this;
        }

        public Position RelFractionToPixelsInPlace(double screenWidth, double screenHeight)
        {
            this.x = screenWidth * x;
            this.y = screenHeight * y;
            this.Type = PositionType.Pixels;
            return this;
        }
    }

    public enum PositionType
    {
        Pixels,
        RelPercentage,
        RelFraction
    }
}
