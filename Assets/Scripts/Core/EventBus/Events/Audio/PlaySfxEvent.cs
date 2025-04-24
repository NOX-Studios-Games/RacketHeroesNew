using Core.Audio;

namespace Core.EventBus.Events.Audio
{
    public struct PlaySfxEvent : IEvent
    {
        public SfxId SfxId { get; }

        public PlaySfxEvent(SfxId id) => SfxId = id;
    }
}