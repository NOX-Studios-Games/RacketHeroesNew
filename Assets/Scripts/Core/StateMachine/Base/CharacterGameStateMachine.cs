using UnityEngine;

namespace Core.StateMachine.Base
{
    public class CharacterGameStateMachine : GameStateMachine
    {
        [SerializeField] private Animator animator;

        protected override void SetupStates()
        {
            foreach (var stateData in stateDataList)
            {
                stateData.CreateCharacterState(animator);
            }
        }
    }
}