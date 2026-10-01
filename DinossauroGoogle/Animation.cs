using Raylib_cs;

public class Animation
{
    Rectangle[] frames;
    int currentFrame;
    float animationTimer;
    float frameTime;

    public Animation(Rectangle[] frames, float frameTime)
    {
        this.frames = frames;
        this.frameTime = frameTime;
        currentFrame = 0;
        animationTimer = 0;
    }

    public Animation(Rectangle frame)
    {
        frames = new Rectangle[] { frame };
        currentFrame = 0;
        animationTimer = 0;
        frameTime = 0;
    }

    public void Update(float delta)
    {
        //quando utiliza apenas 1 frame
        if (frames.Length <= 1)
            return;
            
        animationTimer += delta;

        if (animationTimer >= frameTime)
        {
            animationTimer -= frameTime;

            currentFrame++;

            if (currentFrame >= frames.Length)
                currentFrame = 0;
        }
    }

    public Rectangle GetCurrentFrame()
    {
        return frames[currentFrame];
    }
    

}