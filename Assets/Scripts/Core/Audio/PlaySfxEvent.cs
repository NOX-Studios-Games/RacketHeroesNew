using Core.EventBus;

namespace Core.Audio
{
    public struct PlaySfxEvent : IEvent
    {
        public SfxId SfxId { get; }

        public PlaySfxEvent(SfxId id) => SfxId = id;
    }
}