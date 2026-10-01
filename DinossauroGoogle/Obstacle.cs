using System.Numerics;
using Raylib_cs;

public abstract class Obstacle
{
    protected Texture2D texture;
    protected Animation animation;

    protected float speed;
    protected Vector2 position;
    protected Rectangle collisionBox;

    public Rectangle GetCollisionBox()
    {
        return collisionBox;
    }

    public virtual void Init(Texture2D tx)
    {
        texture = tx;
    }

    public virtual void Update(float delta)
    {
        Move(delta);
        UpdateCollisionBox();
        animation.Update(delta);

        if (position.X < -GetWidth())
        {
            Respawn();
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

    protected abstract void Respawn();

    protected abstract float GetWidth();
}