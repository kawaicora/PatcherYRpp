using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 108)]
    public struct CCFileClass
    {
        public unsafe AnsiString GetFileName()
        {
            var func = (delegate* unmanaged[Thiscall]<ref CCFileClass, IntPtr>)this.GetVirtualFunctionPointer(1);
            return func(ref this);
        }
        public unsafe bool Exists(bool writeShared = false)
        {
            var func = (delegate* unmanaged[Thiscall]<ref CCFileClass, Bool, Bool>)this.GetVirtualFunctionPointer(5);
            return func(ref this, writeShared);
        }

        public static unsafe void Constructor(Pointer<CCFileClass> pThis, string fileName)
        {
            var func = (delegate* unmanaged[Thiscall]<ref CCFileClass, IntPtr, void>)0x4739F0;
            func(ref pThis.Ref, new AnsiString(fileName));
        }

        public static unsafe void Destructor(Pointer<CCFileClass> pThis)
        {
            var func = (delegate* unmanaged[Thiscall]<ref CCFileClass, Bool, void>)Helpers.GetVirtualFunctionPointer(pThis, 0);
            func(ref pThis.Ref, false);
        }


        public unsafe IntPtr ReadWholeFile()
        {
            var func = (delegate* unmanaged[Thiscall]<ref CCFileClass, IntPtr>)0x4A3890;
            return func(ref this);
        }


        [FieldOffset(12)] public int FilePointer;
        [FieldOffset(16)] public int FileSize;

        [FieldOffset(24)] public AnsiStringPointer FileName;

        [FieldOffset(32)] public Bool FileNameAllocated;
    }

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public struct FileClass
    {
        public unsafe AnsiStringPointer GetFileName()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint>)this.GetVirtualFunctionPointer(1);
            return func(this.GetThisPointer());
        }

        public unsafe AnsiStringPointer SetFileName(Pointer<char> pFileName)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, nint>)this.GetVirtualFunctionPointer(2);
            return func(this.GetThisPointer(), pFileName);
        }

        public unsafe bool CreateFile()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)this.GetVirtualFunctionPointer(3);
            return func(this.GetThisPointer());
        }

        public unsafe bool DeleteFile()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)this.GetVirtualFunctionPointer(4);
            return func(this.GetThisPointer());
        }

        public unsafe bool Exists(bool writeShared = true)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool, bool>)this.GetVirtualFunctionPointer(5);
            return func(this.GetThisPointer(), writeShared);
        }

        public unsafe bool HasHandle()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)this.GetVirtualFunctionPointer(6);
            return func(this.GetThisPointer());
        }

        public unsafe bool Open(FileAccessMode access)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, FileAccessMode, bool>)this.GetVirtualFunctionPointer(7);
            return func(this.GetThisPointer(), access);
        }

        public unsafe bool OpenEx(Pointer<char> pFileName, FileAccessMode access)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, FileAccessMode, bool>)this.GetVirtualFunctionPointer(8);
            return func(this.GetThisPointer(), pFileName, access);
        }

        public unsafe int ReadBytes(nint pBuffer, int nNumBytes)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, int, int>)this.GetVirtualFunctionPointer(9);
            return func(this.GetThisPointer(), pBuffer, nNumBytes);
        }

        public unsafe int Seek(int offset, FileSeekMode seek)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int, FileSeekMode, int>)this.GetVirtualFunctionPointer(10);
            return func(this.GetThisPointer(), offset, seek);
        }

        public unsafe int GetFileSize()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int>)this.GetVirtualFunctionPointer(11);
            return func(this.GetThisPointer());
        }

        public unsafe int WriteBytes(nint pBuffer, int nNumBytes)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, int, int>)this.GetVirtualFunctionPointer(12);
            return func(this.GetThisPointer(), pBuffer, nNumBytes);
        }

        public unsafe void Close()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, void>)this.GetVirtualFunctionPointer(13);
            func(this.GetThisPointer());
        }

        public unsafe uint GetFileTime()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, uint>)this.GetVirtualFunctionPointer(14);
            return func(this.GetThisPointer());
        }

        public unsafe bool SetFileTime(uint FileTime)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, uint, bool>)this.GetVirtualFunctionPointer(15);
            return func(this.GetThisPointer(), FileTime);
        }

        public unsafe void CDCheck(uint errorCode, bool bUnk, Pointer<char> pFilename)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, uint, bool, nint, void>)this.GetVirtualFunctionPointer(16);
            func(this.GetThisPointer(), errorCode, bUnk, pFilename);
        }

        public unsafe nint ReadWholeFile()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint>)0x4A3890;
            return func(this.GetThisPointer());
        }


        [FieldOffset(0)] public nint vftable;
        [FieldOffset(4)] public Bool SkipCDCheck;
        [FieldOffset(5)] public byte padding_5;
        [FieldOffset(6)] public byte padding_6;
        [FieldOffset(7)] public byte padding_7;
    }

    [StructLayout(LayoutKind.Explicit, Size = 36)]
    public struct RawFileClass
    {
        public unsafe void Bias(int offset = 0, int length = -1)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int, int, void>)0x65D2B0;
            func(this.GetThisPointer(), offset, length);
        }

        [FieldOffset(0)] public FileClass Base;
        
        [FieldOffset(4)] public FileAccessMode FileAccess;
        [FieldOffset(8)] public int FilePointer;
        [FieldOffset(12)] public int FileSize;
        [FieldOffset(16)] public nint Handle;
        [FieldOffset(20)] public nint fileName;
        public readonly AnsiStringPointer FileName => fileName;
        [FieldOffset(24)] public short unknown_short_1C;
        [FieldOffset(26)] public short unknown_short_1E;
        [FieldOffset(28)] public Bool FileNameAllocated;

    }

    [Flags]
    public enum  FileAccessMode: uint {
        None = 0,
        Read = 1,
        Write = 2,
        ReadWrite = Read | Write
    }

    public enum FileSeekMode: uint {
        Set = 0, // SEEK_SET
        Current = 1, // SEEK_CUR
        End = 2 // SEEK_END
    }
}
