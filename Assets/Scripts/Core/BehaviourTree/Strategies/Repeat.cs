namespace Core.BehaviourTree.Strategies
{
    public class Repeat : Node
    {
        private readonly int _repeatCount;
        private int _currentCount;

        public Repeat(string name, int repeatCount) : base(name) => _repeatCount = repeatCount;

        public override Status Process()
        {
            var child = ChildrenList[0];

            var status = child.Process();

            if (status == Status.Running) return Status.Running;

            _currentCount++;
            
            if (_repeatCount > 0 && _currentCount >= _repeatCount)
            {
                Reset();
                _currentCount = 0;
                return Status.Success;
            }

            child.Reset();
            return Status.Running;
        }

        protected internal override void Reset()
        {
            base.Reset();
            _currentCount = 0;
        }
    }
}