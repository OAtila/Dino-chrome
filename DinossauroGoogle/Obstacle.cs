using System.Numerics;
using Raylib_cs;

public abstract class Obstacle
{
    protected Texture2D texture;
    protected Animation animation;

    protected float speed;
    protected Vector2 position;
    protected Rectangle collisionBox;
    protected bool finished;
    public bool IsFinished()
    {
        return finished;
    }
 

    public Rectangle GetCollisionBox()
    {
        return collisionBox;
    }

    public virtual void Init(Texture2D tx)
    {
        texture = tx;
        
        finished = false;
    }

    public virtual void Update(float delta)
    {
        Move(delta);
        UpdateCollisionBox();
        animation.Update(delta);

        if (position.X < -GetWidth())
        {
            finished = true;
        }
    }

    public virtual void Draw()
    {
        Raylib.DrawTextureRec(
            texture,
            animation.GetCurrentFrame(),
            position,
            Color.White
        );
    }

    protected virtual void Move(float delta)
    {
        position.X -= delta * speed;
    }

    protected virtual void UpdateCollisionBox()
    {
        collisionBox.X = position.X;
        collisionBox.Y = position.Y;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    protected abstract float GetWidth();
}