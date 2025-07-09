namespace Core.BehaviourTree
{
    public class UntilSuccess : Node
    {
        public UntilSuccess(string name) : base(name) { }

        public override Status Process()
        {
            if (ChildrenList[0].Process() == Status.Success)
            {
                Reset();
                return Status.Success;
            }

            return Status.Running;
        }
    }
}