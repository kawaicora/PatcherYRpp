
namespace PatcherYRpp
{
    public interface INetworkHandle
    {
        byte Index { get; }
        uint Lenth { get; }
        string Name { get; }

        void Respond(Pointer<EventClass> pEvent);

    }
}