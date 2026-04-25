using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DynamicPatcher;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit)]
    public struct VocClass
    {
       public const nint array = 0xB1D378;
        public static ref DynamicVectorClass<Pointer<VocClass>> Array => ref array.Convert<DynamicVectorClass<Pointer<VocClass>>>().Ref;
        public const nint voicesEnabled = 0x8464AC;
        public static ref Bool VoicesEnabled => ref voicesEnabled.Convert<Bool>().Ref;


        [FieldOffset(0)] public VocClassHeader Header;
        [FieldOffset(12)] public int SamplesOK;
        [FieldOffset(16)] public SoundControl Control;
        [FieldOffset(20)] public SoundType Type;
        [FieldOffset(24)] public VolumeStruct Volume;
        [FieldOffset(56)] public uint unknown_38;
        [FieldOffset(60)] public uint unknown_3C;
        [FieldOffset(64)] public SoundPriority Priority;
        [FieldOffset(68)] public uint unknown_44;
        [FieldOffset(72)] public int Limit;
        [FieldOffset(76)] public int Loop;
        [FieldOffset(80)] public int Range;
        [FieldOffset(84)] public float MinVolume;
        [FieldOffset(88)] public int MinDelay;
        [FieldOffset(92)] public int MaxDelay;
        [FieldOffset(96)] public int MinFDelta;
        [FieldOffset(100)] public int MaxFDelta;
        [FieldOffset(104)] public int VShift;
        [FieldOffset(108)] public IntPtr name;
        public AnsiStringPointer Name => new AnsiStringPointer(name);
        [FieldOffset(140)] public uint unknown_8C;
        [FieldOffset(144)] public uint unknown_90;
        [FieldOffset(148)] public uint unknown_94;
        [FieldOffset(152)] public uint unknown_98;
        [FieldOffset(156)] public uint unknown_9C;
        [FieldOffset(160)] public uint unknown_A0;
        [FieldOffset(164)] public uint unknown_A4;
        [FieldOffset(168)] public uint unknown_A8;
        [FieldOffset(172)] public uint unknown_AC;
        [FieldOffset(176)] public uint unknown_B0;
        [FieldOffset(180)] public byte sampleIndex;
        public FixedArray<int> SampleIndex => new(ref Unsafe.As<byte, int>(ref sampleIndex), 32);
        [FieldOffset(308)] public int NumSamples;
        [FieldOffset(312)] public int Attack;
        [FieldOffset(316)] public int Decay;
        [FieldOffset(320)] public uint unknown_140;
        [FieldOffset(324)] public uint unknown_144;





        public static unsafe int FindIndex(string soundName)
        {
            for (int i = 0; i < Array.Count; i++)
            {
                Pointer<VocClass> pVoc = Array[i];
                if (pVoc.Ref.GetName() == soundName)
                {
                    return i;
                }

            }
            return -1;
        }

        
        public static unsafe string GetSpeakName(int soundIndex)
        {
            var func = (delegate* unmanaged[Thiscall]<int, int, string>)ASM.FastCallTransferStation;
            return func(0x753330, soundIndex);
        }

        /* Play a Eva's sound.
           n = Index of VocClass in Array to be played */
        public static unsafe void Speak(int soundIndex, VoxType voxType = VoxType.Invalid, VoxPriority voxPriorityType = VoxPriority.Invalid)
        {
            var func = (delegate* unmanaged[Thiscall]<int, int, VoxType, VoxPriority, void>)ASM.FastCallTransferStation;
            func(0x752480, soundIndex, voxType, voxPriorityType);
        }

        /* Play a Eva's sound. */
        public static unsafe void Speak(string name, VoxType voxType = VoxType.Invalid, VoxPriority voxPriorityType = VoxPriority.Invalid)
        {
            var func = (delegate* unmanaged[Thiscall]<int, string, VoxType, VoxPriority, void>)ASM.FastCallTransferStation;
            func(0x752700, name, voxType, voxPriorityType);
        }

        /* Play a sound independant of the position.
           n = Index of VocClass in Array to be played
           Volume = 0.0f to 1.0f
           Panning = 0x0000 (left) to 0x4000 (right) (0x2000 is center)
           */
        public static unsafe void PlayGlobal(int soundIndex, int panning, float volume, Pointer<AudioController> ctrl = default)
        {
            var func = (delegate* unmanaged[Thiscall]<int, int, int, float, IntPtr, void>)ASM.FastCallTransferStation;
            func(0x750920, soundIndex, panning, volume, ctrl);
        }

        /* Play a sound at a certain Position.
               n = Index of VocClass in Array to be played */
        public static unsafe void PlayAt(int soundIndex, CoordStruct location, Pointer<AudioController> ctrl = default)
        {
            var func = (delegate* unmanaged[Thiscall]<int, int, ref CoordStruct, IntPtr, void>)ASM.FastCallTransferStation;
            func(0x7509E0, soundIndex, ref location, ctrl);
        }

        // calls the one above ^ - probably sanity checks and whatnot
        public static unsafe void PlayIndexAtPos(int soundIndex, CoordStruct location, int a3)
        {
            var func = (delegate* unmanaged[Thiscall]<int, int, ref CoordStruct, int, void>)ASM.FastCallTransferStation;
            func(0x750E20, soundIndex, ref location, a3);
        }

        // no idea what this does, but Super::Launch uses it on "SW Ready" events right after firing said SW
        public static unsafe void SilenceIndex(int soundIndex)
        {
            var func = (delegate* unmanaged[Thiscall]<int, int, void>)ASM.FastCallTransferStation;
            func(0x752A40, soundIndex);
        }

        public unsafe string GetName()
        {
            var func = (delegate* unmanaged[Thiscall]<ref VocClass, string>)0x751600;
            return func(ref this);
        }

  

    }
    [StructLayout(LayoutKind.Explicit)]
    public struct VolumeStruct
    {
        [FieldOffset(0)] public int Volume;
        [FieldOffset(4)] public uint unknown_4;
        [FieldOffset(8)] public int unknown_int_8;
        [FieldOffset(12)] public int unknown_int_C;
        [FieldOffset(16)] public int unknown_int_10;
        [FieldOffset(20)] public int unknown_int_14;
        [FieldOffset(24)] public uint unknown_18;
        [FieldOffset(28)] public int unknown_int_1C;

    }
    [StructLayout(LayoutKind.Explicit)]
    public struct VocClassHeader
    {
        [FieldOffset(0)] public nint next;
        public Pointer<VocClassHeader> Next => next;
        [FieldOffset(4)] public nint prev;
        public Pointer<VocClassHeader> Prev => prev;
        [FieldOffset(8)] public uint Magic;
    }


    public enum VoxType :int  {
        Standard = 0,
        Queue = 1,
        Interrupt = 2,
        QueuedInterrupt = 3,

        Invalid = -1

        
    }

    
    public enum VoxPriority : int {
        Low = 0,
        Normal = 1,
        Important = 2,
        Critical = 3,
        Invalid = -1
    };

    public enum WaveType : int {
        Sonic = 0,
        BigLaser = 1,
        Laser = 2,
        Magnetron = 3
    };
}