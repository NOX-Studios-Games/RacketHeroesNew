using UnityEngine;

namespace Core.EventBus.Events {
  public struct PlayerAttackEvent : IEvent {
    public AudioClip audioClip { get; }

    public PlayerAttackEvent(AudioClip newAudioClip) => audioClip = newAudioClip;
  }
}