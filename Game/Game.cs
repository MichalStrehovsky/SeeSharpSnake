using System;
using SeeSharpSnake;
using SeeSharpSnake.Game;

unsafe struct Game
{
    static Music music;

    enum Result
    {
        Win, Loss, None
    }

    static Game game;

    // sprites are 4x4
    internal const byte boardWidth = 40;
    internal const byte boardHeight = 40; 

    static Random _random;

    Snake s;
    byte foodX, foodY;

    static readonly byte[] foodSprite = new byte[]
    {
        0b10100101,
        0b10100101,
    };
    static readonly byte[] foodSprite2 = new byte[]
    {
        0b11111111,
        0b11111111,
    };

    /// <summary>
    ///  runs once at startup
    /// </summary>
    [System.Runtime.InteropServices.UnmanagedCallersOnly(EntryPoint = "start")]
    public static void start()
    {
        game = new Game(42);
    }

    [System.Runtime.InteropServices.UnmanagedCallersOnly(EntryPoint = "update")]
    public static void update()
    {
        game.Main();
    }


    private Game(uint randomSeed)
    {
        _random = new Random(randomSeed);
        music.SetUp();
        s = new Snake(
            (byte)(_random.Next() % boardWidth),
            (byte)(_random.Next() % boardHeight),
            (Snake.Direction)(_random.Next() % 4));

        MakeFood(s, out foodX, out foodY);
    }

    Result Update()
    {
        music.PlayTune();

        if (!s.Update())
        {
            s.Draw();
            return Result.Loss;
        }

        s.Draw();

        switch ((*W4.GAMEPAD1)) // just handle single key
        {
            case W4.BUTTON_UP:
                s.Course = Snake.Direction.Up; break;
            case W4.BUTTON_DOWN:
                s.Course = Snake.Direction.Down; break;
            case W4.BUTTON_LEFT:
                s.Course = Snake.Direction.Left; break;
            case W4.BUTTON_RIGHT:
                s.Course = Snake.Direction.Right; break;
        }

        if (s.HitTest(foodX, foodY))
        {
            if (s.Extend())
                MakeFood(s, out foodX, out foodY);
            else
                return Result.Win;
        }

        fixed (byte* spAddr = &(foodSprite[0]))
        {
            W4.blit(spAddr, foodX << 2, foodY << 2, 4, 4, W4.BLIT_1BPP);
        }

        return Result.None;
    }

    static void MakeFood(in Snake snake, out byte foodX, out byte foodY)
    {
        do
        {
            foodX = (byte)(_random.Next() % boardWidth);
            foodY = (byte)(_random.Next() % boardHeight);
        }
        while (snake.HitTest(foodX, foodY));
    }

    void Main()
    {
        Result result = Update();

        if (result != Result.None)
        {
            string message = result == Result.Win ? "You win" : "You lose";

            fixed (char* fixedMsg = message)
            {
                W4.textUtf16((byte*) fixedMsg, (uint) message.Length * 2, 70, 40);
            }
        }
    }
}

