using System.Runtime.InteropServices;

namespace System.Threading
{
    static class Thread
    {
        [DllImport("api-ms-win-core-synch-l1-2-0")]
        public static extern void Sleep(int delayMs);
    }
}