namespace MyGame;
using SFML.Graphics;

public class Border
{
    private readonly RectangleShape 
        topBorder = new(Constants.borderUp)
    {
        Position = Constants.borderUpPosition, FillColor = Constants.borderColor
    }, bottomBorder = new(Constants.borderDown)
    {
        Position = Constants.borderDownPosition, FillColor = Constants.borderColor
    }, leftBorder = new(Constants.borderLeft)
    {
        Position = Constants.borderLeftPosition, FillColor = Constants.borderColor
    }, rightBorder = new(Constants.borderRight)
    {
        Position = Constants.borderRightPosition, FillColor = Constants.borderColor
    };

    private readonly RectangleShape[] borders;

    public Border()
    {
        borders = [topBorder, bottomBorder, leftBorder, rightBorder];
    }

    public void Display(RenderWindow window)
    {
        foreach (var border in borders)
            window.Draw(border);
    }

    public FloatRect? interacts(FloatRect bounds)
    {
        foreach (var border in borders)
            if (border.GetGlobalBounds().Intersects(bounds))
                return border.GetGlobalBounds();
        return null;
    }
}