using SFML.Graphics;
using SFML.System;

namespace MyGame;

public class GameOver
{
    private readonly Sprite game;
    private readonly Sprite over;

    public GameOver()
    {
        game = new Sprite(Constants.texture)
        {
            TextureRect = Constants.gameTextRect,
            Scale = Constants.gameOverScale,
        };
        over = new Sprite(Constants.texture)
        {
            TextureRect = Constants.overTextRect,
            Scale = Constants.gameOverScale,
        };
        game.Position = new Vector2f((Constants.gameX2 + Constants.gameX1) / 2 - game.GetGlobalBounds().Width * 2.1f / 2, 
            Constants.gameY2 - game.GetGlobalBounds().Height);
        over.Position = new Vector2f(game.GetGlobalBounds().Left + game.GetGlobalBounds().Width * 1.1f, 
            game.GetGlobalBounds().Top);

    }

    public void Update()
    {
        game.Position += new Vector2f(0, -5);
        over.Position += new Vector2f(0, -5);
        if (game.Position.Y < Constants.gameY1 + Constants.gameHeight / 2f)
            throw new GameOverException("Game Over");
    }   
    
    public void Display(RenderWindow window)
    {
        window.Draw(game);
        window.Draw(over);
    }
}

public class Win
{
    private readonly Text win;

    public Win()
    {
        win = new Text("WIN", Constants.font, 90);
        win.FillColor = Color.Green;
        
        win.Position = new Vector2f((Constants.gameX2 + Constants.gameX1) / 2 - win.GetGlobalBounds().Width / 2, 
            Constants.gameY2 - win.GetGlobalBounds().Height);
    }

    public void Update()
    {
        win.Position += new Vector2f(0, -5);
        if (win.Position.Y < Constants.gameY1 + Constants.gameHeight / 2f)
            throw new WinException("WIN");
    }   
    
    public void Display(RenderWindow window)
    {
        window.Draw(win);
    }
}