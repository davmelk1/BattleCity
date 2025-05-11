using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Flag : IHavingBounds
{
    private Sprite flagSprite;
    bool status = false;
    public Flag()
    {
        flagSprite = new Sprite(Constants.texture)
        {
            TextureRect = Constants.flagRect,
            Scale = Constants.tankScale,
            Position = Constants.flagInitPosition
        };
    }
    
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