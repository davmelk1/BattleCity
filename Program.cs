﻿using SFML.Graphics;
using SFML.Window;

namespace MyGame;

public static class TankGame
{
    private static RenderWindow window = null!;
    private static Map map = null!;

    public static void Main()
    {
        window = new RenderWindow(new VideoMode(Constants.windowWidth, Constants.windowHeight), "Battle City for ՀԾ");
        window.SetFramerateLimit(Constants.frameRate);
        window.Closed += (_, _) => window.Close();
        LoadTextures();
        GameLoop();
        Task.Delay(0).Wait();
    }

    private static void LoadTextures()
    {
        map = new Map();
    }

    private static void GameLoop()
    {
        try
        {
            while (window.IsOpen)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) break;
                window.DispatchEvents();
                window.Clear();
                map.Display(window);
                window.Display();
                map.Update();
            }
        } catch (Exception ex) when (ex is GameOverException or WinException)
        {
            window.Close();
        }
    }
    
}