namespace System
{
    public static class Console
    {
        public static unsafe string Title
        {
            set
            {
            }
        }

        public static unsafe bool CursorVisible
        {
            set
            {
                EfiRuntimeHost.SystemTable->ConOut->EnableCursor(EfiRuntimeHost.SystemTable->ConOut, value);
            }
        }

        public unsafe static ConsoleColor ForegroundColor
        {
            set
            {
                uint color = (ushort)value;
                EfiRuntimeHost.SystemTable->ConOut->SetAttribute(EfiRuntimeHost.SystemTable->ConOut, color);
            }
        }


        static char lastKey = '\0';
        static ushort lastScanCode;
        public static unsafe bool KeyAvailable
        {
            get
            {
                if (lastKey == '\0')
                {
                    return true;
                }

                EFI_INPUT_KEY key;
                var errorCode = EfiRuntimeHost.SystemTable->ConIn->ReadKeyStroke(EfiRuntimeHost.SystemTable->ConIn, &key);
                lastKey = (char)key.UnicodeChar;
                return errorCode == 0;
            }
        }

        public static unsafe void Clear()
        {
            EfiRuntimeHost.SystemTable->ConOut->ClearScreen(EfiRuntimeHost.SystemTable->ConOut);
        }

        public static unsafe ConsoleKeyInfo ReadKey(bool intercept)
        {
            if (lastKey == '\0')
            {
                EFI_INPUT_KEY key;
                var errorCode = EfiRuntimeHost.SystemTable->ConIn->ReadKeyStroke(EfiRuntimeHost.SystemTable->ConIn, &key);
                lastKey = (char)key.UnicodeChar;
                lastScanCode = key.ScanCode;
            }

            char c = lastKey;

            // Interpret WASD as arrow keys.
            ConsoleKey k = default;
            if (c == 'w')
                k = ConsoleKey.UpArrow;
            else if (c == 'd')
                k = ConsoleKey.RightArrow;
            else if (c == 's')
                k = ConsoleKey.DownArrow;
            else if (c == 'a')
                k = ConsoleKey.LeftArrow;
            if (lastScanCode != 0)
            {
                k = lastScanCode switch
                {
                    1 => ConsoleKey.UpArrow,
                    2 => ConsoleKey.DownArrow,
                    3 => ConsoleKey.RightArrow,
                    4 => ConsoleKey.LeftArrow,
                    _ => k,
                };
            }

            lastKey = '\0';
            return new ConsoleKeyInfo(c, k, false, false, false);
        }

        public static unsafe void SetWindowSize(int x, int y)
        {
        }

        public static void SetBufferSize(int x, int y)
        {
        }

        public unsafe static void SetCursorPosition(int x, int y)
        {
            EfiRuntimeHost.SystemTable->ConOut->SetCursorPosition(
                EfiRuntimeHost.SystemTable->ConOut,
                (uint)x,
                (uint)y);
        }

        unsafe static void WriteChar(EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut, char data)
        {
            // Translate some unicode characters into the IBM hardware codepage
            data = data switch
            {
                '│' => '\u2502',
                '┌' => '\u250c',
                '┐' => '\u2510',
                '─' => '\u2500',
                '└' => '\u2514',
                '┘' => '\u2518',
                _ => data,
            };

            char* x = stackalloc char[2];
            x[0] = data;
            x[1] = '\0';
            ConOut->OutputString(ConOut, x);
        }

        public static unsafe void Write(char c)
        {
            WriteChar(EfiRuntimeHost.SystemTable->ConOut, c);
        }
    }
}
