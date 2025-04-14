using System.Collections.Generic;
using Core.EventBus;
using UnityEngine;

namespace Core.Audio 
{
    public class AudioEventHandler : MonoBehaviour 
    {
        public List<AudioClipData> audioClips;
        private readonly Dictionary<SfxId, AudioClip> _audioClipDictionary = new ();
        
        private EventBinding<PlaySfxEvent> _playSfxBinding;

        private void Awake() => PopulateAudioClipDictionary();

        private void OnEnable() {
            _playSfxBinding = new EventBinding<PlaySfxEvent>(PlaySfx);
            EventBus<PlaySfxEvent>.Register(_playSfxBinding);
        }

        private void OnDisable() => EventBus<PlaySfxEvent>.Unregister(_playSfxBinding);

        private void PlaySfx(PlaySfxEvent eventData)
        {
            if(_audioClipDictionary.TryGetValue(eventData.SfxId, out var audioClip))
            {
                AudioManager.Instance.PlaySfx(audioClip);
            }
        }

        public void PlayBGM(PlaySfxEvent eventData)
        {
            //AudioManager.Instance.PlayAudio();
        }

        public void StopBGM() => AudioManager.Instance.StopAudio();
        
        private void PopulateAudioClipDictionary()
        {
            foreach (var audioClip in audioClips)
            {
                if (_audioClipDictionary.ContainsKey(audioClip.sfxId)) continue;
                _audioClipDictionary[audioClip.sfxId] = audioClip.clip;
            }
        }
    }
    
    public enum SfxId
    {
        Walking,
        Attacking,
        TakingDamage,
        Dying
    }
    
    [System.Serializable]
    public struct AudioClipData
    {
        public AudioClip clip;
        public SfxId sfxId;
    }
}