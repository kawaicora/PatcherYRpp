using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 784)]
    public struct ParticleSystemTypeClass
    {
        static public readonly IntPtr ArrayPointer = new IntPtr(0xA83D68);
        static public YRPP.GLOBAL_DVC_ARRAY<ParticleSystemTypeClass> ABSTRACTTYPE_ARRAY = new YRPP.GLOBAL_DVC_ARRAY<ParticleSystemTypeClass>(ArrayPointer);
        [FieldOffset(0)] public ObjectTypeClass Base;
        [FieldOffset(0)] public AbstractTypeClass BaseAbstractType;

    }
}
