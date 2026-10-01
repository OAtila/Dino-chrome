using Raylib_cs;

public static class Collision
{
    public static bool BoxCheck(Rectangle a, Rectangle b)
    {
        return a.X < b.X + b.Width &&
        a.X + a.Width > b.X &&
        a.Y < b.Y + b.Height &&
        a.Y + a.Height > b.Y;
    }

    public static void ShowCollisionBox(Rectangle collisionBox,Color color)
    {
         Raylib.DrawRectangleLines((int)collisionBox.Position.X, (int)collisionBox.Position.Y, (int)collisionBox.Width, (int)collisionBox.Height, color);
    }
}