using System.Collections.Generic;
using System.Linq;

namespace Core.BehaviourTree
{
    public class PrioritySelector : Selector
    {
        private List<Node> _sortedChildren;
        private List<Node> SortedChildren => _sortedChildren ??= SortChildren();

        protected virtual List<Node> SortChildren() 
            => ChildrenList.OrderByDescending(child => child.Priority).ToList();

        protected PrioritySelector(string name) : base(name) { }

        protected override void Reset()
        {
            base.Reset();
            _sortedChildren = null;
        }

        public override Status Process()
        {
            foreach (var child in SortedChildren)
            {
                switch (child.Process())
                {
                    case Status.Running:
                        return Status.Running;
                    
                    case Status.Success:
                        return Status.Success;
                    
                    default:
                        continue;
                }
            }

            return Status.Failure;
        }
    }
}