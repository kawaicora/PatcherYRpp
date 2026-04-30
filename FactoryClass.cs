using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using DynamicPatcher;

namespace PatcherYRpp
{
    [StructLayout(LayoutKind.Explicit, Size = 116)]
    public struct FactoryClass
    {
        static public readonly IntPtr ArrayPointer = new IntPtr(0xA83E30);
        static public ref DynamicVectorClass<Pointer<FactoryClass>> Array { get => ref DynamicVectorClass<Pointer<FactoryClass>>.GetDynamicVector(ArrayPointer); }
        public static unsafe IntPtr FactoryClass_CTOR()
        {
            IntPtr pThis = Marshal.AllocHGlobal(Marshal.SizeOf<FactoryClass>()); 
            var func = (delegate* unmanaged[Thiscall]<nint, IntPtr>)0x4C98B0;
            return func(pThis);
        }

        public static List<Pointer<FactoryClass>> GetAllFactory()
        {
            var factories = new List<Pointer<FactoryClass>>();
            foreach (var factoryPtr in Array)
            {
                if (factoryPtr.IsNotNull)
                {
                    factories.Add(factoryPtr);
                }
            }
            return factories;
        }

        public static int GetCount()
        {
            int count = 0;
            foreach (var factoryPtr in Array)
            {
                if (factoryPtr.IsNotNull)
                {
                    count++;
                }
            }
            return count;
        }



        public bool IsNaval()
        {
            return Object.Convert<AircraftClass>().Ref.BaseTechno.Type.Ref.IsNaval;
        }
        
