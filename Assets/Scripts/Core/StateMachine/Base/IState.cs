namespace Core.StateMachine.Base
{
    public interface IState
    {
        public void OnEnter();
        public void OnUpdate();
        public void OnFixedUpdate();
        public void OnExit();
    }
}