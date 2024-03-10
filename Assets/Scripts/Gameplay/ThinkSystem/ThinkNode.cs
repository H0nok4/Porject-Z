using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ThinkNode
{
    public List<ThinkNode> Children = new List<ThinkNode>();

    public ThinkNode Parent;

    public IEnumerable<ThinkNode> ChildrenRecursive
    {
        get
        {
            if (Children.Count <= 0)
            {
                yield break;
            }

            Stack<ThinkNode> stack = new Stack<ThinkNode>();
            for (int i = Children.Count - 1; i >= 0 ; i--)
            {
                stack.Push(Children[i]);
            }

            while (stack.Count > 0)
            {
                var child = stack.Pop();
                yield return child;
                for (int i = child.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(child.Children[i]);
                }
            }
        }
    }

    public abstract ThinkResult GetResult(Thing_Unit unit);

}