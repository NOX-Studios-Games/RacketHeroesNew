namespace Core.BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            if (CurrentChild < ChildrenList.Count)
            {
                switch (ChildrenList[CurrentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    
                    default:
                        CurrentChild++;
                        return CurrentChild == ChildrenList.Count ? Status.Success : Status.Running;
                }
            }
            
            Reset();
            return Status.Success;
        }
    }
}