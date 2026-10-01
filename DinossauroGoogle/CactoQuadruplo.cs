using System.Numerics;
using Raylib_cs;

public class CactoQuadruplo : Obstacle
{
    public override void Init(Texture2D tx)
    {
        texture = tx;
        speed = 300;

        animation = new Animation(new Rectangle(751, 2, 200, 101));

            position = new Vector2(
            Prefs.WIDTH,
            Prefs.HEIGHT - 101
        );

        collisionBox = 
        new Rectangle(
            position.X,
            position.Y,
            200,
            101
        );
    }

    protected override void Respawn()
    {
        position = new Vector2(
            Prefs.WIDTH,
            Prefs.HEIGHT - 101
        );

        speed += 10;
    }

    protected override float GetWidth()
    {
        return 200;
    }
}