using System;
using SeeSharpSnake;

unsafe struct Snake
{
    public const int MaxLength = 30;

    private int _length;

    // Body is a packed integer that packs the X coordinate, Y coordinate, and the character
    // for the snake's body.
    // Only primitive types can be used with C# `fixed`, hence this is an `int`.
    private unsafe fixed int _body[MaxLength];

    private Direction _direction;
    private Direction _oldDirection;

    public Direction Course
    {
        set
        {
            if (_oldDirection != _direction)
                _oldDirection = _direction;

            if (_direction - value != 2 && value - _direction != 2)
                _direction = value;
        }
    }

    public unsafe Snake(byte x, byte y, Direction direction)
    {
        _body[0] = new Part(x, y, DirectionToSprite(direction, direction)).Pack();
        _direction = direction;
        _oldDirection = direction;
        _length = 1;
    }

    public unsafe bool Update()
    {
        Part oldHead = Part.Unpack(_body[0]);
        Part newHead = new Part(
            (byte)(_direction switch
            {
                Direction.Left => oldHead.X == 0 ? Game.boardWidth - 1 : oldHead.X - 1,
                Direction.Right => (oldHead.X + 1) % Game.boardHeight,
                _ => oldHead.X,
            }),
            (byte)(_direction switch
            {
                Direction.Up => oldHead.Y == 0 ? Game.boardWidth - 1 : oldHead.Y - 1,
                Direction.Down => (oldHead.Y + 1) % Game.boardHeight,
                _ => oldHead.Y,
            }),
            DirectionToSprite(_direction, _direction)
            );

        oldHead = new Part(oldHead.X, oldHead.Y, DirectionToSprite(_oldDirection, _direction));

        bool result = true;

        for (int i = 0; i < _length - 1; i++)
        {
            Part current = Part.Unpack(_body[i]);
            if (current.X == newHead.X && current.Y == newHead.Y)
                result = false;
        }

        _body[0] = oldHead.Pack();

        for (int i = _length - 2; i >= 0; i--)
        {
            _body[i + 1] = _body[i];
        }

        _body[0] = newHead.Pack();

        _oldDirection = _direction;

        return result;
    }

    // divide w4 into 40x40, each square 4x4 pixels
    public readonly void Draw()
    {
        for (int i = 0; i < _length; i++)
        {
            Part p = Part.Unpack(_body[i]);
            fixed (byte* spriteAddress = &(DirectionSprites[0]))
            {
                W4.blit(spriteAddress, p.X << 2, p.Y << 2, 4, 4, W4.BLIT_1BPP);
            }
        }
    }

    public bool Extend()
    {
        if (_length < MaxLength)
        {
            _length += 1;
            return true;
        }
        return false;
    }

    public unsafe readonly bool HitTest(int x, int y)
    {
        for (int i = 0; i < _length; i++)
        {
            Part current = Part.Unpack(_body[i]);
            if (current.X == x && current.Y == y)
                return true;
        }

        return false;
    }

    static readonly byte[] DirectionSprites = new byte[]
    {
        0b11111111,
        0b11111111,
    };

    //TODO:
    // create sprites for these and place in DirectionSprites "│┌?┐┘─┐??└│┘└?┌─";
    private static byte DirectionToSprite(Direction oldDirection, Direction newDirection)
    {
        return 0;
        // byte* a;
        // fixed (byte* t = DirectionSprites)
        // {
        //     a = t;
        // }
        //
        // return a;
    }

    // Helper struct to pack and unpack the packed integer in _body.
    readonly unsafe struct Part
    {
        public readonly byte X, Y;
        public readonly byte SpriteIx;

        public Part(byte x, byte y, byte spriteIx)
        {
            X = x;
            Y = y;
            SpriteIx = spriteIx;
        }

        public int Pack() => X << 24 | Y << 16 | SpriteIx;
        public static Part Unpack(int packed) => new Part((byte)(packed >> 24), (byte)(packed >> 16), (byte)packed);
    }

    public enum Direction
    {
        Up, Right, Down, Left
    }
}
