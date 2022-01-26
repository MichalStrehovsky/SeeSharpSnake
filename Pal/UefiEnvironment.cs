using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct EFI_HANDLE
{
    private IntPtr _handle;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EFI_TABLE_HEADER
{
    public readonly ulong Signature;
    public readonly uint Revision;
    public readonly uint HeaderSize;
    public readonly uint Crc32;
    public readonly uint Reserved;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_SYSTEM_TABLE
{
    public readonly EFI_TABLE_HEADER Hdr;
    public readonly char* FirmwareVendor;
    public readonly uint FirmwareRevision;
    public readonly EFI_HANDLE ConsoleInHandle;
    public readonly EFI_SIMPLE_TEXT_INPUT_PROTOCOL* ConIn;
    public readonly EFI_HANDLE ConsoleOutHandle;
    public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* ConOut;
    public readonly EFI_HANDLE StandardErrorHandle;
    public readonly EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL* StdErr;
    public readonly EFI_RUNTIME_SERVICES* RuntimeServices;
    public readonly EFI_BOOT_SERVICES* BootServices;
    public readonly ulong NumberOfTableEntries;
    public readonly void* ConfigurationTable;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_RUNTIME_SERVICES
{
    public readonly EFI_TABLE_HEADER Hdr;
    private readonly IntPtr _GetTime;
    private readonly IntPtr _SetTime;
    private readonly IntPtr _GetWakeupTime;
    private readonly IntPtr _SetWakeupTime;
    private readonly IntPtr _SetVirtualAddressMap;
    private readonly IntPtr _ConvertPointer;
    private readonly IntPtr _GetVariable;
    private readonly IntPtr _GetNextVariableName;
    private readonly IntPtr _SetVariable;
    private readonly IntPtr _GetNextHighMonotonicCount;
    private readonly IntPtr _ResetSystem;
    private readonly IntPtr _UpdateCapsule;
    private readonly IntPtr _QueryCapsuleCapabilities;
    private readonly IntPtr _QueryVariableInfo;

    public ulong GetTime(out EFI_TIME time, out EFI_TIME_CAPABILITIES capabilities)
    {
        fixed (EFI_TIME* timeAddress = &time)
        fixed (EFI_TIME_CAPABILITIES* capabilitiesAddress = &capabilities)
            return RawCalliHelper.StdCall(_GetTime, timeAddress, capabilitiesAddress);
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct EFI_TIME
{
    public ushort Year;
    public byte Month;
    public byte Day;
    public byte Hour;
    public byte Minute;
    public byte Second;
    public byte Pad1;
    public uint Nanosecond;
    public short TimeZone;
    public byte Daylight;
    public byte PAD2;
}

[StructLayout(LayoutKind.Sequential)]
public struct SIMPLE_TEXT_OUTPUT_MODE
{
    public readonly int MaxMode;
    public readonly int Mode;
    public readonly int Attribute;
    public readonly int CursorColumn;
    public readonly int CursorRow;
    public readonly bool CursorVisible;
}

[StructLayout(LayoutKind.Sequential)]
public struct EFI_TIME_CAPABILITIES
{
    public uint Resolution;
    public uint Accuracy;
    public bool SetsToZero;
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct EFI_INPUT_KEY
{
    public readonly ushort ScanCode;
    public readonly ushort UnicodeChar;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_SIMPLE_TEXT_INPUT_PROTOCOL
{
    private readonly IntPtr _reset;

    private readonly IntPtr _readKeyStroke;
    public readonly IntPtr WaitForKey;

    public void Reset(void* handle, bool ExtendedVerification)
    {
        RawCalliHelper.StdCall(_reset, (byte*)handle, ExtendedVerification);
    }

    public ulong ReadKeyStroke(void* handle, EFI_INPUT_KEY* Key)
    {
        return RawCalliHelper.StdCall(_readKeyStroke, (byte*)handle, Key);
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_BOOT_SERVICES
{
    readonly EFI_TABLE_HEADER Hdr;
    private readonly IntPtr _RaiseTPL;
    private readonly IntPtr _RestoreTPL;
    private readonly IntPtr _AllocatePages;
    private readonly IntPtr _FreePages;
    private readonly IntPtr _GetMemoryMap;
    private readonly IntPtr _AllocatePool;
    private readonly IntPtr _FreePool;
    private readonly IntPtr _CreateEvent;
    private readonly IntPtr _SetTimer;
    private readonly IntPtr _WaitForEvent;
    private readonly IntPtr _SignalEvent;
    private readonly IntPtr _CloseEvent;
    private readonly IntPtr _CheckEvent;
    private readonly IntPtr _InstallProtocolInterface;
    private readonly IntPtr _ReinstallProtocolInterface;
    private readonly IntPtr _UninstallProtocolInterface;
    private readonly IntPtr _HandleProtocol;
    private readonly IntPtr _Reserved;
    private readonly IntPtr _RegisterProtocolNotify;
    private readonly IntPtr _LocateHandle;
    private readonly IntPtr _LocateDevicePath;
    private readonly IntPtr _InstallConfigurationTable;
    private readonly IntPtr _LoadImage;
    private readonly IntPtr _StartImage;
    private readonly IntPtr _Exit;
    private readonly IntPtr _UnloadImage;
    private readonly IntPtr _ExitBootServices;
    private readonly IntPtr _GetNextMonotonicCount;
    private readonly IntPtr _Stall;
    private readonly IntPtr _SetWatchdogTimer;
    private readonly IntPtr _ConnectController;
    private readonly IntPtr _DisconnectController;
    private readonly IntPtr _OpenProtocol;
    private readonly IntPtr _CloseProtocol;
    private readonly IntPtr _OpenProtocolInformation;
    private readonly IntPtr _ProtocolsPerHandle;
    private readonly IntPtr _LocateHandleBuffer;
    private readonly IntPtr _LocateProtocol;
    private readonly IntPtr _InstallMultipleProtocolInterfaces;
    private readonly IntPtr _UninstallMultipleProtocolInterfaces;
    private readonly IntPtr _CalculateCrc32;
    private readonly IntPtr _CopyMem;
    private readonly IntPtr _SetMem;
    private readonly IntPtr _CreateEventEx;

    public ulong Stall(uint Microseconds)
    {
        return RawCalliHelper.StdCall(_Stall, Microseconds);
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe readonly struct EFI_SIMPLE_TEXT_OUTPUT_PROTOCOL
{
    private readonly IntPtr _reset;
    private readonly IntPtr _outputString;
    private readonly IntPtr _testString;
    private readonly IntPtr _queryMode;
    private readonly IntPtr _setMode;
    private readonly IntPtr _setAttribute;
    private readonly IntPtr _clearScreen;
    private readonly IntPtr _setCursorPosition;
    private readonly IntPtr _enableCursor;

    public readonly SIMPLE_TEXT_OUTPUT_MODE* Mode;

    public ulong OutputString(void* handle, char* str)
    {
        return RawCalliHelper.StdCall(_outputString, (byte*)handle, str);
    }
    public void SetAttribute(void* handle, uint Attribute)
    {
        RawCalliHelper.StdCall(_setAttribute, (byte*)handle, Attribute);
    }
    public void ClearScreen(void* handle)
    {
        RawCalliHelper.StdCall(_clearScreen, (byte*)handle);
    }
    public void SetCursorPosition(void* handle, uint Column, uint Row)
    {
        RawCalliHelper.StdCall(_setCursorPosition, (byte*)handle, Column, Row);
    }
    public void EnableCursor(void* handle, bool Visible)
    {
        RawCalliHelper.StdCall(_enableCursor, (byte*)handle, Visible);
    }
}

public unsafe static class EfiRuntimeHost
{
    public static EFI_SYSTEM_TABLE* SystemTable { get; private set; }

    public static void Initialize(EFI_SYSTEM_TABLE* systemTable)
    {
        SystemTable = systemTable;
    }
}