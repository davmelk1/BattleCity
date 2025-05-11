using SFML.Graphics;

namespace MyGame;

public class Utilities
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
        for (int i = 0; i < Map.currentEnemyCount; i++)
        {
            if (enemies[i] == null) continue;
            if (enemies[index].getGlobalBounds().Intersects(enemies[i].getGlobalBounds()) && i != index)
            {
                enemies[i].randomChangeDirection();
                enemies[index].randomChangeDirection();
                enemies[index].myClamp(enemies[i].getGlobalBounds());
            }
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
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i] != null)
            {
                FloatRect interaction;
                interaction = bricks[i].getInteractions(tank);
                if (interaction.Width == 0) continue;
                tank.myClamp(interaction);
                if (tank is Enemy)
                    ((Enemy)tank).randomChangeDirection();
            }
        }   
}
}