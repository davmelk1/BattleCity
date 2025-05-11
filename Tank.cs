namespace MyGame;
using System;
using SFML.Graphics;
using SFML.System;
public abstract class Tank : IHavingBounds
{
    protected Sprite tankSprite;
    protected Direction direction;
    protected Bullet?[] bullets;
    protected uint fireTimer;
    public Direction Direction => direction;

    protected Tank()
    {
        bullets = new Bullet?[Constants.maxBulletCount];
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            bullets[i] = null;
        }
        
    }

    protected void Fire()
    {
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            if (bullets[i] == null)
            {
                bullets[i] = new Bullet(tankSprite.Position.X, tankSprite.Position.Y, direction);
                break;
            }
        }
    }
    
    public bool handleBulletInteractions(Brick[]? bricks, Hero? hero = null)
    {
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            if (bullets[i] == null)
                continue;
            if (Utilities.interacts(Map.border, bullets[i]) != null)
            {
                bullets[i] = null;
            }
            else if (hero != null && Utilities.interacts(bullets[i], hero))
            {
                bullets[i] = null;
                return true;
            }
        }
        
        for (int j = 0; j < Constants.maxBulletCount; j++)
        {
            // for (int i = 0; i < bricks.Length; i++)
            //     if (bricks[i] != null && bricks[i].isTouching(bullets[j]))
            //     {
            //         bullets[j].hit = true;
            //         break;
            //     }
            bool bulletDestroyed = false;
            for (int i = 0; i < bricks.Length; i++)
            {
                if (bricks[i] != null)
                    bulletDestroyed = bricks[i].handleBullet(ref bullets[j]) || bulletDestroyed;
            }
            if (bulletDestroyed)
                bullets[j] = null;
        }
        
        return false;
    }

    public void handleBulletHitsFlag(Flag flag)
    {
        for (int i = 0; i < Constants.maxBulletCount; i++)
            if (bullets[i] != null && Utilities.interacts(bullets[i], flag))
            {
                bullets[i] = null;
                flag.destroy();
            }
    }

    public void handleBulletInteractions(Tank? other)
    {
        if (other == null)
            return;
        for (int i = 0; i < bullets.Length; i++)
        {
            bool destroy = false;
            if (bullets[i] == null) continue;
            for (int j = 0; j < other.bullets.Length; j++)
            {
                if (other.bullets[j] == null) continue;
                if (Utilities.interacts(bullets[i], other.bullets[j]))
                {
                    other.bullets[j] = null;
                    destroy = true;
                }
            }
            if (destroy)
                bullets[i] = null;
        }
    }

    public FloatRect getGlobalBounds()
    {
        return tankSprite.GetGlobalBounds();
    }

    public void myClamp(FloatRect rect)
    {
        FloatRect a = getGlobalBounds();
    
        if (!a.Intersects(rect, out FloatRect intersection))
            return;
        
        Vector2f correction = new Vector2f(0, 0);

        if (intersection.Width < intersection.Height)
        {
            if (a.Left < rect.Left)
                correction.X = -intersection.Width;
            else
                correction.X = intersection.Width;
        }
        else
        {
            if (a.Top < rect.Top)
                correction.Y = -intersection.Height;
            else
                correction.Y = intersection.Height;
        }
        tankSprite.Position += correction;
    }

    public virtual void Display(RenderWindow window)
    {
        for (int i = 0; i < bullets.Length; i++)
            if (bullets[i] != null)
                bullets[i].Display(window);;
        window.Draw(tankSprite);
    }

    protected void Move(Direction direction)
    {
        this.direction = direction;
        if (direction == Direction.Left) MoveLeft();
        else if (direction == Direction.Right) MoveRight();
        else if (direction == Direction.Up) MoveUp();
        else if (direction == Direction.Down) MoveDown();
    }

    protected void MoveLeft()
    {
        tankSprite.Position += new Vector2f(-Constants.tankSpeed, 0);
    }
    protected void MoveRight()
    {
        tankSprite.Position += new Vector2f(Constants.tankSpeed, 0);
    }
    protected void MoveUp()
    {
        tankSprite.Position += new Vector2f(0, -Constants.tankSpeed);
    }
    protected void MoveDown()
    {
        tankSprite.Position += new Vector2f(0, Constants.tankSpeed);
    }

    protected void updateBullets()
    {   
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            if (bullets[i] == null)
                continue;
            bullets[i].Update();
        }
    }
    
    


    public abstract void Update();
}
