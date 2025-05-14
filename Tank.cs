namespace MyGame;
using SFML.Graphics;
using SFML.System;
public abstract class Tank : IHavingBounds
{
    protected Sprite tankSprite;
    protected Direction direction;
    protected readonly Bullet?[] bullets;
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
        fireTimer = 0;
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            if (bullets[i] != null) continue;
            bullets[i] = new Bullet(tankSprite.Position.X, tankSprite.Position.Y, direction);
            break;
        }
    }
    
    public bool handleBulletInteractions(Brick?[] bricks, Hero? hero = null)
    {
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            if (bullets[i] == null)
                continue;
            if (Utilities.interacts(Map.border, bullets[i]!) != null)
            {
                bullets[i] = null;
            }
            else if (hero != null && Utilities.interacts(bullets[i]!, hero))
            {
                bullets[i] = null;
                return true;
            }
        }
        
        for (int j = 0; j < Constants.maxBulletCount; j++)
        {
            if (bullets[j] == null)
                continue;
            foreach (var brick in bricks)
                if (brick != null && brick.isTouching(bullets[j]))
                {
                    bullets[j]!.Hit = true;
                    break;
                }

            var bulletDestroyed = bricks.OfType<Brick>().Aggregate(false, (current, brick) => brick.handleBullet(ref bullets[j]) || current);
            if (bulletDestroyed)
                bullets[j] = null;
        }
        
        return false;
    }

    public void handleBulletHitsFlag(Flag flag)
    {
        for (int i = 0; i < Constants.maxBulletCount; i++)
            if (bullets[i] != null && Utilities.interacts(bullets[i]!, flag))
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
            var destroy = false;
            if (bullets[i] == null) continue;
            for (int j = 0; j < other.bullets.Length; j++)
            {
                if (other.bullets[j] == null) continue;
                if (!Utilities.interacts(bullets[i]!, other.bullets[j]!)) continue;
                other.bullets[j] = null;
                destroy = true;
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
        var myBounds = getGlobalBounds();
    
        if (!myBounds.Intersects(rect, out var intersection))
            return;
        
        var correctionVector = new Vector2f(0, 0);

        if (intersection.Width < intersection.Height)
        {
            if (myBounds.Left < rect.Left)
                correctionVector.X = -intersection.Width;
            else
                correctionVector.X = intersection.Width;
        }
        else
        {
            if (myBounds.Top < rect.Top)
                correctionVector.Y = -intersection.Height;
            else
                correctionVector.Y = intersection.Height;
        }
        tankSprite.Position += correctionVector;
    }

    public virtual void Display(RenderWindow window)
    {
        foreach (var bullet in bullets)
            bullet?.Display(window);
        window.Draw(tankSprite);
    }

    protected void Move(Direction moveDirection)
    {
        direction = moveDirection;
        if (direction == Direction.Left) MoveLeft();
        else if (direction == Direction.Right) MoveRight();
        else if (direction == Direction.Up) MoveUp();
        else if (direction == Direction.Down) MoveDown();
    }

    private void MoveLeft()
    {
        tankSprite.Position += new Vector2f(-Constants.tankSpeed, 0);
    }

    private void MoveRight()
    {
        tankSprite.Position += new Vector2f(Constants.tankSpeed, 0);
    }

    private void MoveUp()
    {
        tankSprite.Position += new Vector2f(0, -Constants.tankSpeed);
    }

    private void MoveDown()
    {
        tankSprite.Position += new Vector2f(0, Constants.tankSpeed);
    }

    protected void updateBullets()
    {   
        for (int i = 0; i < Constants.maxBulletCount; i++)
            bullets[i]?.Update();
    }

    public abstract void Update();
}
