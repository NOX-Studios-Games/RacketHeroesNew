using JetBrains.Annotations;
using UnityEngine;

namespace Core.StateMachine.Base
{
    public abstract class StateData : ScriptableObject
    {
        [CanBeNull] public Animation animation;
        
        public virtual BaseState CreateGameState() => null;
        public virtual BaseState CreateCharacterState(Animator animator) => null;
    }
}