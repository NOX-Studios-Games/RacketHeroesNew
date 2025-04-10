using UnityEngine;

namespace Core.StateMachine.Base
{
    public class CharacterStateMachine : StateMachine
    {
        [SerializeField] private Animator animator;

        protected override void SetupStates()
        {
            foreach (var stateData in stateDataList)
            {
                //var state = stateData.CreateCharacterState(animator);
                
                //if (state == null) continue;
                //StateDictionary.TryAdd(stateData, state);
            }
        }
    }
}