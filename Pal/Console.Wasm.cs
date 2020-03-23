using System.Runtime.InteropServices;

namespace System
{
    static unsafe class Console
    {
        [DllImport("*")]
        extern static void js_set_title(char *c);
        public static unsafe string Title
        {
            set
            {
                // set dom title
                fixed (char* c = value)
                   js_set_title(c);
            }
        }

        public static unsafe bool CursorVisible
        {
            set
            {
                // html table has no cursor
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

        [DllImport("*")]
        extern static int js_key_available();
        public static unsafe bool KeyAvailable
        {
            get
            {
                return js_key_available() != 0;
            }
        }

        [DllImport("*")]
        extern static void emscripten_sleep(uint ms);
        [DllImport("*")]
        extern static int js_read_key();
        public static unsafe ConsoleKeyInfo ReadKey(bool intercept)
        {
            int k;
            if (intercept)
            {
                while ((k = js_read_key()) == 0)
                {
                    emscripten_sleep(100);
                }
            }
            else k = js_read_key();

            return new ConsoleKeyInfo((char)0, (ConsoleKey)k, false, false, false);
        }

        [DllImport("*")]
        extern static void js_build_table(int x, int y);

        public static unsafe void SetWindowSize(int x, int y)
        {
            // create dom elements
            js_build_table(x, y);
        }

        public static void SetBufferSize(int x, int y)
        {
        }

        [DllImport("*")]
        extern static void js_print_int(int i);

        static int _x, _y;
        public static void SetCursorPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        [DllImport("*")]
        extern static void js_write_char(int x, int y, int c, int foregroundColor);
        public static unsafe void Write(char c)
        {
            if(c == ' ') c = (char)160; // nbsp
            js_write_char(_x, _y, (int)c, (int)foregroundColor);
            _x++;
        }
    }
}
