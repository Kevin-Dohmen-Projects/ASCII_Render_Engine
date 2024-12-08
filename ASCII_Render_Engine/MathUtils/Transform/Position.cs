using ASCII_Render_Engine.Core;

namespace ASCII_Render_Engine.MathUtils.Transform;

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
                return RelFractionToPixelsInPlace(width, height);
            default:
                throw new NotImplementedException();
        }
    }

    public Position RelPercentageToPixelsInPlace(double screenWidth, double screenHeight)
    {
        x = screenWidth * (x / 100);
        y = screenHeight * (y / 100);
        Type = PositionType.Pixels;
        return this;
    }

    public Position RelFractionToPixelsInPlace(double screenWidth, double screenHeight)
    {
        x = screenWidth * x;
        y = screenHeight * y;
        Type = PositionType.Pixels;
        return this;
    }
}

public enum PositionType
{
    Pixels,
    RelPercentage,
    RelFraction
}
