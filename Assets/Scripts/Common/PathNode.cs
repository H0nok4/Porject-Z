using System;
using System.Collections.Generic;

public class PathNode : IComparable<PathNode> {
    public int MapDataIndex;

    public IntVec2 Pos;

    public bool IsStartPoint;

    public bool IsEndPoint;

    public PathNode Parent;

    public int curCost;//从起始点到这个点的消耗

    public int targetCost;//从这个点到终点的消耗

    //消耗总计
    public int totalCost
    {
        get
        {
            return curCost + targetCost;
        }
    }

    public bool IsSameNode(PathNode other)
    {
        return other.Pos == this.Pos && MapDataIndex == other.MapDataIndex;
        
    }

    public int CompareTo(PathNode other)
    {
        //这个接口用于寻路，比较消耗就可以了
        return totalCost.CompareTo(other.totalCost);
    }

}

public class PathNodeComparer : IEqualityComparer<PathNode>
{
    public bool Equals(PathNode x, PathNode y)
    {
        if (x.GetType() != y.GetType()) return false;
        return x.IsSameNode(y);
    }

    public int GetHashCode(PathNode obj)
    {
        return HashCode.Combine(obj.MapDataIndex, obj.Pos);
    }
}