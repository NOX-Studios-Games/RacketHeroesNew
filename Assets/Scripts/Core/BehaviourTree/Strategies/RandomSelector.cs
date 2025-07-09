using System.Collections.Generic;
using System.Linq;
using Core.Utilities;

namespace Core.BehaviourTree.Strategies
{
    public class RandomSelector : PrioritySelector
    {
        protected override List<Node> SortChildren() => ChildrenList.Shuffle().ToList();

        public RandomSelector(string name) : base(name) { }
    }
}