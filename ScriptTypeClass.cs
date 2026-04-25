using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PatcherYRpp
{
    
    [StructLayout(LayoutKind.Explicit)]
    public struct ScriptTypeClass
    {

        static public readonly IntPtr ArrayPointer = new IntPtr(0x8B41C8);
        static public YRPP.GLOBAL_DVC_ARRAY<ScriptTypeClass> ABSTRACTTYPE_ARRAY = new YRPP.GLOBAL_DVC_ARRAY<ScriptTypeClass>(ArrayPointer);
        [FieldOffset(0)] public AbstractTypeClass Base;
        [FieldOffset(152)] public int ArrayIndex;
        [FieldOffset(156)] public Bool IsGlobal;
        [FieldOffset(160)] public int ActionsCount;
        [FieldOffset(164)] public byte scriptActions;
        public FixedArray<ScriptActionNode> ScriptActions => new(ref Unsafe.As<byte, ScriptActionNode>(ref scriptActions), 50);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ScriptActionNode
    {
        public int Action;
        public int Argument;
    }
}
