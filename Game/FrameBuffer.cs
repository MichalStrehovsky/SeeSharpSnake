using System;

unsafe struct FrameBuffer
{
    public const int Width = 40;
    public const int Height = 20;
    public const int Area = Width * Height;

    fixed char _chars[Area];

    public void SetPixel(int x, int y, char character)
    {
        _chars[y * Width + x] = character;
    }

    public void Clear()
    {
        for (int i = 0; i < Area; i++)
            _chars[i] = ' ';
    }

    public readonly void Render()
    {
        Console.SetCursorPosition(0, 0);

        const ConsoleColor snakeColor = ConsoleColor.Green;

        Console.ForegroundColor = snakeColor;

        for (int i = 1; i <= Area; i++)
        {
            char c = _chars[i - 1];

            if (c == '*' || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                Console.ForegroundColor = c == '*' ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write(c);
                Console.ForegroundColor = snakeColor;
            }
            else
                Console.Write(c);

            if (i % Width == 0)
            {
                Console.SetCursorPosition(0, i / Width);
            }
        }
    }
}
