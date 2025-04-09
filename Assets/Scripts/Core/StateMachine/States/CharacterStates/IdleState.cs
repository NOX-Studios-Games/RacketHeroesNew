using Core.StateMachine.Base;
using Core.StateMachine.StateDatas;
using UnityEngine;

namespace Core.StateMachine.States.CharacterStates
{
    public class IdleState : BaseState
    {
        private readonly IdleStateData _data;
        private readonly Animator _animator;

        public IdleState(IdleStateData data, Animator animator)
        {
            _data = data;
            _animator = animator;
        }
    }
}