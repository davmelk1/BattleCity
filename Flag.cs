using SFML.Graphics;

namespace MyGame;

public class Flag : IHavingBounds
{
    private readonly Sprite flagSprite = new(Constants.texture)
    {
        TextureRect = Constants.flagRect,
        Scale = Constants.tankScale,
        Position = Constants.flagInitPosition
    };
    private bool status;

    public void Display(RenderWindow window)
    {
        window.Draw(flagSprite);
    }

    public FloatRect getGlobalBounds()
    {
        return flagSprite.GetGlobalBounds();
    }

    public void destroy()
    {
        flagSprite.TextureRect = Constants.flagDestroyedRect;
        status = true;
    }
    
    public bool isDestroyed()
    {
        return status;
    }
}