namespace Core.BehaviourTree
{
    public class Selector : Node
    {
        protected Selector(string name, int priority = 0) : base(name, priority) { }

        public override Status Process()
        {
            if(CurrentChild < ChildrenList.Count)
            {
                switch (ChildrenList[CurrentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;

                    case Status.Success:
                        Reset();
                        return Status.Success;

                    case Status.Failure:
                    default:
                        CurrentChild++;
                        return Status.Running;
                }
            }
            
            Reset();
            return Status.Failure;
        }
    }
}