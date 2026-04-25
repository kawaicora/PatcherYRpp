using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DynamicPatcher;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 12508)]
    public struct SessionClass
    {
        private static IntPtr instance = new IntPtr(0xA8B238);
        static public ref SessionClass Instance => ref instance.Convert<SessionClass>().Ref;

        [FieldOffset(0)] public GameMode GameMode;
        [FieldOffset(4)] public Pointer<MPGameModeClass> MPGameMode;
        static public bool IsStandalone()
        {
            return Instance.GameMode == GameMode.Campaign || Instance.GameMode == GameMode.Skirmish ;
        }
    }
}
