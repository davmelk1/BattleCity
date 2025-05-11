using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class Bullet : IHavingBounds
{
    private Sprite bulletSprite;
    private Direction direction;
    public bool hit = false;

    public Bullet(float x, float y, Direction direction)
    {
        this.direction = direction;
        bulletSprite = new Sprite(Constants.texture)
            {
                TextureRect = Constants.bulletDirection2Rect[direction]
                ,Scale = Constants.bulletScale,
                Position = new Vector2f(x + Constants.tankSize/2 - Constants.bulletSize / 2, y + Constants.tankSize/2 - Constants.bulletSize / 2)
            };
    }

    public void Display(RenderWindow window)
    {
        window.Draw(bulletSprite);
    }

    public void Update()
    {
        if (direction == Direction.Left)
            bulletSprite.Position += new Vector2f(-Constants.bulletSpeed, 0);
        else if (direction == Direction.Right)
            bulletSprite.Position += new Vector2f(Constants.bulletSpeed, 0);
        else if (direction == Direction.Up)
            bulletSprite.Position += new Vector2f(0, -Constants.bulletSpeed);
        else
            bulletSprite.Position += new Vector2f(0, Constants.bulletSpeed);
    }

    public FloatRect getGlobalBounds()
    {
        if (!hit)
            return bulletSprite.GetGlobalBounds();
        
        float margin = 12;
        FloatRect bounds = bulletSprite.GetGlobalBounds();
        if (direction is Direction.Left or Direction.Right)
        {
            bounds.Top -= margin;
            bounds.Height += margin * 2;
        }
        else
        {
            bounds.Left -= margin;
            bounds.Width += margin * 2;
        }

        return bounds;
    }
}