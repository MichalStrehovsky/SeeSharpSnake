using System.Runtime.InteropServices;

namespace System
{
    static class Environment
    {
        [DllImport("*")]
        private static extern double emscripten_get_now();

        public static long TickCount64 => (long)(emscripten_get_now() / 1000);
    }
}
