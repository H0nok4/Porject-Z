using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ThinkNode_RandomChoose : ThinkNode
{
    public override ThinkResult GetResult(Thing_Unit unit)
    {
        if (Children.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, Children.Count);
            return Children[index].GetResult(unit);
        }

        return ThinkResult.NoJob;
    }
}
