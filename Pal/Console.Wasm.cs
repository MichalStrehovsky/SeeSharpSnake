using System.Runtime.InteropServices;

namespace System
{
    static class Console
    {
        private enum BOOL : int
        {
            FALSE = 0,
            TRUE = 1,
        }

        public static unsafe string Title
        {
            set
            {
                /*
                fixed (char* c = value)
                    SetConsoleTitle(c);
                    */
                // set dom title
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CONSOLE_CURSOR_INFO
        {
            public uint Size;
            public BOOL Visible;
        }

        public static unsafe bool CursorVisible
        {
            set
            {
                CONSOLE_CURSOR_INFO cursorInfo = new CONSOLE_CURSOR_INFO
                {
                    Size = 1,
                    Visible = value ? BOOL.TRUE : BOOL.FALSE
                };
//TODO: whats this for?/
                // SetConsoleCursorInfo(s_outputHandle, &cursorInfo);
            }
        }

        static ConsoleColor foregroundColor;
        public static ConsoleColor ForegroundColor
        {
            set
            {
                foregroundColor = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEY_EVENT_RECORD
        {
            public BOOL KeyDown;
            public short RepeatCount;
            public short VirtualKeyCode;
            public short VirtualScanCode;
            public short UChar;
            public int ControlKeyState;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT_RECORD
        {
            public short EventType;
            public KEY_EVENT_RECORD KeyEvent;
        }

        [DllImport("*")]
        extern static int js_key_available();
        public static unsafe bool KeyAvailable
        {
            get
            {

                return js_key_available() != 0;
                /*
                uint nRead;
                INPUT_RECORD buffer;
                while (true)
                {
                     //EMSCRIPTEN_RESULT ret = emscripten_set_keypress_callback(EMSCRIPTEN_EVENT_TARGET_WINDOW, 0, 1, key_callback);
                    PeekConsoleInput(s_inputHandle, &buffer, 1, &nRead);

                    if (nRead == 0)
                        return false;

                    if (buffer.EventType == 1 && buffer.KeyEvent.KeyDown != BOOL.FALSE)
                        return true;

                    ReadConsoleInput(s_inputHandle, &buffer, 1, &nRead);
                }
                */
            }
        }

        [DllImport("*")]
        extern static int js_read_key();
        public static unsafe ConsoleKeyInfo ReadKey(bool intercept)
        {
            /*
            uint nRead;
            INPUT_RECORD buffer;
            do
            {
                ReadConsoleInput(s_inputHandle, &buffer, 1, &nRead);
            }
            while (buffer.EventType != 1 || buffer.KeyEvent.KeyDown == BOOL.FALSE);


            return new ConsoleKeyInfo((char)Event.UChar, (ConsoleKey)buffer.KeyEvent.VirtualKeyCode, false, false, false);
            */
            return new ConsoleKeyInfo((char)0, (ConsoleKey)js_read_key(), false, false, false);
        }

        struct SMALL_RECT
        {
            public short Left, Top, Right, Bottom;
        }

        [DllImport("*")]
        extern static void js_build_table(int x, int y);

        public static unsafe void SetWindowSize(int x, int y)
        {
            SMALL_RECT rect = new SMALL_RECT
            {
                Left = 0,
                Top = 0,
                Right = (short)(x - 1),
                Bottom = (short)(y - 1),
            };

            // create dom elements
            js_build_table(x, y);
        }

        [StructLayout(LayoutKind.Sequential)]
        struct COORD
        {
            public short X, Y;
        }


        public static void SetBufferSize(int x, int y)
        {
            //TODO : delete?
//            SetConsoleScreenBufferSize(s_outputHandle, new COORD { X = (short)x, Y = (short)y });
        }


        static int _x, _y;
        public static void SetCursorPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }


        [DllImport("*")]
        extern static void js_write_char(int x, int y, int c);
        public static unsafe void Write(char c)
        {
            if(c == ' ') c = (char)160; // nbsp
            js_write_char(_x, _y, (int)c);
            _x++;
        }
    }
}
