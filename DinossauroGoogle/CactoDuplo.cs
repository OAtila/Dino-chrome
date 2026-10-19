using System.Numerics;
using Raylib_cs;

public class CactoDuplo : Obstacle
{
    public override void Init(Texture2D tx)
    {
        texture = tx;

        animation = new Animation(new Rectangle(751, 2, 99, 101));

            position = new Vector2(
            Prefs.WIDTH,
            Prefs.HEIGHT - 101
        );

        collisionBox = 
        new Rectangle(
            position.X,
            position.Y,
            99,
            101
        );
    }



    protected override float GetWidth()
    {
        return 99;
    }
}