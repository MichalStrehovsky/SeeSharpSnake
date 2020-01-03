using System.Runtime.InteropServices;

namespace System
{
    static class Environment
    {
        [DllImport("api-ms-win-core-sysinfo-l1-1-0")]
        private static extern long GetTickCount64();

        public static long TickCount64 => GetTickCount64();
    }
}
