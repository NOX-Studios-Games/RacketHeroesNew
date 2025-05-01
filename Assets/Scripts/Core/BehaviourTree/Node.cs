using System.Collections.Generic;

namespace Core.BehaviourTree
{
    public class Node
    {
        public enum Status { Running, Success, Failure }

        public readonly string Name;
        protected readonly List<Node> ChildrenList = new();
        protected int CurrentChild;

        protected Node(string name = "Node") => Name = name;
        
        public virtual void AddChild(Node child)
        {
            if (child == null) return;
            ChildrenList.Add(child);
        }

        public virtual Status Process() => ChildrenList[CurrentChild].Process();
        
        public virtual void Reset()
        {
            CurrentChild = 0;
            foreach (var child in ChildrenList) child.Reset();
        }
    }
}