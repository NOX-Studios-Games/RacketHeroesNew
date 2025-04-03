using Core.EventBus;
using Core.EventBus.Events;
using UnityEngine;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        public CharactersAttributesSO attributes;

        private void Start() => SetUpAttributes();

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