using System;

namespace Core.BehaviourTree.Strategies
{
    public class ConditionStrategy : IStrategy
    {
        private readonly Func<bool> _predicate;

        public ConditionStrategy(Func<bool> predicate) => _predicate = predicate;

        public Node.Status Process() => _predicate() ? Node.Status.Success : Node.Status.Failure;

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}