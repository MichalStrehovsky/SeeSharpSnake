using System.Runtime.CompilerServices;

namespace System
{
    static class Environment
    {
        public static unsafe long TickCount64
        {
            // Mark no inlining so that we get "volatile" semantics
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                // Read BIOS data area - 1Ah interrupt counter.
                //
                // Yep, we're just dereferencing memory at some "arbitrary" offset.
                //
                // BIOS data area is a documented data structure laid out by the BIOS
                // when the computer resets. DOS apps are allowed to read it.
                //
                // Offset 0x46C keeps track of time in ~55 ms units.
                uint timerTicks = *(uint*)0x46C;
                return (long)timerTicks * 55;
            }
        }
    }
}
