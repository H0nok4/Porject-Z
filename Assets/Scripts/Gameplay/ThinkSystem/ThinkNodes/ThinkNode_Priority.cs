using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 按顺序执行子Node
/// </summary>
public class ThinkNode_Priority : ThinkNode {
    public override ThinkResult GetResult(Thing_Unit unit)
    {
        for (int i = 0; i < Children.Count; i++)
        {
            ThinkResult result = Children[i].GetResult(unit);

            if (result.IsValid)
            {
                return result;
            }
        }

        return ThinkResult.NoJob;
    }
}