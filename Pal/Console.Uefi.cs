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
                EFI_INPUT_KEY key;
                var errorCode = EfiRuntimeHost.SystemTable->ConIn->ReadKeyStroke(EfiRuntimeHost.SystemTable->ConIn, &key);
                lastKey = (char)key.UnicodeChar;
                lastScanCode = key.ScanCode;
                return errorCode == 0;
            }
        }

        public static unsafe void Clear()
        {
            EfiRuntimeHost.SystemTable->ConOut->ClearScreen(EfiRuntimeHost.SystemTable->ConOut);
        }

        public static unsafe ConsoleKeyInfo ReadKey(bool intercept)
        {
            char c = lastKey;
            ConsoleKey k = default;
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
