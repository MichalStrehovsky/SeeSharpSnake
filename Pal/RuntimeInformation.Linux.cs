namespace System.Runtime.InteropServices
{
    internal class RuntimeInformation
    {
        static bool IsOSPlatform(OSPlatform platform) => platform == OSPlatform.Linux;
    }
}