        /// <summary>
        /// 检查生产进度是否发生变化：如果返回true，表示当前生产的进度已经发生变化，可能是因为生产步骤完成、资源消耗或其他因素导致的；如果返回false，表示当前生产的进度没有发生变化。这个方法可以用来监控生产过程中的动态变化，例如在UI中更新进度条或触发相关事件。
        /// </summary>
        /// <returns></returns>
        public unsafe bool HasProgressChanged()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool>)0x4C9C60;
            return func(this.GetThisPointer());
        }

        /// <summary>
        /// 请求生产：将pType指定的TechnoTypeClass实例添加到生产队列中，并由pOwner指定的HouseClass实例负责生产。如果shouldQueue为true，则将pType添加到队列末尾；如果shouldQueue为false，则将pType插入到当前生产项目之后。这个方法可以用来安排新的生产任务，或者调整现有生产计划。
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pOwner"></param>
        /// <param name="shouldQueue"></param>
        /// <returns></returns>
        public unsafe bool DemandProduction(Pointer<TechnoTypeClass> pType, Pointer<HouseClass> pOwner, bool shouldQueue)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, nint, bool, bool>)0x4C9C70;
            return func(this.GetThisPointer(), pType, pOwner, shouldQueue);
        }


        /// <summary>
        /// 设置生产对象：将当前生产的对象设置为pObject，pObject必须是一个有效的TechnoClass实例。这个方法可以用来更改当前生产的对象，例如在生产过程中需要调整生产目标时。
        /// </summary>
        /// <param name="pObject"></param>
        public unsafe void SetObject(Pointer<TechnoClass> pObject)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, nint, void>)0x4C9E10;
            func(this.GetThisPointer(), pObject);
        }

        /// <summary>
        /// 暂停生产：如果当前生产正在进行中，则暂停生产并保持当前进度；如果当前生产已经被暂停，则无操作。这个方法可以用来临时停止生产过程，例如在资源不足或需要调整生产计划时。
        /// </summary>
        /// <param name="manual"></param>
        /// <returns></returns>
        public unsafe bool Suspend(bool manual)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool, bool>)0x4C9E60;
            return func(this.GetThisPointer(), manual);
        }

        /// <summary>
        /// 恢复生产：如果当前生产被暂停，则恢复生产并继续进行；如果当前生产没有被暂停，则无操作。这个方法可以用来重新开始已经暂停的生产过程。
        /// </summary>
        /// <param name="manual"></param>
        /// <returns></returns>
        public unsafe bool Unsuspend(bool manual)
        {
            var func = (delegate* unmanaged[Thiscall]<nint, bool, bool>)0x4C9EA0;
            return func(this.GetThisPointer(), manual);
        }

        
        /// <summary>
        /// 获取生产所需的总时间：返回当前生产项目完成所需的总时间，单位可能是游戏帧数或秒数。这个值可以用来评估生产的效率和计划生产的时间安排。
        /// </summary>
        /// <returns></returns>
        public unsafe int GetBuildTimeFrames()
        {
            var func = (delegate* unmanaged[Thiscall]<nint, int>)0x4C9FB0;
            return func(this.GetThisPointer());
        }

        /// <summary>
        /// 放弃生产：如果正在生产，则停止生产并移除队列中的第一个项目；如果没有正在生产但队列中有项目，则直接移除队列中的第一个项目；如果两者都没有，则无操作。
        /// </summary>
        /// <returns></returns>
        public unsafe bool AbandonProduction() 
        {
          
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, bool>)0x4C9FF0;
            return func(ref this);
        }

        /// <summary>
        /// 获取生产进度：返回当前生产的完成度，范围从0到1000，其中1000表示完全完成。这个值可以用来判断生产的阶段，例如500表示生产完成了一半。
        /// </summary>
        /// <returns></returns>
        public unsafe int GetProgress()
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, int>)0x4CA120;
            return func(ref this);
        }

        /// <summary>
        /// 检查生产是否完成：如果返回true，表示当前生产的项目已经完成，可以进行收获或开始下一个项目；如果返回false，表示当前生产的项目仍在进行中。
        /// </summary>
        /// <returns></returns>
        public unsafe bool IsDone()
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, bool>)0x4CA130;
            return func(ref this);
        }


        /// <summary>
        /// 获取每步的成本：返回当前生产项目每个步骤的成本，单位可能是资源数量或时间等。这个值可以用来评估生产的效率和资源消耗。
        /// </summary>
        /// <returns></returns>
        public unsafe int GetCostPerStep()
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, int>)0x4CA180;
            return func(ref this);
        }

        /// <summary>
        /// 完成生产：如果当前生产的项目已经完成
        /// </summary>
        /// <returns></returns>
        public unsafe bool CompletedProduction()
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, bool>)0x4CA1A0;
            return func(ref this);
        }

        /// <summary>
        /// 开始生产：如果队列中有项目且当前没有正在生产的项目
        /// </summary>
        public unsafe void StartProduction()
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, void>)0x4CA5A0;
            func(ref this);
        }

        /// <summary>
        /// 从队列中移除一个项目：如果pItem在队列中，则移除它并返回true；如果pItem不在队列中，则返回false且不进行任何操作。这个方法可以用来取消已经排队的生产项目。
        /// </summary>
        /// <param name="pItem"></param>
        /// <returns></returns>
        public unsafe bool RemoveOneFromQueue(Pointer<TechnoTypeClass> pItem)
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, nint, bool>)0x4CA620;
            return func(ref this, pItem);
        }

        /// <summary>
        /// 获取队列中某个项目的数量：返回pType在队列中出现的次数，即pType被排队生产的数量。这个方法可以用来查询特定类型的生产需求。
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public unsafe int CountTotal(Pointer<TechnoTypeClass> pType)
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, nint, int>)0x4CA670;
            return func(ref this, pType);
        }

        /// <summary>
        /// 检查某个项目是否在队列中：如果pType在队列中至少出现一次，则返回true；如果pType不在队列中，则返回false。这个方法可以用来快速判断是否有某个类型的生产需求。
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public unsafe bool IsQueued(Pointer<TechnoTypeClass> pType)
        {
            var func = (delegate* unmanaged[Thiscall]<ref FactoryClass, nint, bool>)0x4CA6B0;
            return func(ref this, pType);
        }


      

        [FieldOffset(0)] public AbstractClass Base;
        [FieldOffset(36)] public ProgressTimer Production;

        [FieldOffset(64)] public byte queuedObjects;
        public ref DynamicVectorClass<Pointer<TechnoTypeClass>> QueuedObjects => ref Pointer<byte>.AsPointer(ref queuedObjects).Convert<DynamicVectorClass<Pointer<TechnoTypeClass>>>().Ref;

        [FieldOffset(88)] public IntPtr _object;
        public Pointer<TechnoClass> Object { get => _object; set => _object = value; }


        [FieldOffset(92)] public byte OnHold;
        [FieldOffset(93)] public byte IsDifferent;
        [FieldOffset(96)] public int Balance;
        [FieldOffset(100)] public int OriginalBalance;
        [FieldOffset(104)] public int SpecialItem;


        [FieldOffset(108)] public nint owner;
        public Pointer<HouseClass> Owner { get => owner; set => owner = value; }


        [FieldOffset(112)] public byte IsSuspended;
        [FieldOffset(113)] public byte IsManual;

        public static Action<Pointer<FactoryClass>> ProgressUpdateCallback ;
        public static Action<Pointer<FactoryClass>> CreateCallback ;
        public static Action<Pointer<FactoryClass>> DemandProductionCallback ;
        public static Action<Pointer<FactoryClass>> SetDurationCallback;   //setDuration之后



        public static unsafe uint FactoryClass_Create_Hook(REGISTERS* R)
        {
            try
            {
                var pItem = (Pointer<FactoryClass>)R->ESI;
                DemandProductionCallback?.Invoke(pItem);
                CreateCallback?.Invoke(pItem);
            }catch(Exception ex){
                Logger.PrintException(ex);
            }
            return 0;
        }
        public static bool CompleteProdution(Pointer<FactoryClass> pFactory)
        {
            if (pFactory.IsNull)
            {
                return false;   
            }
            if (pFactory.Ref.Owner.IsNull)
            {
                return false;
            }
            
            if (pFactory.Ref.Owner.Ref.Money >= pFactory.Ref.Balance)
            {
                pFactory.Ref.Production.Value = 54;
                if (pFactory.Ref.Object.IsNotNull)
                {
                    pFactory.Ref.Owner.Ref.TakeMoney(pFactory.Ref.Balance);
                }
                
                pFactory.Ref.Balance = 0;
                pFactory.Ref.Production.HasChanged = 1;
                pFactory.Ref.IsDifferent = 1;
                pFactory.Ref.IsSuspended = 1;
                pFactory.Ref.Production.Step = Game.CurrentFrame;
                pFactory.Ref.Production.Timer.Duration = 0;
                pFactory.Ref.Production.Timer.Base = new TimerStruct();
            
                return true;
            }
            else
            {
                pFactory.Ref.Production.Value = 53;
                pFactory.Ref.OnHold = 1;
                return false;
            }
        }
        public static unsafe uint FactoryClass_ProgressUpdate_Hook(REGISTERS* R)
        {
            try
            {
                var pItem = (Pointer<FactoryClass>)R->ECX;
                if (pItem.Ref.Production.Value != 54 && pItem.Ref.SpecialItem == -1)
                {
                    ProgressUpdateCallback?.Invoke(pItem);
                }
              
                
            }catch(Exception ex){
                Logger.PrintException(ex);
            }
            return 0;
        }

    }
}