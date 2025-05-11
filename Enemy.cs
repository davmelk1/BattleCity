namespace MyGame;
using SFML.Graphics;
using SFML.System;

public class Enemy : Tank
{
    static Random rand = new Random();
    public Enemy()
    {
        fireTimer = 0;
        tankSprite = new Sprite(Constants.texture)
            {
                TextureRect = Constants.upHeroRect
                ,Scale = Constants.tankScale
            }
            ;
        direction = Direction.Left;
        
        tankSprite.Position = new Vector2f(Constants.enemyInitPositionX1 + rand.Next(2) * Constants.enemyInitPositionX2, 
            Constants.enemyInitPositionY);
    }

    private new void MoveLeft()
    {
        Move(direction);
        tankSprite.TextureRect = Constants.leftEnemyRect;
    }

    private new void MoveUp()
    {
        Move(direction);
        tankSprite.TextureRect = Constants.upEnemyRect;
    }

    private new void MoveRight()
    {
        Move(direction);
        tankSprite.TextureRect = Constants.rightEnemyRect;
    }

    private new void MoveDown()
    {
        Move(direction);
        tankSprite.TextureRect = Constants.downEnemyRect;
    }

    public void randomChangeDirection()
    {
        Array enumValues = Enum.GetValues(typeof(Direction));
        direction = (Direction)enumValues.GetValue(rand.Next(enumValues.Length));
        tankSprite.Position = new Vector2f(Math.Clamp(tankSprite.Position.X, Constants.gameX1, Constants.gameX2 - Constants.tankSize), 
            Math.Clamp(tankSprite.Position.Y, Constants.gameY1, Constants.gameY2 - Constants.tankSize));
    }

    public override void Update()
    {
        if (direction == Direction.Up) MoveUp();
        else if (direction == Direction.Down) MoveDown();
        else if (direction == Direction.Left) MoveLeft();
        else if (direction == Direction.Right) MoveRight();
        
        if (Utilities.interacts(Map.border, this) != null)
        {
           randomChangeDirection();
        }
        if (fireTimer++ > Constants.enemyFireIntervalInFrames)
        {
            fireTimer = 0;
            Fire();
        }
        updateBullets();
    }
}