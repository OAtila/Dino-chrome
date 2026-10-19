using Raylib_cs;

public class ObstacleManager
{
    Obstacle obstacle;
    float speed;
    bool finished;
    public bool IsFinished()
    {
        return finished;
    }
    public void MarkObstaclePassed()
    {
        finished = false;
    }
    
    Texture2D texture;

    private readonly Random random = new Random();

    private readonly Func<Obstacle>[] obstacleFactories =
    {
        () => new Cacto(),
        () => new CactoDuplo(),
        () => new CactoQuadruplo(),
        () => new Passaro()
    };

    public void Init(Texture2D tx, float intialSpeed)
    {
        texture = tx;
        speed = intialSpeed;
        finished = false;

        SpawnObstacle();
        
    }

    public void Update(float delta)
    {
        obstacle.Update(delta);
        if (obstacle.IsFinished())
        {
            finished = true;
            SpawnObstacle();
        }
        
    }

    public void Draw()
    {
        obstacle.Draw();
    }

    public Rectangle GetCollisionBox()
    {
        return obstacle.GetCollisionBox();
    }

    private void SpawnObstacle()
    {
        int index = random.Next(obstacleFactories.Length);

        obstacle = obstacleFactories[index]();
        obstacle.Init(texture);

        obstacle.SetSpeed(speed);
        
    }

    public void SetSpeed(float newSpeed)
    {
        obstacle.SetSpeed(newSpeed);
    }

    public void Reset(Texture2D tx, float initialSpeed)
    {
        texture = tx;
        speed = initialSpeed;
        finished = false;

        SpawnObstacle();
    }
}