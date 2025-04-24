using UnityEngine;

namespace Core.StateMachine.Base
{
    public class StateMachine : MonoBehaviour
    {
        private IState _currentState;
        
        protected virtual void Update() => _currentState?.OnUpdate();
        protected virtual void FixedUpdate() => _currentState?.OnFixedUpdate();
        
        public void SetState(IState newState)
        {
            if(newState == null || _currentState == newState) return;
            
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }
    }
}