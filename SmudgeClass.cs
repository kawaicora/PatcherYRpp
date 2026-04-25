using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using PatcherYRpp.FileFormats;
using DynamicPatcher;

namespace PatcherYRpp
{

    [StructLayout(LayoutKind.Explicit)]
    public struct SmudgeClass
    {
        public const nint array = 0xA8B1E0;
        public static ref DynamicVectorClass<Pointer<SmudgeClass>> Array => ref array.Convert<DynamicVectorClass<Pointer<SmudgeClass>>>().Ref;
        
        [FieldOffset(0)] public ObjectClass Base;
        [FieldOffset(172)] public nint type;
        public Pointer<SmudgeTypeClass> Type => type;
    }
}