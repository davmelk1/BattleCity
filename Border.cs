namespace MyGame;
using SFML.Graphics;

public class Border
{
    private RectangleShape topBorder, bottomBorder, leftBorder, rightBorder;
    public Border()
    {
        topBorder = new RectangleShape(Constants.borderUp) { Position = Constants.borderUpPosition, FillColor = Constants.borderColor };
        bottomBorder = new RectangleShape(Constants.borderDown) { Position = Constants.borderDownPosition, FillColor = Constants.borderColor };
        leftBorder = new RectangleShape(Constants.borderLeft) { Position = Constants.borderLeftPosition, FillColor = Constants.borderColor };
        rightBorder = new RectangleShape(Constants.borderRight) { Position = Constants.borderRightPosition, FillColor = Constants.borderColor };
    }

    public void Display(RenderWindow window)
    {
        window.Draw(topBorder);
        window.Draw(bottomBorder);
        window.Draw(leftBorder);
        window.Draw(rightBorder);
    }

    public FloatRect? interacts(FloatRect bounds)
    {
        if (bounds.Intersects(topBorder.GetGlobalBounds()))
            return topBorder.GetGlobalBounds();
        if (bounds.Intersects(bottomBorder.GetGlobalBounds()))
            return bottomBorder.GetGlobalBounds();
        if (bounds.Intersects(leftBorder.GetGlobalBounds()))
            return leftBorder.GetGlobalBounds();
        if (bounds.Intersects(rightBorder.GetGlobalBounds()))
            return rightBorder.GetGlobalBounds();
        return null;
    }
}