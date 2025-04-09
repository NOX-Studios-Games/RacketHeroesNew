using Core.StateMachine.Base;
using Core.StateMachine.States.CharacterStates;
using UnityEngine;

namespace Core.StateMachine.StateDatas
{
    [CreateAssetMenu(fileName = "IdleStateData", menuName = "StateMachine/IdleStateData")]
    public class IdleStateData : StateData
    {
        public override BaseState CreateCharacterState(Animator animator) 
            => new IdleState(this, animator);
    }
}