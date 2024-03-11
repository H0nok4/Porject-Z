using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 可以返回工作类型的Node
/// </summary>
public abstract class ThinkNode_JobGiver : ThinkNode
{
    public abstract Job TryGiveJob(Thing_Unit unit);

    public override ThinkResult GetResult(Thing_Unit unit)
    {
        Job result = TryGiveJob(unit);
        if (result  == null)
        {
            return ThinkResult.NoJob;
        }

        return new ThinkResult(result, this);
    }
}