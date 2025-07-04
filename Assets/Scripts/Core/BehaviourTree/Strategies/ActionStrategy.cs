using System;

namespace Core.BehaviourTree.Strategies
{
    public class ActionStrategy : IStrategy
    {
        private readonly Action _action;

        public ActionStrategy(Action action) => _action = action;

        public Node.Status Process()
        {
            _action();
            return Node.Status.Success;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}