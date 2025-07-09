namespace Core.BehaviourTree
{
    public class UntilFail : Node
    {
        public UntilFail(string name) : base(name) { }

        public override Status Process()
        {
            if (ChildrenList[0].Process() == Status.Failure)
            {
                Reset();
                return Status.Failure;
            }

            return Status.Running;
        }
    }
}