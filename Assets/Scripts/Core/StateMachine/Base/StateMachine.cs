using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine.Base
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] protected List<StateData> stateDataList;
        protected readonly Dictionary<StateData, IState> StateDictionary = new();
        protected IState CurrentState;

        protected virtual void Awake() => SetupStates();
        
        protected virtual void SetupStates()
        {
            StateDictionary.Clear();
            
            foreach (var stateData in stateDataList)
            {
                if(stateData == null) continue;
                
                var state = stateData.CreateGameState();
                if (state == null) continue;
                
                StateDictionary.TryAdd(stateData, state);
            }
        }

        protected virtual void Update() => CurrentState?.OnUpdate();
        protected virtual void FixedUpdate() => CurrentState?.OnFixedUpdate();
        
        public virtual void SetState(StateData stateData)
        {
            if(stateData == null 
               || !StateDictionary.TryGetValue(stateData, out var newState)) return;
            
            if(CurrentState == newState) return;
            
            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
    }
}