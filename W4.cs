using System.Runtime.InteropServices;

namespace SeeSharpSnake
{
    internal unsafe class W4
    {
        internal static readonly byte* GAMEPAD1 = (byte*)0x16;

        internal static readonly byte* FRAMEBUFFER = (byte*)0x00a0;

        internal const byte BUTTON_LEFT = 16;
        internal const byte BUTTON_RIGHT = 32;
        internal const byte BUTTON_UP = 64;
        internal const byte BUTTON_DOWN = 128;

        internal const uint BLIT_1BPP = 0;

        [DllImport("*")]
        internal static extern void blit(byte* data, int x, int y, uint width, uint height, uint flags);

        [DllImport("*")]
        internal static extern void textUtf16(byte* data, uint length, uint x, uint y);

        /** Plays a sound tone. */
        [DllImport("*")]
        internal static extern void tone(uint frequency, uint duration, uint volume, uint flags);
    }
}

namespace System.Runtime.InteropServices
{
    public class UnmanagedCallersOnlyAttribute : Attribute
    {
        public string EntryPoint { get; set; }
    }
}

