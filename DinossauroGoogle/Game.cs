using Raylib_cs;

namespace Dinossauro;

public class Game
{
    
    // STAThread is required if you deploy using NativeAOT on Windows
    // See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Raylib.SetTargetFPS(60);
        Level1 lv1 = new Level1();
        Raylib.InitWindow(Prefs.WIDTH, Prefs.HEIGHT, "Dinossauro do google");
        Raylib.InitAudioDevice();
        lv1.Init();

        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            lv1.Update(deltaTime);
            Raylib.BeginDrawing();
            
            lv1.Draw();
            Raylib.DrawFPS(10, 10);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}