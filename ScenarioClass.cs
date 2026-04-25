using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 41)]
    public struct Variable
    {
        [FieldOffset(0)] public byte Name_first;
        public AnsiStringPointer Name => Pointer<byte>.AsPointer(ref Name_first);
        [FieldOffset(40)] public byte Value;
    };

    [StructLayout(LayoutKind.Explicit, Size = 14144)]
    public struct ScenarioClass
    {
        private static IntPtr instance = new IntPtr(0xA8B230);
        static public ref ScenarioClass Instance { get => ref instance.Convert<Pointer<ScenarioClass>>().Ref.Ref; }

            
        [FieldOffset(0)] public ScenarioFlags SpecialFlags;
        [FieldOffset(4)] public IntPtr nextScenario;
        public AnsiStringPointer NextScenario => new AnsiStringPointer(nextScenario);
        [FieldOffset(264)] public IntPtr altNextScenario;
        public AnsiStringPointer AltNextScenario => new AnsiStringPointer(altNextScenario);
        
        [FieldOffset(524)] public int HomeCell;
        [FieldOffset(528)] public int AltHomeCell;
        [FieldOffset(4480)] public byte houseIndices;
        public FixedArray<int> HouseIndices => new(ref Unsafe.As<byte, int>(ref houseIndices), 16);


        [FieldOffset(4700)] public byte FileName_first;
        public AnsiStringPointer FileName => Pointer<byte>.AsPointer(ref FileName_first);

        [FieldOffset(7304)] public Variable GlobalVariables_first;
        public Pointer<Variable> GlobalVariables => Pointer<Variable>.AsPointer(ref GlobalVariables_first);
        [FieldOffset(9354)] public Variable LocalVariables_first;
        public Pointer<Variable> LocalVariables => Pointer<Variable>.AsPointer(ref LocalVariables_first);
    }

    [Flags]
    public enum ScenarioFlags : uint
    {
        bit00 = 1,
        bit01 = 2,
        bit02 = 4,
        bit03 = 8,
        CTFMode = 16,
        Inert = 32,
        TiberiumGrows = 64,
        TiberiumSpreads = 128,
        MCVDeploy = 256,
        InitialVeteran = 512,
        FixedAlliance = 1024,
        HarvesterImmune = 2048,
        FogOfWar = 4096,
        bit13 = 8192,
        TiberiumExplosive = 16384,
        DestroyableBridges = 32768,
        Meteorites = 65536,
        IonStorms = 131072,
        Visceroids = 262144,
        bit19 = 524288,
        bit20 = 1048576,
        bit21 = 2097152,
        bit22 = 4194304,
        bit23 = 8388608,
        bit24 = 16777216,
        bit25 = 33554432,
        bit26 = 67108864,
        bit27 = 134217728,
        bit28 = 268435456,
        bit29 = 536870912,
        bit30 = 1073741824,
        bit31 = 2147483648
    }
}
