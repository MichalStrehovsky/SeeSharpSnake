using System.Runtime.InteropServices;

namespace System.Threading
{
    static class Thread
    {
        [DllImport("*")]
        extern static void emscripten_sleep(uint ms);
        public static void Sleep(int delayMs)
        {
            emscripten_sleep((uint)delayMs);
        }
    }
}
