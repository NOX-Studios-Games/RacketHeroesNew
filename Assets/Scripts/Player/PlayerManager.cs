using Core.EventBus.Events;
using RacketHeroes.Characters;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerManager : MonoBehaviour
    {
        public CharactersAttributesSO attributes;

        private void Awake() => SetUpAttributes();

        private void SetUpAttributes()
        {
            EventBus<SetupAttributesEvent>.Publish(new SetupAttributesEvent
            (
                attributes.movementSpeed,
                attributes.swingForce,
                attributes.damage,
                attributes.criticalRate,
                attributes.health,
                attributes.armor
            ));
        }
    }
}