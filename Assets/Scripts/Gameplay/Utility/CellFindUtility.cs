
using System.Collections.Generic;

public static class CellFindUtility {
    public static bool TryFindPositionToTouch(Thing target,out PosNode resultPos)
    {
        foreach (var posNode in AdjacentUtility.GetAroundThingPosition(target))
        {
            if (posNode.Standable())
            {
                resultPos = posNode;
                return true;
            }
        }

        resultPos = target.Position;
        return false;
    }
}