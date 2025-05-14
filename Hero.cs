namespace MyGame;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Hero : Tank
{
    private bool isMoving;
    public Hero()
    {
        fireTimer = Constants.heroFireIntervalInFrames;
        tankSprite = new Sprite(Constants.texture)
        {
            TextureRect = Constants.upHeroRect
            ,Scale = Constants.tankScale
        }
            ;
        direction = Direction.Up;
        tankSprite.Position = new Vector2f(Constants.heroInitPositionX, Constants.heroInitPositionY);
    }

    private new void Move(Direction moveDirection)
    {
        isMoving = true;
        base.Move(moveDirection);
        var borderIntersection = Utilities.interacts(Map.border, this);
        if (borderIntersection != null)
            myClamp((FloatRect)borderIntersection);
    }

    private new void MoveLeft()
    {
        tankSprite.TextureRect = Constants.leftHeroRect;
        Move(Direction.Left);
    }

    private new void MoveUp()
    {
        tankSprite.TextureRect = Constants.upHeroRect;
        Move(Direction.Up);
    }

    private new void MoveRight()
    {
        tankSprite.TextureRect = Constants.rightHeroRect;
        Move(Direction.Right);
    }

    private new void MoveDown()
    {
        tankSprite.TextureRect = Constants.downHeroRect;
        Move(Direction.Down);
    }

    public override void Update()
    {
        isMoving = false;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))  MoveLeft();
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) MoveRight();
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Up))    MoveUp();
        else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))  MoveDown();
        // if (fireTimer++ > Constants.heroFireIntervalInFrames && Keyboard.IsKeyPressed(Keyboard.Key.Space))
        if (bullets.Count(x => x != null) < 2 && fireTimer++ > Constants.heroFireIntervalInFrames && Keyboard.IsKeyPressed(Keyboard.Key.Space))
            Fire();
        updateBullets();
    }

    public void handleBulletInteractions(Enemy?[] enemies)
    {
        for (int i = 0; i < Constants.maxBulletCount; i++)
        {
            if (bullets[i] == null)
                continue;
            for (int j = 0; j < enemies.Length; j++)
            {
                if (enemies[j] == null || !Utilities.interacts(bullets[i]!, enemies[j]!))
                    continue;
                enemies[j] = null;
                bullets[i] = null;
                break;
            }
        }
    }
    public void HandleCollisionWith(Tank? other)
    {
        if (other == null)
            return;
        var thisBounds = getGlobalBounds();
        var otherBounds = other.getGlobalBounds();

        if (!thisBounds.Intersects(otherBounds, out FloatRect intersection))
            return;

        var facingEachOther =
            (direction == Direction.Right && other.Direction == Direction.Left) ||
            (direction == Direction.Left && other.Direction == Direction.Right) ||
            (direction == Direction.Up && other.Direction == Direction.Down) ||
            (direction == Direction.Down && other.Direction == Direction.Up);

        var thisHitFromSide = false;
        var otherHitFromSide = false;

        if (intersection.Width < intersection.Height)
        {
            if ((direction == Direction.Right && thisBounds.Left < otherBounds.Left) ||
                (direction == Direction.Left && thisBounds.Left > otherBounds.Left))
                thisHitFromSide = true;

            if ((other.Direction == Direction.Right && otherBounds.Left < thisBounds.Left) ||
                (other.Direction == Direction.Left && otherBounds.Left > thisBounds.Left))
                otherHitFromSide = true;
        }
        else
        {
            if ((direction == Direction.Down && thisBounds.Top < otherBounds.Top) ||
                (direction == Direction.Up && thisBounds.Top > otherBounds.Top))
                thisHitFromSide = true;

            if ((other.Direction == Direction.Down && otherBounds.Top < thisBounds.Top) ||
                (other.Direction == Direction.Up && otherBounds.Top > thisBounds.Top))
                otherHitFromSide = true;
        }

        if (facingEachOther)
        {
            var halfIntersection = new FloatRect(
                intersection.Left, intersection.Top,
                intersection.Width / 2f, intersection.Height / 2f
            );
            if (isMoving)
            {
                myClamp(halfIntersection);
                other.myClamp(halfIntersection);  
            }
            else
                other.myClamp(intersection);
        }
        else if (thisHitFromSide)
            myClamp(intersection);
        else if (otherHitFromSide) 
            other.myClamp(intersection);
    }
}
