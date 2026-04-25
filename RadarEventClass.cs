
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit)]
    public struct RadarEventClass
    {
     

        public static unsafe bool Create(RadarEventType nType, CellStruct nMapCoords)
        {
            Pointer<CellStruct> cell = Pointer<CellStruct> .AsPointer(ref nMapCoords);
            var func = (delegate* unmanaged[Fastcall]<int, IntPtr, bool>)0x6FDD50;
            return func((int)nType, cell);
        }

    }
}