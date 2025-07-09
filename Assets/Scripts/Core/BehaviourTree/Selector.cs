namespace Core.BehaviourTree
{
    public class Selector : Node
    {
        public Selector(string name) : base(name) { }

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