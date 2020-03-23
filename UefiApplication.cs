using System;
using System.Threading;

public unsafe static class EfiApplication
{
    [System.Runtime.RuntimeExport("EfiMain")]
    unsafe static long EfiMain(IntPtr imageHandle, EFI_SYSTEM_TABLE* systemTable)
    {
        EfiRuntimeHost.Initialize(systemTable);

        // Change to 2 * 1000 once proper time will be implemented.
        Thread.Sleep(40 * 1000);
        Console.Clear();

        Game.Main();
        return 0;
    }
}