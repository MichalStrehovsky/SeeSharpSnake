using System;

namespace System.Threading
{
    public static class Thread
    {
        public static unsafe void Sleep(int delayMs)
        {
            // Deliberately change sematics of function, to be able run game
            // Proper implementation should multiply by 1000,
            // But that require change to the calculation of ticks
            // and more UEFI related code.
            EfiRuntimeHost.SystemTable->BootServices->Stall(100 * (uint)delayMs);
        }
    }
}
