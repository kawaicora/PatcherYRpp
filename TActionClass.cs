using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PatcherYRpp
{
        

    [StructLayout(LayoutKind.Explicit)]
    public struct TActionClass
    {
        public const nint array = 0xB0E658;
        public static ref DynamicVectorClass<Pointer<TActionClass>> Array => ref array.Convert<DynamicVectorClass<Pointer<TActionClass>>>().Ref;
        
        [FieldOffset(0)] public AbstractClass Base;
        [FieldOffset(36)] public int ArrayIndex;
        [FieldOffset(40)] public nint nextAction;
        public Pointer<TActionClass> NextAction => nextAction;
        [FieldOffset(44)] public TriggerAction ActionKind;
        [FieldOffset(48)] public nint teamType;
        public Pointer<TeamTypeClass> TeamType => teamType;
        // [FieldOffset(52)] public _ ParmElement;
        [FieldOffset(68)] public int Waypoint;
        [FieldOffset(72)] public int Value2;
        [FieldOffset(76)] public nint tagType;
        public Pointer<TagTypeClass> TagType => tagType;
        [FieldOffset(80)] public nint triggerType;
        public Pointer<TriggerTypeClass> TriggerType => triggerType;
        [FieldOffset(84)] public byte technoID;
        public FixedArray<byte> TechnoID => new(ref Unsafe.As<byte, byte>(ref technoID), 25);
        [FieldOffset(109)] public byte text;
        public FixedArray<byte> Text => new(ref Unsafe.As<byte, byte>(ref text), 32);
        [FieldOffset(144)] public int Value;

    }

    public enum TriggerAction : uint
    {
        None = 0x0,
        Win = 0x1,
        Lose = 0x2,
        ProductionBegins = 0x3,
        CreateTeam = 0x4,
        DestroyTeam = 0x5,
        AllToHunt = 0x6,
        Reinforcement = 0x7,
        DropZoneFlare = 0x8,
        FireSale = 0x9,
        PlayMovie = 0xA,
        TextTrigger = 0xB,
        DestroyTrigger = 0xC,
        AutocreateBegins = 0xD,
        ChangeHouse = 0xE,
        AllowWin = 0xF,
        RevealAllMap = 0x10,
        RevealAroundWaypoint = 0x11,
        RevealWaypointZone = 0x12,
        PlaySoundEffect = 0x13,
        PlayMusicTheme = 0x14,
        PlaySpeech = 0x15,
        ForceTrigger = 0x16,
        TimerStart = 0x17,
        TimerStop = 0x18,
        TimerExtend = 0x19,
        TimerShorten = 0x1A,
        TimerSet = 0x1B,
        GlobalSet = 0x1C,
        GlobalClear = 0x1D,
        AutoBaseBuilding = 0x1E,
        GrowShroud = 0x1F,
        DestroyAttachedObject = 0x20,
        AddOneTimeSuperWeapon = 0x21,
        AddRepeatingSuperWeapon = 0x22,
        PreferredTarget = 0x23,
        AllChangeHouse = 0x24,
        MakeAlly = 0x25,
        MakeEnemy = 0x26,
        ChangeZoomLevel = 0x27,
        ResizePlayerView = 0x28,
        PlayAnimAt = 0x29,
        DoExplosionAt = 0x2A,
        CreateVoxelAnim = 0x2B,
        IonStormStart = 0x2C,
        IonStormStop = 0x2D,
        LockInput = 0x2E,
        UnlockInput = 0x2F,
        MoveCameraToWaypoint = 0x30,
        ZoomIn = 0x31,
        ZoomOut = 0x32,
        ReshroudMap = 0x33,
        ChangeLightBehavior = 0x34,
        EnableTrigger = 0x35,
        DisableTrigger = 0x36,
        CreateRadarEvent = 0x37,
        LocalSet = 0x38,
        LocalClear = 0x39,
        MeteorShower = 0x3A,
        ReduceTiberium = 0x3B,
        SellBuilding = 0x3C,
        TurnOffBuilding = 0x3D,
        TurnOnBuilding = 0x3E,
        Apply100Damage = 0x3F,
        SmallLightFlash = 0x40,
        MediumLightFlash = 0x41,
        LargeLightFlash = 0x42,
        AnnounceWin = 0x43,
        AnnounceLose = 0x44,
        ForceEnd = 0x45,
        DestroyTag = 0x46,
        SetAmbientStep = 0x47,
        SetAmbientRate = 0x48,
        SetAmbientLight = 0x49,
        AiTriggersBegin = 0x4A,
        AiTriggersStop = 0x4B,
        RatioOfAiTriggerTeams = 0x4C,
        RatioOfTeamAircraft = 0x4D,
        RatioOfTeamInfantry = 0x4E,
        RatioOfTeamUnits = 0x4F,
        ReinforcementAt = 0x50,
        WakeupSelf = 0x51,
        WakeupAllSleepers = 0x52,
        WakeupAllHarmless = 0x53,
        WakeupGroup = 0x54,
        VeinGrowth = 0x55,
        TiberiumGrowth = 0x56,
        IceGrowth = 0x57,
        ParticleAnim = 0x58,
        RemoveParticleAnim = 0x59,
        LightningStrike = 0x5A,
        GoBerzerk = 0x5B,
        ActivateFirestorm = 0x5C,
        DeactivateFirestorm = 0x5D,
        IonCannonStrike = 0x5E,
        NukeStrike = 0x5F,
        ChemMissileStrike = 0x60,
        ToggleTrainCargo = 0x61,
        PlaySoundEffectRandom = 0x62,
        PlaySoundEffectAtWaypoint = 0x63,
        PlayIngameMovie = 0x64,
        ReshroudMapAtWaypoint = 0x65,
        LightningStormStrike = 0x66,
        TimerText = 0x67,
        FlashTeam = 0x68,
        TalkBubble = 0x69,
        SetObjectTechLevel = 0x6A,
        ReinforcementByChrono = 0x6B,
        CreateCrate = 0x6C,
        IronCurtain = 0x6D,
        PauseGame = 0x6E,
        EvictOccupiers = 0x6F,
        CenterCameraAtWaypoint = 0x70,
        MakeHouseCheer = 0x71,
        SetTabTo = 0x72,
        FlashCameo = 0x73,
        StopSounds = 0x74,
        PlayIngameMovieAndPause = 0x75,
        ClearAllSmudges = 0x76,
        DestroyAll = 0x77,
        DestroyAllBuildings = 0x78,
        DestroyAllLandUnits = 0x79,
        DestroyAllNavalUnits = 0x7A,
        MindControlBase = 0x7B,
        RestoreMindControlledBase = 0x7C,
        CreateBuilding = 0x7D,
        RestoreStartingUnits = 0x7E,
        StartChronoScreenEffect = 0x7F,
        TeleportAll = 0x80,
        SetSuperWeaponCharge = 0x81,
        RestoreStartingBuildings = 0x82,
        FlashBuildingsOfType = 0x83,
        SuperWeaponSetRechargeTime = 0x84,
        SuperWeaponResetRechargeTime = 0x85,
        SuperWeaponReset = 0x86,
        SetPreferredTargetCell = 0x87,
        ClearPreferredTargetCell = 0x88,
        SetBaseCenterCell = 0x89,
        ClearBaseCenterCell = 0x8A,
        BlackoutRadar = 0x8B,
        SetDefensiveTargetCell = 0x8C,
        ClearDefensiveTargetCell = 0x8D,
        RetintRed = 0x8E,
        RetintGreen = 0x8F,
        RetintBlue = 0x90,
        JumpCameraHome = 0x91
    }

}
