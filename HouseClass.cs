using DynamicPatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 90296)]
    public struct HouseClass : IOwnAbstractType<HouseTypeClass>
    {
        static public readonly IntPtr ArrayPointer = new IntPtr(0xA80228);
        static public ref DynamicVectorClass<Pointer<HouseClass>> Array { get => ref DynamicVectorClass<Pointer<HouseClass>>.GetDynamicVector(ArrayPointer); }

        public static Pointer<HouseClass> Player { get => player.Convert<Pointer<HouseClass>>().Data; set => player.Convert<Pointer<HouseClass>>().Ref = value; }
        private static IntPtr player = new IntPtr(0xA83D4C);
        public static Pointer<HouseClass> Observer { get => observer.Convert<Pointer<HouseClass>>().Data; set => observer.Convert<Pointer<HouseClass>>().Ref = value; }
        private static IntPtr observer = new IntPtr(0xAC1198);

        Pointer<HouseTypeClass> IOwnAbstractType<HouseTypeClass>.OwnType => Type;
        Pointer<AbstractTypeClass> IOwnAbstractType.AbstractType => Type.Convert<AbstractTypeClass>();


        public static unsafe IntPtr HouseClass_CTOR(Pointer<HouseTypeClass> country)
        {
            // 分配对象内存（游戏标准分配方式）
            IntPtr pThis = Marshal.AllocHGlobal(Marshal.SizeOf<HouseClass>()); 
            // 构造函数地址 0x4F54A0
            var func = (delegate* unmanaged[Thiscall]<IntPtr, IntPtr, IntPtr>)0x4F54A0;

            // 静态方法没有 this，直接传分配的对象指针
            return func(pThis, country);
        }
        // HouseClass is too large that clr could not process. so we user Pointer instead.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Pointer<HouseClass> GetThis() => this.GetThisPointer();

        public Pointer<SuperClass> FindSuperWeapon(Pointer<SuperWeaponTypeClass> pType)
        {
            for (int i = 0; i < Supers.Count; i++)
            {
                var pItem = Supers[i];
                if (pItem.Ref.Type == pType)
                {
                    return pItem;
                }
            }

            return Pointer<SuperClass>.Zero;
        }

        public unsafe void Win(bool bSavourSomething)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, Bool, void>)0x4FC9E0;
            func(GetThis(), bSavourSomething);
        }

        public unsafe void Lose(bool bSavourSomething)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, Bool, void>)0x4FCBD0;
            func(GetThis(), bSavourSomething);
        }
        public unsafe bool IsAlliedWith(int idxHouse)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, int, Bool>)0x4F9A10;
            return func(GetThis(), idxHouse);
        }
        public unsafe bool IsAlliedWith(Pointer<HouseClass> pHouse)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, IntPtr, Bool>)0x4F9A50;
            return func(GetThis(), pHouse);
        }
        public unsafe bool IsAlliedWith(Pointer<ObjectClass> pObject)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, IntPtr, Bool>)0x4F9A90;
            return func(GetThis(), pObject);
        }
        public unsafe bool IsAlliedWith(Pointer<AbstractClass> pAbstract)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, IntPtr, Bool>)0x4F9AF0;
            return func(GetThis(), pAbstract);
        }

        public unsafe bool ControlledByHuman()
        {
            bool result = CurrentPlayer;
            return result;
        }
        public unsafe bool ControlledByPlayer()
        {
            bool result = CurrentPlayer || PlayerControl;
            return result;
        }

        public unsafe void RegisterGain(Pointer<TechnoClass> pTechno, bool ownerChange)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, bool, void>)0x502A80;
            func(this.GetThisPointer(), pTechno, ownerChange);
        }

        public unsafe void RegisterLoss(Pointer<TechnoClass> pTechno, bool keepTiberium)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, bool, void>)0x5025F0;
            func(this.GetThisPointer(), pTechno, keepTiberium);
        }
        public unsafe void TakeMoney(int amount)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, int, void>)0x4F9790;
            func(GetThis(), amount);
        }
        public unsafe void GiveMoney(int amount)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, int, void>)0x4F9950;
            func(GetThis(), amount);
        }

        public void TransactMoney(int amount)
        {
            if (amount > 0)
            {
                GiveMoney(amount);
            }
            else
            {
                TakeMoney(-amount);
            }
        }
      
        public static unsafe Pointer<HouseClass> FindByCountryIndex(int houseType)
        {
            var func = (delegate* unmanaged[Thiscall]<int, IntPtr>)0x502D30;
            return func(houseType);
        }
        public static unsafe Pointer<HouseClass> FindByIndex(int idxHouse)
        {
            var func = (delegate* unmanaged[Thiscall]<int, IntPtr>)0x510ED0;
            return func(idxHouse);
        }
        public static unsafe int FindIndexByName(AnsiString name)
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, int>)0x50C170;
            return func(name);
        }

        // gets the first house of a type with this name
        public static Pointer<HouseClass> FindByCountryName(AnsiString name)
        {
            var idx = HouseTypeClass.FindIndexOfName(name);
            return FindByCountryIndex(idx);
        }

        // gets the first house of a type with name Neutral
        public static Pointer<HouseClass> FindNeutral()
        {
            return FindByCountryName("Neutral");
        }

        // gets the first house of a type with name Special
        public static Pointer<HouseClass> FindSpecial()
        {
            return FindByCountryName("Special");
        }

        // gets the first house of a side with this name
        public static Pointer<HouseClass> FindBySideIndex(int index)
        {
            foreach (var pHouse in Array)
            {
                if (pHouse.Ref.Type.Ref.SideIndex == index)
                {
                    return pHouse;
                }
            }
            return Pointer<HouseClass>.Zero;
        }
        public unsafe bool Fire_ParaDrop(Pointer<SuperClass> pSuper)
        {
            return ((delegate* unmanaged[Thiscall]<nint, Pointer<SuperClass>, bool>)0x509CD0)(
                this.GetStructPointer(), pSuper);
        }
        
        // gets the first house of a type with this name
        public static Pointer<HouseClass> FindBySideName(AnsiString name)
        {
            var idx = SideClass.ABSTRACTTYPE_ARRAY.FindIndex(name);
            return FindBySideIndex(idx);
        }

        // gets the first house of a type from the Civilian side
        public static Pointer<HouseClass> FindCivilianSide()
        {
            return FindBySideName("Civilian");
        }
        
        /// <summary>
        /// 获取主工厂：返回一个指向主工厂的指针，该工厂是当前阵营的主要生产设施，通常用于生产单位和建筑。
        /// </summary>
        /// <param name="absID"></param>
        /// <param name="naval"> 是否为海军工厂</param>
        /// <param name="buildCat"></param>
        /// <returns></returns>
        public unsafe Pointer<FactoryClass> GetPrimaryFactory(AbstractType absID, bool naval, BuildCat buildCat)   //建造时出现
        {
            var func = (delegate* unmanaged[Thiscall]<nint, AbstractType, bool, BuildCat, nint>)0x500510;
            return func(this.GetThisPointer(), absID, naval, buildCat);
        }

        /// <summary>
        /// 设置主工厂：将当前阵营的主工厂设置为指定的工厂实例。参数pFactory是一个指向FactoryClass实例的指针，absID指定工厂的抽象类型，naval表示是否为海军工厂，buildCat指定建造类别。这个方法可以用来更改当前阵营的主工厂，例如在特定条件下切换生产设施或调整生产策略时。
        /// </summary>
        /// <param name="pFactory"></param>
        /// <param name="absID"></param>
        /// <param name="naval">是否为海军工厂</param>
        /// <param name="buildCat"></param>
        public unsafe void SetPrimaryFactory(Pointer<FactoryClass> pFactory, AbstractType absID, bool naval, BuildCat buildCat)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, AbstractType, bool, BuildCat, void>)0x500850;
            func(this.GetThisPointer(), pFactory, absID, naval, buildCat);
        }


        // Target ought to be Object, I imagine, but cell doesn't work then
        public unsafe void SendSpyPlanes(int AircraftTypeIdx, int AircraftAmount, Mission SetMission, Pointer<AbstractClass> Target, Pointer<ObjectClass> Destination)
        {
            var func = (delegate* unmanaged[Thiscall]<int, IntPtr, int, int, Mission, IntPtr, IntPtr, void>)ASM.FastCallTransferStation;
            func(0x65EAB0, GetThis(), AircraftTypeIdx, AircraftAmount, SetMission, Target, Destination);
        }

        public unsafe Edge GetCurrentEdge()
        {
            var func = (delegate* unmanaged[Thiscall]<IntPtr, Edge>)0x50DA80;
            return func(GetThis());
        }
        public unsafe Edge GetStartingEdge()
        {
            var edge = this.StartingEdge;
            if (edge < Edge.North || edge > Edge.West)
                edge = this.GetCurrentEdge();
            return edge;
        }

        public unsafe int Available_Money()
        {
            var func = (delegate* unmanaged[Stdcall]<IntPtr, int>)this.GetVirtualFunctionPointer(6, tableIndex: 9);
            return func(GetThis().RawOffset(36));
        }  


        public unsafe Pointer<FactoryClass> GetFactoryProducing(Pointer<TechnoTypeClass> pItem)
        {
            return ((delegate* unmanaged[Thiscall]<nint, Pointer<TechnoTypeClass>, Pointer<FactoryClass>>)0x4F83C0)(
                this.GetThisPointer(), pItem);
        }


        /// <summary>
        /// 获取所有阵营
        /// </summary>
        /// <returns></returns>
        public static List<Pointer<HouseClass>> GetAllHouse(bool bFilterLose = false, bool bFilterNotBuilder = true, bool bFilterNeutral = false ,bool bFilterSpecial = false)
        {
            List<Pointer<HouseClass>>  HousePtrList = new List<Pointer<HouseClass>>();

            foreach (var house in HouseClass.Array)
            {
                if (house.IsNotNull)
                {
                    if (house.Ref.Type.IsNull)
                    {
                        Logger.Log("GetAllHouse: house type is null, skipping this house");
                        continue;
                    }
                    if (bFilterLose && house.Ref.IsLoser)
                    {
                       
                        continue;
                    }

                    if (bFilterNeutral && house == HouseClass.FindNeutral())
                    {
                        
                        continue;
                    }
                    if (bFilterSpecial && house == HouseClass.FindSpecial())
                    {
                        
                        continue;
                    }

                    if (bFilterNotBuilder && house.Ref.Buildings.Count == 0)
                    {
                        
                        continue;
                    }


                    HousePtrList.Add(house);
                }
            }
            return  HousePtrList;
        }

        /// <summary>
        /// 获取所有敌对阵营
        /// </summary>
        /// <returns></returns>
        public static List<Pointer<HouseClass>> GetTargetAllEnemyHouse(Pointer<HouseClass> target, bool bFilterNeutral = true ,bool bFilterSpecial = true,bool bFilterDefeated = true)
        {
            List<Pointer<HouseClass>> enemyHousePtrList = new List<Pointer<HouseClass>>();

            foreach (var house in HouseClass.Array)
            {
                if (house.IsNotNull && !house.Ref.IsAlliedWith(target) && house != target)
                {
                    if (house.Ref.Type.IsNull)
                    {
                        // Logger.Log("GetAllEnemyHouse: house type is null, skipping this house");
                        continue;
                    }
                   

                    if (bFilterNeutral && house == HouseClass.FindNeutral())
                    {
                        // Logger.Log("GetAllEnemyHouse: 跳过平民阵营");
                        continue;
                    }
                    if (bFilterSpecial && house == HouseClass.FindSpecial())
                    {
                        // Logger.Log("GetAllEnemyHouse: 跳过治安官阵营");
                        continue;
                    }
                    if (bFilterDefeated && house.Ref.Defeated)
                    {
                        continue;
                    }
               
                    enemyHousePtrList.Add(house);
                }
            }
            return enemyHousePtrList;
        }
        
        public CellStruct GetBaseCenter()
        {
            if(GetThis().IsNull)
            {
                return CellStruct.Empty;
            }
            if(GetThis().Ref.BaseCenter == CellStruct.Empty)
            {
                
                return GetThis().Ref.BaseSpawnCell;
            }
            return GetThis().Ref.BaseCenter;
        }

        [FieldOffset(48)] public int ArrayIndex;

        [FieldOffset(52)] public Pointer<HouseTypeClass> Type;
        [FieldOffset(56)] DynamicVectorClass<Pointer<TagClass>> RelatedTags;

        [FieldOffset(80)] public byte conyards;
        public ref DynamicVectorClass<Pointer<BuildingClass>> ConYards => ref Pointer<byte>.AsPointer(ref conyards).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(104)] public byte buildings;
        public ref DynamicVectorClass<Pointer<BuildingClass>> Buildings => ref Pointer<byte>.AsPointer(ref buildings).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        
        [FieldOffset(128)] public byte unitRepairStations;
        public ref DynamicVectorClass<Pointer<BuildingClass>> UnitRepairStations => ref Pointer<byte>.AsPointer(ref unitRepairStations).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(152)] public byte grinders;
        public ref DynamicVectorClass<Pointer<BuildingClass>> Grinders => ref Pointer<byte>.AsPointer(ref grinders).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(176)] public byte absorbers;
        public ref DynamicVectorClass<Pointer<BuildingClass>> Absorbers => ref Pointer<byte>.AsPointer(ref absorbers).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;
        [FieldOffset(195)] public int Money;
        


        [FieldOffset(200)] public byte bunkers;
        public ref DynamicVectorClass<Pointer<BuildingClass>> Bunkers => ref Pointer<byte>.AsPointer(ref bunkers).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(224)] public byte occupiables;
        public ref DynamicVectorClass<Pointer<BuildingClass>> Occupiables => ref Pointer<byte>.AsPointer(ref occupiables).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(248)] public byte cloningVats;
        public ref DynamicVectorClass<Pointer<BuildingClass>> CloningVats => ref Pointer<byte>.AsPointer(ref cloningVats).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(272)] public byte secretLabs;
        public ref DynamicVectorClass<Pointer<BuildingClass>> SecretLabs => ref Pointer<byte>.AsPointer(ref secretLabs).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(296)] public byte psychicDetectionBuildings;
        public ref DynamicVectorClass<Pointer<BuildingClass>> PsychicDetectionBuildings => ref Pointer<byte>.AsPointer(ref psychicDetectionBuildings).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;

        [FieldOffset(320)] public byte factoryPlants;
        public ref DynamicVectorClass<Pointer<BuildingClass>> FactoryPlants => ref Pointer<byte>.AsPointer(ref factoryPlants).Convert<DynamicVectorClass<Pointer<BuildingClass>>>().Ref;


        [FieldOffset(388)] public uint AIDifficulty;
        [FieldOffset(392)] public double FirepowerMultiplier;
        [FieldOffset(400)] public double GroundspeedMultiplier;
        [FieldOffset(408)] public double AirspeedMultiplier;
        [FieldOffset(416)] public double ArmorMultiplier;
        [FieldOffset(424)] public double ROFMultiplier;
        [FieldOffset(432)] public double CostMultiplier;
        [FieldOffset(440)] public double BuildTimeMultiplier;
        [FieldOffset(464)] public uint IQLevel;
        [FieldOffset(468)] public uint TechLevel;
        [FieldOffset(480)] public Edge StartingEdge;
        [FieldOffset(492)] public Bool CurrentPlayer;
        [FieldOffset(493)] public Bool PlayerControl;
        [FieldOffset(494)] public Bool Production;
        [FieldOffset(495)] public Bool AutocreateAllowed;
        [FieldOffset(501)] public Bool Defeated;
        [FieldOffset(502)] public Bool IsGameOver;
        [FieldOffset(503)] public Bool IsWinner;
        [FieldOffset(504)] public Bool IsLoser;
        [FieldOffset(508)] public Bool RecheckTechTree;
        
        [FieldOffset(588)] public uint IQLevel2;
        [FieldOffset(596)] public DynamicVectorClass<Pointer<SuperClass>> Supers;


        

        [FieldOffset(21368)] public int NumAirpads;

        [FieldOffset(21372)] public int NumBarracks;

        [FieldOffset(21376)] public int NumWarFactories;

        [FieldOffset(21380)] public int NumConYards;

        [FieldOffset(21384)] public int NumShipyards;

        [FieldOffset(21388)] public int NumOrePurifiers;

        [FieldOffset(21412)] public int PowerOutput;
        [FieldOffset(21416)] public int PowerDrain;



        
        [FieldOffset(21420)] public nint primary_ForAircraft;
        public readonly Pointer<FactoryClass> PrimaryForAircraft => primary_ForAircraft;
        [FieldOffset(21424)] public nint primary_ForInfantry;
        public readonly Pointer<FactoryClass> PrimaryForInfantry => primary_ForInfantry;
        [FieldOffset(21428)] public nint primary_ForVehicles;
        public readonly Pointer<FactoryClass> PrimaryForVehicles => primary_ForVehicles;
        [FieldOffset(21432)] public nint primary_ForShips;
        public readonly Pointer<FactoryClass> PrimaryForShips => primary_ForShips;
        [FieldOffset(21436)] public nint primary_ForBuildings;
        public readonly Pointer<FactoryClass> PrimaryForBuildings => primary_ForBuildings;

        [FieldOffset(21452)] public nint primary_ForDefenses;
        public readonly Pointer<FactoryClass> PrimaryForDefenses => primary_ForDefenses;

        [FieldOffset(21556)]public int TotalKilledUnits;

        [FieldOffset(21640)] public int TotalKilledBuildings;

        [FieldOffset(21648)] public CellStruct BaseSpawnCell;
        [FieldOffset(21652)] public CellStruct BaseCenter;

        [FieldOffset(21736)] public int SiloMoney;

        [FieldOffset(22265)] public ColorStruct Color;

        [FieldOffset(22268)] public ColorStruct LaserColor;

        [FieldOffset(22396)] public Edge Edge;


        
    }
}
