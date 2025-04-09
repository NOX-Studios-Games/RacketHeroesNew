using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine.Base
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] protected List<StateData> stateDataList;
        protected IState CurrentState;

        protected virtual void Awake() => SetupStates();
        
        protected virtual void SetupStates()
        {
            foreach (var stateData in stateDataList)
            {
                stateData.CreateGameState();
            }
        }

        protected virtual void Update() => CurrentState?.OnUpdate();
        protected virtual void FixedUpdate() => CurrentState?.OnFixedUpdate();
        
        public virtual void SetState(IState newState)
        {
            if(newState == null || CurrentState == newState) return;
            
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}