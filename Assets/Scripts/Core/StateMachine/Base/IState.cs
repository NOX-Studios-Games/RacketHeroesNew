namespace Core.StateMachine.Base
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnFixedUpdate();
        void OnExit();
    }
}