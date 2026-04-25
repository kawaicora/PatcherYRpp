using System.Runtime.InteropServices;

namespace PatcherYRpp
{
    
    public enum TriggerEvent : uint
    {
        None = 0x0,
        EnteredBy = 0x1,
        SpiedBy = 0x2,
        ThievedBy = 0x3,
        DiscoveredByPlayer = 0x4,
        HouseDiscovered = 0x5,
        AttackedByAnybody = 0x6,
        DestroyedByAnybody = 0x7,
        AnyEvent = 0x8,
        DestroyedUnitsAll = 0x9,
        DestroyedBuildingsAll = 0xA,
        DestroyedAll = 0xB,
        CreditsExceed = 0xC,
        ElapsedTime = 0xD,
        MissionTimerExpired = 0xE,
        DestroyedBuildingsNum = 0xF,
        DestroyedUnitsNum = 0x10,
        NoFactoriesLeft = 0x11,
        CiviliansEvacuated = 0x12,
        BuildBuildingType = 0x13,
        BuildUnitType = 0x14,
        BuildInfantryType = 0x15,
        BuildAircraftType = 0x16,
        TeamLeavesMap = 0x17,
        ZoneEntryBy = 0x18,
        CrossesHorizontalLine = 0x19,
        CrossesVerticalLine = 0x1A,
        GlobalSet = 0x1B,
        GlobalCleared = 0x1C,
        DestroyedFakesAll = 0x1D,
        LowPower = 0x1E,
        AllBridgesDestroyed = 0x1F,
        BuildingExists = 0x20,
        SelectedByPlayer = 0x21,
        ComesNearWaypoint = 0x22,
        EnemyInSpotlight = 0x23,
        LocalSet = 0x24,
        LocalCleared = 0x25,
        FirstDamagedCombatonly = 0x26,
        HalfHealthCombatonly = 0x27,
        QuarterHealthCombatonly = 0x28,
        FirstDamagedAnysource = 0x29,
        HalfHealthAnysource = 0x2A,
        QuarterHealthAnysource = 0x2B,
        AttackedByHouse = 0x2C,
        AmbientLightBelow = 0x2D,
        AmbientLightAbove = 0x2E,
        ElapsedScenarioTime = 0x2F,
        DestroyedByAnything = 0x30,
        PickupCrate = 0x31,
        PickupCrateAny = 0x32,
        RandomDelay = 0x33,
        CreditsBelow = 0x34,
        SpyAsHouse = 0x35,
        SpyAsInfantry = 0x36,
        DestroyedUnitsNaval = 0x37,
        DestroyedUnitsLand = 0x38,
        BuildingDoesNotExist = 0x39,
        PowerFull = 0x3A,
        EnteredOrOverflownBy = 0x3B,
        TechTypeExists = 0x3C,
        TechTypeDoesntExist = 0x3D
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct TriggerClass
    {
        public const nint array = 0xA8EAE8;

        public static ref DynamicVectorClass<Pointer<TriggerClass>> Array =>
            ref array.Convert<DynamicVectorClass<Pointer<TriggerClass>>>().Ref;


        public unsafe bool HasCrossesHorizontalLineEvent()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)0x726250;
            return func(this.GetThisPointer());
        }

        public unsafe bool HasCrossesVerticalLineEvent()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)0x726290;
            return func(this.GetThisPointer());
        }

        public unsafe bool HasZoneEntryByEvent()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)0x7262D0;
            return func(this.GetThisPointer());
        }

        public unsafe bool HasAllowWinAction()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)0x726310;
            return func(this.GetThisPointer());
        }

        public unsafe bool HasGlobalSetOrClearedEvent(int idxGlobal)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int, bool>)0x726350;
            return func(this.GetThisPointer(), idxGlobal);
        }

        public unsafe void NotifyGlobalChanged(int idxGlobal)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int, void>)0x7263A0;
            func(this.GetThisPointer(), idxGlobal);
        }

        public unsafe void NotifyLocalChanged(int idxLocal)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int, void>)0x7263D0;
            func(this.GetThisPointer(), idxLocal);
        }

        public unsafe void ResetTimers()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, void>)0x726400;
            func(this.GetThisPointer());
        }

        public unsafe void Destroy()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, void>)0x726720;
            func(this.GetThisPointer());
        }

        public unsafe bool RegisterEvent(TriggerEvent @event, Pointer<ObjectClass> pObject, bool forceFire, bool persistent,
            Pointer<TechnoClass> pSource)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, TriggerEvent, nint, bool, bool, nint, bool>)0x7264C0;
            return func(this.GetThisPointer(), @event, pObject, forceFire, persistent, pSource);
        }

        public unsafe bool FireActions(Pointer<ObjectClass> pObj, CellStruct location)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, CellStruct, bool>)0x7265C0;
            return func(this.GetThisPointer(), pObj, location);
        }

        [FieldOffset(0)] public AbstractClass Base;
        [FieldOffset(36)] public nint type;
        public Pointer<TriggerTypeClass> Type => type;
        [FieldOffset(40)] public nint nextTrigger;
        public Pointer<TriggerClass> NextTrigger => nextTrigger;
        [FieldOffset(44)] public nint house;
        public Pointer<HouseClass> House => house;
        [FieldOffset(48)] public Bool Destoryed;
        [FieldOffset(52)] public TimerStruct Timer;
        [FieldOffset(64)] public uint OccuredEvents;
        [FieldOffset(68)] public Bool Enabled;
    }
}