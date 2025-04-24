using UnityEngine;

namespace Core.StateMachine.Base
{
    public abstract class CharacterState : State
    {
        [SerializeField] private AnimationClip animationClip;
        
        public override void OnEnter() => PlayAnimation();

        private void PlayAnimation()
        {
            if (myStateMachine is not CharacterStateMachine characterStateMachine) return;
            if (animationClip == null) return;
            
            characterStateMachine.myAnimator.Play(animationClip.name);
        }
    }
}