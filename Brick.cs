using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Brick
{
    private readonly SubBrick?[] subBricks;

    public Brick(float x, float y)
    {
        subBricks = new SubBrick[4];
        for (int k = 0; k < 4; k++)
            subBricks[k] = new SubBrick(x + Constants.smallBrickSize * (k % 2), y + Constants.smallBrickSize * (int)(k / 2), k % 2 == 0);
    }

    public void Display(RenderWindow window)
    {
        for (int k = 0; k < 4; k++)
            subBricks[k]?.Display(window);
    }

    public bool handleBullet(ref Bullet? bullet)
    {
        if (bullet == null)
            return false;
        bool hit = false;
        for (int k = 0; k < 4; k++)
        {
            if (isTouching(bullet))
            {
                bullet.Hit = true;
                break;
            }
        }
        for (int k = 0; k < 4; k++)
            if (subBricks[k] != null && Utilities.interacts(bullet, subBricks[k]!))
            {
                hit = true;
                subBricks[k] = null;
            }
        return hit;
    }

    public void destroy()
    {
        for (int k = 0; k < 4; k++)
            subBricks[k] = null;
    }

    public bool isTouching(Bullet? bullet)
    {
        if (bullet == null)
            return false;
        for (int k = 0; k < 4; k++)
        {
            if (subBricks[k] != null && Utilities.interacts(bullet, subBricks[k]!))
                return true;
        }
        return false;
    }

    public FloatRect getInteractions(Tank tank)
    {
        for (int k = 0; k < 4; k++)
        {
            if (subBricks[k] != null && Utilities.interacts(subBricks[k]!, tank))
            {
                subBricks[k]!.getGlobalBounds().Intersects(tank.getGlobalBounds(), out FloatRect intersection);
                return intersection;
            }
        }
        return new FloatRect();
    }
}

public class SubBrick : IHavingBounds
{
    private readonly Sprite brickSprite;
    

    public SubBrick(float x, float y, bool first)
    {
        if (first)
            brickSprite = new Sprite(Constants.texture)
            {
                TextureRect = Constants.brickTextureRect
                ,Scale = Constants.smallBrickScale,
                Position = new Vector2f(x, y) 
            };
        else
            brickSprite = new Sprite(Constants.texture)
            {
                TextureRect = Constants.brickTextureRect2
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