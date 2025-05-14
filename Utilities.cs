using SFML.Graphics;

namespace MyGame;

public abstract class Utilities
{
    public static FloatRect? interacts(Border border, IHavingBounds obj)
    {
        return border.interacts(obj.getGlobalBounds());
    }
    public static bool interacts(IHavingBounds obj1, IHavingBounds obj2)
    {
        return obj1.getGlobalBounds().Intersects(obj2.getGlobalBounds());
    }

    public static void handleCrushes(int index, Enemy?[] enemies)
    {
        if (enemies[index] == null) return;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) continue;
            if (!enemies[index]!.getGlobalBounds().Intersects(enemies[i]!.getGlobalBounds()) || i == index) 
                continue;
            enemies[i]!.randomChangeDirection();
            enemies[index]!.randomChangeDirection();
            enemies[index]!.myClamp(enemies[i]!.getGlobalBounds());
        }
    }

    public static void handleCrushes(Tank? tank, Flag obj2)
    {
        if (tank == null || !tank.getGlobalBounds().Intersects(obj2.getGlobalBounds())) 
            return;
        tank.myClamp(obj2.getGlobalBounds());
    }

    public static void handleCrushes(Tank? tank, Brick[]? bricks)
    {
        if (tank == null || bricks == null)
            return;
        foreach (var brick in bricks)
        {
            var interaction = brick.getInteractions(tank);
            if (interaction.Width == 0) continue;
            tank.myClamp(interaction);
            if (tank is Enemy enemy)
                enemy.randomChangeDirection();
        }
    }
}