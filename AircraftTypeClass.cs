using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 3600)]
    public struct AircraftTypeClass
    {   
        static public readonly IntPtr ArrayPointer = new IntPtr(0xA8B218);
        static public YRPP.GLOBAL_DVC_ARRAY<AircraftTypeClass> ABSTRACTTYPE_ARRAY = new YRPP.GLOBAL_DVC_ARRAY<AircraftTypeClass>(ArrayPointer);

        [FieldOffset(0)] public TechnoTypeClass Base;
        [FieldOffset(0)] public ObjectTypeClass BaseObjectType;
        [FieldOffset(0)] public AbstractTypeClass BaseAbstractType;

    }
}
