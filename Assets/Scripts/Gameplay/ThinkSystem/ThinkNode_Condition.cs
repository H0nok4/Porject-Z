using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 有条件的Node
/// </summary>
public abstract class ThinkNode_Condition : ThinkNode_Priority
{
    public override ThinkResult GetResult(Thing_Unit unit)
    {
        if (Satisfied(unit))
        {
            return base.GetResult(unit);
        }

        return ThinkResult.NoJob;
    }

    protected abstract bool Satisfied(Thing_Unit unit);
}