using System.Numerics;
using Raylib_cs;

public class Dino
{
    
    Texture2D texture;
    Animation animation;
    Animation runAnimation;
    Animation jumpAnimation;
    Animation duckAnimation;
    Rectangle collisionBox;
    Rectangle duckCollisionBox;
    Rectangle standCollisionBox;
    float gravityUp = 400f;
    float gravityDown = 900f;
    public Rectangle GetCollisionBox()
    {
        return collisionBox;
    }
    Vector2 position; 
    float jumpForce;
    float speed;
    bool canJump;
    
    

    public void Init(Texture2D tx)
    {
        texture = tx;
        //animation
       Rectangle[] runFrames =
        {
            new Rectangle(1514, 2, 88, 93),
            new Rectangle(1602, 2, 88, 93)
        };

        Rectangle[] duckFrames =
        {
            new Rectangle(1865, 35, 119, 61),
            new Rectangle(1984, 35, 119, 61)
        };

        Rectangle jumpFrame = new Rectangle(1337, 2, 88, 93);
        runAnimation = new Animation(runFrames, 0.2f);
        duckAnimation = new Animation(duckFrames, 0.2f);
        jumpAnimation = new Animation(jumpFrame);
        animation = runAnimation;

        //caixas de colisao
        position = new Vector2(0,Prefs.HEIGHT-93);
        standCollisionBox = new Rectangle(position.X+40, position.Y,48,93);
        duckCollisionBox = new Rectangle(position.X, position.Y,119,61);
        collisionBox = standCollisionBox;

        jumpForce = 500;
        canJump = true;
        speed = 0;
    }

    public void Draw()
    {
        //dinossauro
        Raylib.DrawTextureRec(texture,animation.GetCurrentFrame(),position, Color.White);
    }

    public void Update(float delta)
    {

        collisionBox = standCollisionBox;
        //pulo
        position.Y += speed*delta;
        
        if(Raylib.IsKeyPressed(KeyboardKey.Space)&& canJump)
        {
            animation = jumpAnimation;
            canJump = false;
            speed = -jumpForce;
               
        }
        if(!canJump)
        {
            if (speed < 0)
                speed += gravityUp * delta;
            else
                speed += gravityDown * delta;
        }

        //impede o dinossauro de atravessar o chão
        if(position.Y>Prefs.HEIGHT-93)
        {
            canJump = true; 
            speed = 0;
            position.Y = Prefs.HEIGHT-93;
            animation = runAnimation;
        }

        //duck
        if (Raylib.IsKeyDown(KeyboardKey.Down)&& canJump)
        {
            animation = duckAnimation;
            position.Y = Prefs.HEIGHT - 61;
            collisionBox = duckCollisionBox;
        }else if(canJump)
        {
            animation = runAnimation;
            position.Y = Prefs.HEIGHT - 93;
            collisionBox = standCollisionBox;
        }
        //animation
        animation.Update(delta);

        UpdateCollisionBoxes();

        
    }

    private void UpdateCollisionBoxes()
    {
        standCollisionBox.X = position.X+40;
        standCollisionBox.Y = position.Y;

        duckCollisionBox.X = position.X;
        duckCollisionBox.Y = position.Y;

    }


}