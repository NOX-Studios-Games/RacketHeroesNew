using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;

namespace Core.Audio {
    public class AudioEventHandler : MonoBehaviour {
        //Lista de Bidings
        private EventBinding<PlayerAttackEvent> _playerAttackBiding;

        private void OnEnable() {
            _playerAttackBiding = new EventBinding<PlayerAttackEvent>(PlaySFX);
            EventBus<PlayerAttackEvent>.Register(_playerAttackBiding);
        }

        private void OnDisable() {
            EventBus<PlayerAttackEvent>.Unregister(_playerAttackBiding);
        }

        //Fun��o de tocar SFX
        public void PlaySFX(PlayerAttackEvent eventData) {
            AudioManager.Instance.PlaySFX(eventData.audioClip);
        }
        //Fun��o de tocar BGM
        public void PlayBGM(PlayerAttackEvent eventData) {
            AudioManager.Instance.PlayAudio(eventData.audioClip);
        }
        //Fun��o de parar BGM
        public void StopBGM() {
            AudioManager.Instance.StopAudio();
        }

}
}
