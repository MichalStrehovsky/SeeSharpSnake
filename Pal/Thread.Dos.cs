using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
    static class Thread
    {
        public static unsafe void Sleep(int delayMs)
        {
            // Place the sequence of bytes 0xF4, 0xC3 on the stack.
            // The bytes correspond to the following assembly instruction sequence:
            //
            // hlt
            // ret
            //
            ushort hlt = 0xc3f4;

            long expected = Environment.TickCount64 + delayMs;
            while (Environment.TickCount64 < expected)
            {
                // Call the helper we placed on the stack to halt the processor
                // for a little bit.
                // (Security people are crying in a corner right now).
                ClassConstructorRunner.Call<int>(new IntPtr(&hlt));
            }
        }
    }
}
