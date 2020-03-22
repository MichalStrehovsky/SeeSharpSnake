using System.Runtime.InteropServices;

namespace System.Threading
{
    static class Thread
    {
        [DllImport("*")]
        extern static void emscripten_sleep(uint ms);
//        [DllImport("*")]
//        extern static void js_print_int(int i);
        public static void Sleep(int delayMs)
        {
//            Game.PrintLine("sleep");
//            js_print_int(delayMs);
            emscripten_sleep((uint)delayMs);
        }
    }
}
