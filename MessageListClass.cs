using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DynamicPatcher;

namespace PatcherYRpp
{

    [StructLayout(LayoutKind.Explicit,Size = 0x4C)]
    public struct TextLabelClass
    {
        public unsafe void unk_func(Pointer<MessageListClass> messageListClass)
        {
            var func = (delegate* unmanaged[Thiscall]<ref TextLabelClass,IntPtr ,void>)this.GetVirtualFunctionPointer(16);
            func(ref this,messageListClass);
        }
        [FieldOffset(36)] public bool unk_24;
        [FieldOffset(40)] public int unk_28;
        [FieldOffset(56)] public int unk_38;
        [FieldOffset(65)] public bool unk_41;

        /// <summary>
        /// 创建 Message 对象
        /// </summary>
        public static unsafe Pointer<TextLabelClass> AddMessage(UniString text, int x, int y, int color, int style)
        {
            if (string.IsNullOrEmpty(text))
                text = "";

            // RA2/YR 使用 wchar_t*
            // byte[] unicode = Encoding.Unicode.GetBytes(text + "\0");

            var func = (delegate* unmanaged[Thiscall]<IntPtr, string, int, int, int,int, IntPtr>)0x72A440;
            IntPtr p = Marshal.AllocHGlobal(0x4C);
            return func(
                p,
                text,
                x,
                y,
                color,
                style
            );
        }

        
    }
    
    [StructLayout(LayoutKind.Explicit, Size = 1)]
    public struct MessageListClass
    {
        private static IntPtr instance = new IntPtr(0xA8BC60);
        public static ref MessageListClass Instance { get => ref instance.Convert<MessageListClass>().Ref; }
        [FieldOffset(4)] public int startX_;
        [FieldOffset(8)] public int startY_;
        [FieldOffset(12)] public int maxMessages_;
        [FieldOffset(20)] public int lineHeight_;
      

        public ref int StartY =>
            ref Pointer<int>.AsPointer(ref startY_).Convert<int>().Ref;
        public ref int StartX =>
            ref Pointer<int>.AsPointer(ref startX_).Convert<int>().Ref;
        
        public ref int LineHeight =>
            ref Pointer<int>.AsPointer(ref lineHeight_).Convert<int>().Ref;

        public ref int MaxMessages =>
            ref Pointer<int>.AsPointer(ref maxMessages_).Convert<int>().Ref;
        
        // if pLabel is given, the message will be {$pLabel}:{$pMessage}
        // else it will be just {$pMessage}
        public unsafe void PrintMessage(UniString label, int unk1, UniString message, ColorSchemeIndex colorSchemeIndex = ColorSchemeIndex.Yellow, int unk2 = 0x4046, int duration = 0x96, bool silent = false)
        {
            var func = (delegate* unmanaged[Thiscall]<ref MessageListClass, IntPtr, int, IntPtr, ColorSchemeIndex, int, int, Bool, void>)0x5D3BA0;
            func(ref this, label, unk1, message, colorSchemeIndex, unk2, duration, silent);
        }

        public unsafe void PrintMessage(string message, ColorSchemeIndex colorSchemeIndex = ColorSchemeIndex.Yellow, int duration = 0x96, bool silent = false)
        {
            PrintMessage(IntPtr.Zero, 0, message, colorSchemeIndex, 0x4046, duration, silent);
        }

        public unsafe void PrintMessage(string label, string message, ColorSchemeIndex ColorSchemeIndex = ColorSchemeIndex.Yellow, int duration = 0x96, bool silent = false)
        {
            PrintMessage(label, 0, message, ColorSchemeIndex, 0x4046, duration, silent);
        }

        
        
        public static unsafe int MessageBox(
            UniString message,
            UniString button1,
            UniString button2,
            UniString button3
            )
        {
            var func =
                (delegate* unmanaged[Stdcall]<
                    IntPtr,
                    IntPtr,
                    IntPtr,
                    IntPtr,
                    int,
                    int>)0x5D3490;
            return func(
                (IntPtr)message,
                (IntPtr)button1,
                (IntPtr)button2,
                (IntPtr)button3,
                0        
            );
        }

    }
}



