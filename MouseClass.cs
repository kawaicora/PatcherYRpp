using System.Runtime.InteropServices;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct MouseCursor
    {
        public const nint cursors = 0xABEFD8;

        public static FixedArray<MouseCursor> Cursors => new(cursors, 86);

        
        [FieldOffset(0)] public int Frame;
        [FieldOffset(4)] public int Count;
        [FieldOffset(8)] public int Interval;
        [FieldOffset(12)] public int MiniFrame;
        [FieldOffset(16)] public int MiniCount;
        [FieldOffset(20)] public MouseHotSpotX HotX;
        [FieldOffset(24)] public MouseHotSpotY HotY;

    }

    [StructLayout(LayoutKind.Explicit)]
    public struct TabDataClass
    {
        [FieldOffset(0)] public int TargetValue;
        [FieldOffset(4)] public int LastValue;
        [FieldOffset(8)] public Bool NeedsRedraw;
        [FieldOffset(9)] public Bool ValueIncreased;
        [FieldOffset(10)] public Bool ValueChanged;
        [FieldOffset(12)] public int ValueDelta;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct TabClass
    {
        public const nint instance = 0x87F7E8;

        public static ref TabClass Instance => ref instance.Convert<TabClass>().Ref;

        public unsafe void Activate(int control = 1)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int, void>)0x6D04F0;
            func(this.GetStructPointer(), control);
            
        }


        [FieldOffset(0)] public SidebarClass Base;
        [FieldOffset(21784)] public nint INoticeSink;
        [FieldOffset(21788)] public TabDataClass TabData;
        [FieldOffset(21804)] public TimerStruct unknown_timer_552C;
        [FieldOffset(21816)] public TimerStruct InsufficientFundsBlinkTimer;
        [FieldOffset(21828)] public byte unknown_byte_5544;
        [FieldOffset(21829)] public Bool MissionTimerPinged;
        [FieldOffset(21830)] public byte unknown_byte_5546;

    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ScrollClass
    {    
        public const nint instance = 0x87F7E8;

        public static ref ScrollClass Instance => ref instance.Convert<ScrollClass>().Ref;


        [FieldOffset(0)] public TabClass Base;
        [FieldOffset(21832)] public uint unknown_int_5548;
        [FieldOffset(21836)] public byte unknown_byte_554C;
        [FieldOffset(21840)] public uint unknown_int_5550;
        [FieldOffset(21844)] public uint unknown_int_5554;
        [FieldOffset(21848)] public byte unknown_byte_5558;
        [FieldOffset(21849)] public byte unknown_byte_5559;
        [FieldOffset(21850)] public byte unknown_byte_555A;

    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MouseClass
    {    
        public const nint instance = 0x87F7E8;

        public static ref MouseClass Instance => ref instance.Convert<MouseClass>().Ref;


        [FieldOffset(0)] public ScrollClass Base;
        [FieldOffset(21852)] public Bool MouseCursorIsMini;
        [FieldOffset(21856)] public MouseCursorType MouseCursorIndex;
        [FieldOffset(21860)] public MouseCursorType MouseCursorLastIndex;
        [FieldOffset(21864)] public int MouseCursorCurrentFrame;
    }

    public enum MouseCursorType : uint
    {
        Default = 0x0,
        Move_N = 0x1,
        Move_NE = 0x2,
        Move_E = 0x3,
        Move_SE = 0x4,
        Move_S = 0x5,
        Move_SW = 0x6,
        Move_W = 0x7,
        Move_NW = 0x8,
        NoMove_N = 0x9,
        NoMove_NE = 0xA,
        NoMove_E = 0xB,
        NoMove_SE = 0xC,
        NoMove_S = 0xD,
        NoMove_SW = 0xE,
        NoMove_W = 0xF,
        NoMove_NW = 0x10,
        Select = 0x11,
        Move = 0x12,
        NoMove = 0x13,
        Attack = 0x14,
        AttackOutOfRange = 0x15,
        Protect = 0x16,
        DesolatorDeploy = 0x17,
        Cursor_18 = 0x18,
        Enter = 0x19,
        NoEnter = 0x1A,
        Deploy = 0x1B,
        NoDeploy = 0x1C,
        Cursor_1D = 0x1D,
        Sell = 0x1E,
        SellUnit = 0x1F,
        NoSell = 0x20,
        Repair = 0x21,
        EngineerRepair = 0x22,
        NoRepair = 0x23,
        Waypoint = 0x24,
        Disguise = 0x25,
        IvanBomb = 0x26,
        MindControl = 0x27,
        RemoveSquid = 0x28,
        Crush = 0x29,
        SpyTech = 0x2A,
        SpyPower = 0x2B,
        Cursor_2C = 0x2C,
        GIDeploy = 0x2D,
        Cursor_2E = 0x2E,
        ParaDrop = 0x2F,
        Cursor_30 = 0x30, // RallyPoint
        CloseWaypoint = 0x31, // ???
        LightningStorm = 0x32,
        Detonate = 0x33,
        Demolish = 0x34,
        Nuke = 0x35,
        Cursor_36 = 0x36, // BlueMove
        Power = 0x37,
        Cursor_38 = 0x38, // NoBlueMove
        IronCurtain = 0x39,
        Chronosphere = 0x3A,
        Disarm = 0x3B,
        Disallowed = 0x3C,
        Scroll = 0x3D,
        Scroll_ESW = 0x3E,
        Scroll_SW = 0x3F,
        Scroll_NSW = 0x40,
        Scroll_NW = 0x41,
        Scroll_NEW = 0x42,
        Scroll_NE = 0x43,
        Scroll_NES = 0x44,
        Scroll_ES = 0x45,
        Protect2 = 0x46,
        AttackOutOfRange2 = 0x47,
        Cursor_48 = 0x48, // LeaveBuilding
        InfantryAbsorb = 0x49,
        NoMindControl = 0x4A,
        Cursor_4B = 0x4B, // NoRallyPoint
        Cursor_4C = 0x4C,
        Cursor_4D = 0x4D,
        Beacon = 0x4E,
        ForceShield = 0x4F,
        NoForceShield = 0x50,
        GeneticMutator = 0x51,
        AirStrike = 0x52,
        PsychicDominator = 0x53,
        PsychicReveal = 0x54,
        SpyPlane = 0x55
    }

}