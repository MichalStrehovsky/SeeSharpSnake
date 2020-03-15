namespace System.Runtime.InteropServices
{
    internal class RuntimeInformation
    {
        public static bool IsOSPlatform(OSPlatform platform) => platform == OSPlatform.Windows;
    }
}
