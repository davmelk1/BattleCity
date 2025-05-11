using SFML.System;

namespace MyGame;
using SFML.Graphics;

public class Map
{
    private static Texture mapTexture;
    private static Sprite mapSprite;
    private int enemyCount;
    public static int currentEnemyCount;
    private int brickCount;
    private Brick[]? bricks;
    private static Hero hero;
    private static Enemy?[] enemies;
    private static Sprite?[] enemiesLeft;
    private static Sprite playerLivesIcon;
    private static int playerLives = 3;
    private static Flag flag;
    public static Border border;
    private bool pending = false;
    private bool allEnemiesCreated = false;
    private int level;
    private GameOver? gameOver;
    private Win? win;
    public Map(int level)
    {
        enemyCount = Constants.levelToEnemyCount[level];
        mapTexture = new Texture(Constants.backgroundImage);
        mapSprite = new Sprite(mapTexture)
        {
            Scale = new Vector2f(20, 20)
        };
        enemies = new Enemy[enemyCount];
        Task.Run(async () => await createEnemies());
        enemiesLeft = new Sprite[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            enemiesLeft[i] = new Sprite(Constants.texture){
                TextureRect = Constants.enemyLeftRect
                ,Scale = Constants.enemyLeftScale
            };
            enemiesLeft[i].Position = new Vector2f(Constants.gameX2 + Constants.enemyLeftSize + (Constants.enemyLeftSize * (i % 2)), 
                Constants.borderUp.Y + Constants.enemyLeftSize * (i / 2) );
        }
        playerLivesIcon = new Sprite(Constants.texture){
            TextureRect = Constants.playerLivesRect
            ,Scale = Constants.playerLivesScale
        };
        playerLivesIcon.Position = Constants.playerLivesIconPosition;
        hero = new Hero();
        border = new Border();
        flag = new Flag();
        loadMapFromFile();
    }

    private void loadMapFromFile(int level2load = 1)
    {
        string[] lines = File.ReadAllLines(Constants.levelsPath + level2load);

        int count = 0;
        foreach (var line in lines)
        {
            foreach (char c in line)
            {
                if (c == '#') count++;
            }
        }

        // Initialize the Sprite array
        bricks = new Brick[count];
        brickCount = count;
        int index = 0;

        // Second pass: create and store sprites
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == '#')
                {
                    bricks[index++] = new Brick(50 + j * 25, 50 + i * 25);
                }
            }
        }
    }
    
    private void createBricks()
    {
        brickCount = Constants.levelToBrickCount[1];
        bricks = new Brick[brickCount];
        bricks[0] = new Brick(Constants.flagInitPosition.X - Constants.brickSize, Constants.flagInitPosition.Y - Constants.brickSize);
        bricks[1] = new Brick(Constants.flagInitPosition.X - Constants.brickSize, Constants.flagInitPosition.Y);
        bricks[2] = new Brick(Constants.flagInitPosition.X - Constants.brickSize, Constants.flagInitPosition.Y + Constants.brickSize);
        bricks[3] = new Brick(Constants.flagInitPosition.X, Constants.flagInitPosition.Y - Constants.brickSize);
        bricks[4] = new Brick(Constants.flagInitPosition.X + Constants.brickSize, Constants.flagInitPosition.Y - Constants.brickSize);
        bricks[5] = new Brick(Constants.flagInitPosition.X + 2 * Constants.brickSize, Constants.flagInitPosition.Y - Constants.brickSize);
        bricks[6] = new Brick(Constants.flagInitPosition.X + 2 * Constants.brickSize, Constants.flagInitPosition.Y);
        bricks[7] = new Brick(Constants.flagInitPosition.X + 2 * Constants.brickSize, Constants.flagInitPosition.Y + Constants.brickSize);
        bricks[11] = new Brick(180, 200);
    }

    public void LocalReset()
    {
        
    }

    public void Display(RenderWindow window)
    {
        window.Draw(mapSprite);
        border.Display(window);
        hero.Display(window);
        for (int i = 0; i < currentEnemyCount; i++)
            enemies[i]?.Display(window);
        for (int i = 0; i < enemyCount - currentEnemyCount; i++)
            window.Draw(enemiesLeft[i]);
        window.Draw(playerLivesIcon);
        
        var text = new Text(playerLives.ToString(), new Font("/home/davit/C#/kursach/MyGame/fonts/Arial.ttf"), 40);
        text.FillColor = Color.Black;
        text.Position = new Vector2f(Constants.playerLivesIconPosition.X + 15, 
            Constants.playerLivesIconPosition.Y + Constants.playerLivesIconSize);
        window.Draw(text);
        flag.Display(window);
        for (int i = 0; i < brickCount; i++)
            if (bricks[i] != null)
                bricks[i].Display(window);
        gameOver?.Display(window);
        win?.Display(window); 
    }

    public void Update()
    {
        updateHero();
        updateEnemies();
        if (checkGameOver())
        {
            gameOver = new GameOver();
            win = null;
        }
        gameOver?.Update();
        if (checkWin()) 
            win = new Win();
        win?.Update();
            
    }

    private bool checkWin()
    {
        return win == null && allEnemiesCreated && enemies.All(x => x == null) && gameOver == null;
    }

    private void updateEnemies()
    {
        for (int i = 0; i < currentEnemyCount; i++) 
            updateEnemy(i);
    }

    private void updateEnemy(int i)
    {
        for (int j = 0; j < currentEnemyCount; j++)
        {
            if (i == j) continue;
            enemies[i]?.handleBulletInteractions(enemies[j]);
        }
        hero.handleBulletInteractions(enemies[i]);
        enemies[i]?.Update();
        Utilities.handleCrushes(i, enemies);
        Utilities.handleCrushes(enemies[i], flag);
        Utilities.handleCrushes(enemies[i], bricks);
        hero.HandleCollisionWith(enemies[i]);
        handleEnemyBullets(i);
        
    }

    private void updateHero()
    {
        hero.Update();
        Utilities.handleCrushes(hero, flag);
        Utilities.handleCrushes(hero, bricks);
        handleHeroBullets();
    }

    private bool checkGameOver()
    {
        return gameOver == null && (playerLives == 0 || flag.isDestroyed());
    }

    private void handleHeroBullets()
    {
        hero.handleBulletInteractions(enemies);
        hero.handleBulletInteractions(bricks);
        hero.handleBulletHitsFlag(flag);
    }

    private void handleEnemyBullets(int i)
    {
        if (enemies[i] == null)
            return;
        bool heroDestroyed = enemies[i].handleBulletInteractions(bricks, hero);
        if (!pending && heroDestroyed)
        {
            CreateDumpThenRealHero();
            playerLives--;
        }
        enemies[i].handleBulletHitsFlag(flag);
    }

    private void CreateDumpThenRealHero()
    {
        hero = new DumpHero();
        Task.Run(async () => await createHero());
    }

    private async Task createEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i] = new Enemy();
            currentEnemyCount++;
            await Task.Delay(3000);
        }
        allEnemiesCreated = true;
    }

    private async Task createHero()
    {
        pending = true;
        await Task.Delay(3000);
        hero = new Hero();
        pending = false;
    }
    
}
