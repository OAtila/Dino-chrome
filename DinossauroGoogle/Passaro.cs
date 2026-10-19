using System.Numerics;
using Raylib_cs;

public class Passaro : Obstacle
{
    float high = Prefs.HEIGHT/2 - 67;
    float low = Prefs.HEIGHT - 150;

    Random random = new Random();

    public override void Init(Texture2D tx)
    {
        texture = tx;

        Rectangle[] flyingFrames =
        {
            new Rectangle(260, 2, 91, 67),
            new Rectangle(352, 2, 91, 59)
        };

        Animation flyingAnimation;
        flyingAnimation = new Animation(flyingFrames, 0.2f);
        animation = flyingAnimation;

        position = new Vector2(Prefs.WIDTH,GetRandomHeight());

        collisionBox = new Rectangle(
            position.X,
            position.Y,
            91,
            59
        );
    }



    private float GetRandomHeight()
    {
        return random.Next(2) == 0 ? low : high;
    }

    protected override float GetWidth()
    {
        return 91;
    }
}