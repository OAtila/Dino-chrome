using System.Numerics;
using System.Reflection.Metadata;
using Dinossauro;
using Raylib_cs;

//classe onde o jogo acontece, por enquanto
public class Level1
{
    //debug
    Color bgColor;
    Color scoreColor;
    String scoreStr;
    String hiScoreStr;
    
  
    float score = 0;
    float hiScore = 0;
    int nextCicle;
    //para as texturas
    Image image;
    Texture2D texture;
    //background
    float bgSpeed;
    float speedIncrease = 5;
    float initialBgSpeed = 400f;
    int nextSoundScore;
    int scoreChangeInterval;
    bool gameOver;
    Rectangle bgRect;
    Rectangle nuvemRect;
    Rectangle scoreRectangle;
    Rectangle gameOverRect;
    Rectangle restartButton;
    Rectangle restartButtonText;
    Rectangle hiSource;
    Vector2 bgPos1;
    Vector2 bgPos2;
    Vector2 restartPos;
    Vector2 gameOverPos;
    Vector2 nuvemPos;
    Vector2 mouse;
    Vector2 scorePos;
    Vector2 hiScorePos;
    Sound dieSound;
    Sound pointSound;
    Dino dino = new Dino();
    ObstacleManager obstacleManager = new ObstacleManager();
     
    

    public void Init()
    {
        //dia e noite
        nextCicle = 700;
        //sons
        dieSound = Raylib.LoadSound("assets/sounds/die.wav");
        pointSound = Raylib.LoadSound("assets/sounds/point.wav");

        //background
        bgColor = Color.White;
        scoreColor = Color.Black;
        bgSpeed = 400;
        nextSoundScore = 100;
        scoreChangeInterval = 100;
        bgPos1 = new Vector2(0,Prefs.HEIGHT-32);
        bgPos2 = new Vector2(2404, Prefs.HEIGHT - 32);
        nuvemPos = new Vector2(Prefs.WIDTH, 50);

        //textura do cenario
        image = Raylib.LoadImage("assets/graphics/sprite.png");
        texture = Raylib.LoadTextureFromImage(image);
        bgRect = new Rectangle(0.0f, 100, 2404, 32);
        nuvemRect = new Rectangle(166, 1, 92, 28);
        //gameover
        gameOverPos = new Vector2(-1000,1000);
        gameOverRect = new Rectangle(954, 27, 381, 23);
        restartButtonText = new Rectangle(1,1, 73, 65);
        restartPos = new Vector2(-1000, 1000);
        restartButton = new Rectangle(Prefs.WIDTH / 2f - 100,
            Prefs.HEIGHT / 2f + 50,
            200,
            60
        );
        //score
        scoreRectangle = new Rectangle(954, 1, 16*5, 22);
        scorePos = new Vector2(Prefs.WIDTH-200, 30);
        dino.Init(texture);
        obstacleManager.Init(texture, bgSpeed);
        gameOver = false;
        hiSource = new Rectangle(1153,1, 39,22);
        hiScorePos = new Vector2(-1000, 1000);

    }
    public void Update(float delta)
    {
        if(gameOver){
            mouse = Raylib.GetMousePosition();

            if (Raylib.CheckCollisionPointRec(mouse, restartButton) &&
                Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                Reset();
            }   
                return;
        }

        //background
        bgPos1.X -= bgSpeed * delta;
        bgPos2.X -= bgSpeed * delta;
        nuvemPos.X -= bgSpeed * 0.5f * delta;
        if (bgPos1.X <= -bgRect.Width)
        {
            bgPos1.X = bgPos2.X + bgRect.Width;
        }

        if (bgPos2.X <= -bgRect.Width)
        {
            bgPos2.X = bgPos1.X + bgRect.Width;
        }
        //nuvem(layer1)
        if(nuvemPos.X<=0)
            nuvemPos.X = Prefs.WIDTH;
        //entidades
        dino.Update(delta);
        obstacleManager.Update(delta);
        //verificando colisão
        if(Collision.BoxCheck(dino.GetCollisionBox(),obstacleManager.GetCollisionBox()))
        {
           
            Raylib.PlaySound(dieSound);
            dino.PlayDeathAnimation();
            gameOverPos.X = Prefs.WIDTH * 0.5f - 190;
            gameOverPos.Y = Prefs.HEIGHT*0.5f;
            restartPos = new Vector2(Prefs.WIDTH * 0.5f - 37, Prefs.HEIGHT*0.5f+65);
            gameOver = true;
        }
        else
        {
            
        }
        //score
        score +=5*delta;
        scoreStr = $"Score: {(int)score}";
        hiScoreStr = $"{(int)hiScore}";
        if(score>hiScore)
            hiScore = score;

        score += 5 * delta;
        //sonzinho a cada 100 pontos
        if (score >= nextSoundScore)
        {
            nextSoundScore += scoreChangeInterval;
            Raylib.PlaySound(pointSound);      
        }
        //muda o dia para noite
        if(score >= nextCicle)
        {
            if(bgColor == Color.White)
            {
                bgColor = Color.Black;
                scoreColor = Color.White;
                nextCicle += 200;
            }
            else if(bgColor == Color.Black)
            {
                bgColor = Color.White;
                scoreColor = Color.Black;
                nextCicle += 700;
            }
        }
        //aumenta a velocidade ao superar obstaculos, talvez de para melhorar
        if(obstacleManager.IsFinished()&& bgSpeed<660)
        {
            bgSpeed += speedIncrease;
            obstacleManager.MarkObstaclePassed();     
        }
        obstacleManager.SetSpeed(bgSpeed);
            

    }
    private void Reset()
    {
        score = 0;
        bgColor = Color.White;
        scoreColor = Color.Black;
        scoreStr = "Score: 0";
        nextCicle = 700;
        nextSoundScore = 100;

        bgSpeed = initialBgSpeed;

        bgPos1 = new Vector2(0, Prefs.HEIGHT - 32);
        bgPos2 = new Vector2(2404, Prefs.HEIGHT - 32);

        nuvemPos = new Vector2(Prefs.WIDTH, 50);

        gameOverPos = new Vector2(-1000, 1000);
        restartPos = new Vector2(-1000, 1000);

        
        gameOver = false;

        dino.Reset();
        obstacleManager.Reset(texture, bgSpeed);
        hiScorePos = new Vector2(Prefs.WIDTH-400, 12);
        
    }
    public void Draw()
    {
        Raylib.ClearBackground(bgColor);
        //score
        Raylib.DrawText(scoreStr, Prefs.WIDTH-200, 12, 20, scoreColor);
        
        //background
        Raylib.DrawTextureRec(texture,bgRect,bgPos1,Color.White);
        Raylib.DrawTextureRec(texture,bgRect,bgPos2,Color.White);
        Raylib.DrawTextureRec(texture,nuvemRect,nuvemPos,Color.White);
        Raylib.DrawTextureRec(texture, gameOverRect,gameOverPos, Color.White);

        //restart
        Raylib.DrawTextureRec(texture, restartButtonText,restartPos, Color.White);
        //hi score
        Raylib.DrawTextureRec(texture, hiSource, hiScorePos, Color.White);
        Raylib.DrawText(hiScoreStr, (int)hiScorePos.X+49, 12, 20, scoreColor);
        //entidades
        dino.Draw();
        obstacleManager.Draw();

    }

 
}