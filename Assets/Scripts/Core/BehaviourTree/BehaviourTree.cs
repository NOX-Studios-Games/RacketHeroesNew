namespace Core.BehaviourTree
{
    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name) { }

        public override Status Process()
        {
            while (CurrentChild < ChildrenList.Count)
            {
                var status = ChildrenList[CurrentChild].Process();
                if(status != Status.Success) return status;
                
                CurrentChild++;
            }
            
            return Status.Success;
        }
    }
}