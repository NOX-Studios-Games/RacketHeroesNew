using UnityEngine;

namespace Core.StateMachine.Base
{
    [System.Serializable]
    public class State : MonoBehaviour, IState
    {
        [HideInInspector] public StateMachine myStateMachine;
        
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnExit() { }
    }
}