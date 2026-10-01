using System.Numerics;
using Dinossauro;
using Raylib_cs;

//classe onde o jogo acontece, por enquanto
public class Level1
{
    //debug
    Color color;
    String palavra = "placeholder";
    //carregando texturas
    Image image;
    Texture2D texture;
    bool gameOver;
    Rectangle bgRect;
    Vector2 pos;
    Dino dino = new Dino();
    Passaro cacto = new Passaro();
     
    

    public void Init()
    {
        //debug
        color = Color.Green;

        pos = new Vector2(0,480-32);
        image = Raylib.LoadImage("assets/graphics/sprite.png");
        texture = Raylib.LoadTextureFromImage(image);
        bgRect = new Rectangle(0.0f, 100, 2404, 32);
        dino.Init(texture);
        cacto.Init(texture);
        gameOver = false;

    }
    public void Update(float delta)
    {
        if(gameOver)
            return;

        //update do cenario

        dino.Update(delta);
        cacto.Update(delta);
        //verificando colisão
        if(Collision.BoxCheck(dino.GetCollisionBox(),cacto.GetCollisionBox()))
        {
            color = Color.Red;
            gameOver = true;
        }
        else
        {
            color = Color.Green;
        }
    }
    public void Draw()
    {
        Raylib.DrawText(palavra, 12, 12, 20, Color.Black);
        //background
        Raylib.DrawTextureRec(texture,bgRect,pos, Color.White);
        dino.Draw();
        cacto.Draw();
        //debug
        Collision.ShowCollisionBox(dino.GetCollisionBox(), color);
        Collision.ShowCollisionBox(cacto.GetCollisionBox(), color);
    }
}