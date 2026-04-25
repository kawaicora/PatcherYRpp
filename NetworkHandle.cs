using System;
using System.Runtime.InteropServices;

namespace PatcherYRpp
{
    public abstract class NetworkHandle<TArg> : INetworkHandle where TArg : unmanaged
    {
        public abstract byte Index { get; }
        public abstract uint Lenth { get; }
        public abstract string Name { get; }

        public void Respond(Pointer<EventClass> pEvent)
        {
            Pointer<TArg> arg = (IntPtr)pEvent + 7;

            Respond(pEvent, arg);
        }

        protected abstract void Respond(Pointer<EventClass> pEvent, Pointer<TArg> pArg);

        public static bool Send(byte id, in TArg arg)
        {
            var sender = new EventClass
            {
                HouseIndex = (byte)HouseClass.Player.Ref.ArrayIndex,
                Type = (NetworkEvents)id,
                Frame = (uint)Game.CurrentFrame
            };

            Pointer<TArg> pArg = (IntPtr)sender.GetThisPointer() + 7;
            pArg.Data = arg;
            
            return EventClass.AddEvent(sender);
        }
    }

}
