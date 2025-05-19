namespace MyGame;
using SFML.Graphics;
using SFML.System;

public static class Constants
{
    public static uint frameRate = 20;
    public static uint heroFireIntervalInFrames = 2;
    public static uint enemyFireIntervalInFrames = 30;
    
    public static Vector2f borderUp = new(1200, 50); 
    public static Vector2f borderDown = new(1200, 50); 
    public static Vector2f borderLeft = new(50, 800); 
    public static Vector2f borderRight = new(100, 800); 
    
    public const uint gameWidth = 650;
    public const uint gameHeight = 650    ;
    public static float gameX1 = borderLeft.X;
    public static float gameY1 = borderUp.Y;
    public static float gameX2 = gameX1 + gameWidth;
    public static float gameY2 = gameY1 + gameHeight;
    
    public static Vector2f borderLeftPosition = new(0, 0); 
    public static Vector2f borderUpPosition = new(0, 0); 
    public static Vector2f borderRightPosition = new(gameWidth + borderLeft.X, 0); 
    public static Vector2f borderDownPosition = new(0, gameHeight + borderUp.Y);
    
    public static uint windowWidth = Convert.ToUInt32(gameWidth + borderLeft.X + borderRight.X);
    public static uint windowHeight = Convert.ToUInt32(gameHeight + borderUp.Y + borderDown.Y);
    public static Color borderColor = new(128, 128, 128);
    
    private const int originalTankSize = 16;
    private const int originalBulletWidth = 3;
    private const int originalBulletHeight = 4;
    private const int originalEnemyLeftWidth = 8;
    private const int originalEnemyLeftHeight = 9;
    private const int originalPlayerLivesSize = 19;
    private const int originalSmallBrickSize = 4;

    private static readonly string basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
    public static readonly string texturesImage = Path.Combine(basePath, "imgs", "all.png");
    public static readonly string backgroundImage = Path.Combine(basePath, "imgs", "black.png");
    public static readonly string levelsPath = Path.Combine(basePath, "levels/");
    public static readonly string font = Path.Combine(basePath, "fonts", "Arial.ttf");
    
    public static Dictionary<int, int> levelToEnemyCount =  new(){
        {1, 1},
        {2, 2},
        {3, 3},
        {4, 3},
    };

    public static Texture texture = new(texturesImage);
    public static uint maxBulletCount = 10;
    public const int tankSpeed = 10;
    public const float bulletSpeed = 12.5f;
    public const float tankSize = 50;
    public const float enemyLeftSize = 25;
    public const float bulletSize = 15;
    public const float playerLivesIconSize = 50;
    public const float smallBrickSize = tankSize / 4;
    public const float brickSize = tankSize / 2;
    public static readonly float heroInitPositionX = gameX1 + gameWidth / 2 - tankSize * 2;
    public static readonly float heroInitPositionY = gameY1 + gameHeight - tankSize;
    public static readonly float enemyInitPositionX1 = gameX1;
    public static readonly float enemyInitPositionX2 = gameX1 + gameWidth - tankSize;
    public static readonly float enemyInitPositionY = gameY1;
    public static readonly Vector2f playerLivesIconPosition = new(gameX2 + enemyLeftSize, borderUp.Y + gameHeight - 200);
    public static readonly Vector2f flagInitPosition = new(gameX1 + gameWidth / 2 - tankSize / 2, heroInitPositionY);
    
    public static Vector2f tankScale = new(tankSize / originalTankSize, tankSize / originalTankSize);
    public static Vector2f enemyLeftScale = new(enemyLeftSize / originalEnemyLeftWidth, enemyLeftSize / originalEnemyLeftHeight);
    public static Vector2f playerLivesScale = new(playerLivesIconSize / originalPlayerLivesSize, playerLivesIconSize / originalPlayerLivesSize);
    public static Vector2f bulletScale = new(bulletSize / originalBulletWidth, bulletSize / originalBulletHeight);
    public static Vector2f smallBrickScale = new(smallBrickSize / originalSmallBrickSize, smallBrickSize / originalSmallBrickSize);
    public static Vector2f gameOverScale = new(gameWidth * 0.2f / 32, gameHeight * 0.1f / 9);
    
    public static IntRect upHeroRect = new(originalTankSize * 0, 0, originalTankSize, originalTankSize);
    public static IntRect leftHeroRect = new(originalTankSize * 2, 0, originalTankSize, originalTankSize);
    public static IntRect downHeroRect = new(originalTankSize * 4, 0, originalTankSize, originalTankSize);
    public static IntRect rightHeroRect = new(originalTankSize * 6, 0, originalTankSize, originalTankSize);
    public static IntRect upEnemyRect = new(originalTankSize * 8, 0, originalTankSize, originalTankSize);
    public static IntRect leftEnemyRect = new(originalTankSize * 10, 0, originalTankSize, originalTankSize);
    public static IntRect downEnemyRect = new(originalTankSize * 12, 0, originalTankSize, originalTankSize);
    public static IntRect rightEnemyRect = new(originalTankSize * 14, 0, originalTankSize, originalTankSize);
    public static IntRect enemyLeftRect = new(320, 192, originalEnemyLeftWidth, originalEnemyLeftHeight);
    public static IntRect upBulletRect = new(323, 102, 3, 4);
    public static IntRect leftBulletRect = new(330, 102, 4, 3);
    public static IntRect downBulletRect = new(339, 102, 3, 4);
    public static IntRect rightBulletRect = new(346, 102, 4, 3);
    public static IntRect playerLivesRect = new(375, 134, originalPlayerLivesSize, originalPlayerLivesSize);
    public static IntRect flagRect = new(originalTankSize * 19, originalTankSize * 2, originalTankSize, originalTankSize);
    public static IntRect flagDestroyedRect = new(originalTankSize * 20, originalTankSize * 2, originalTankSize, originalTankSize);
    public static IntRect brickTextureRect = new IntRect(288, 64, originalSmallBrickSize, originalSmallBrickSize);
    public static IntRect brickTextureRect2 = new IntRect(292, 64, originalSmallBrickSize, originalSmallBrickSize);
    public static IntRect gameTextRect = new IntRect(288, 184, 32, 7);
    public static IntRect overTextRect = new IntRect(288, 192, 32, 7);

    public static IntRect destroyedTankCoverV = new(originalTankSize * 16, originalTankSize * 9, originalTankSize, originalTankSize);
    public static IntRect destroyedTankCoverH = new(originalTankSize * 17, originalTankSize * 9, originalTankSize, originalTankSize);

    public static Dictionary<Direction, IntRect> bulletDirection2Rect = new() {
        { Direction.Up, upBulletRect },
        { Direction.Left, leftBulletRect },
        { Direction.Down, downBulletRect },
        { Direction.Right, rightBulletRect }
    };


}

public interface IHavingBounds
{
    public FloatRect getGlobalBounds();
}

public enum Direction { Up, Down, Left, Right }
