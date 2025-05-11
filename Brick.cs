using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Brick
{
    private SubBrick[]? subBricks;

    public Brick(float x, float y)
    {
        subBricks = new SubBrick[4];
        for (int k = 0; k < 4; k++)
            subBricks[k] = new SubBrick(x + Constants.smallBrickSize * (k % 2), y + Constants.smallBrickSize * (k / 2), true);
    }

    public void Display(RenderWindow window)
    {
        for (int k = 0; k < 4; k++)
            if (subBricks[k] != null)
                subBricks[k].Display(window);
    }

    public bool handleBullet(ref Bullet? bullet)
    {
        if (bullet == null)
            return false;
        bool hit = false;
        for (int k = 0; k < 4; k++)
            if (isTouching(bullet))
            {
                hit = true;
                subBricks[k] = null;
            }
        return hit;
    }

    public bool isTouching(Bullet? bullet)
    {
        if (bullet == null)
            return false;
        for (int k = 0; k < 4; k++)
        {
            if (subBricks[k] != null && Utilities.interacts(bullet, subBricks[k]))
                return true;
        }
        return false;
    }

    public FloatRect getInteractions(Tank tank)
    {
        for (int k = 0; k < 4; k++)
        {
            if (subBricks[k] != null && Utilities.interacts(subBricks[k], tank))
            {
                subBricks[k].getGlobalBounds().Intersects(tank.getGlobalBounds(), out FloatRect intersection);
                return intersection;
            }
        }
        return new FloatRect();
    }
}

public class SubBrick : IHavingBounds
{
    private Sprite brickSprite;
    

    public SubBrick(float x, float y, bool first)
    {
        brickSprite = new Sprite(Constants.texture)
        {
            TextureRect = Constants.brickTextureRect
            ,Scale = Constants.smallBrickScale,
            Position = new Vector2f(x, y) 
        };
    }
    
    public void Display(RenderWindow window)
    {
        window.Draw(brickSprite);
    }

    public FloatRect getGlobalBounds()
    {
        return brickSprite.GetGlobalBounds();
    }
}