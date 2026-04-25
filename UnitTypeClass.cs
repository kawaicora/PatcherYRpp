using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 3704)]
    public struct UnitTypeClass
    {
        static public readonly IntPtr ArrayPointer = new IntPtr(0xA83CE0);
        static public YRPP.GLOBAL_DVC_ARRAY<UnitTypeClass> ABSTRACTTYPE_ARRAY = new YRPP.GLOBAL_DVC_ARRAY<UnitTypeClass>(ArrayPointer);

        [FieldOffset(0)] public TechnoTypeClass Base;
        [FieldOffset(0)] public ObjectTypeClass BaseObjectType;
        [FieldOffset(0)] public AbstractTypeClass BaseAbstractType;

    }
}
