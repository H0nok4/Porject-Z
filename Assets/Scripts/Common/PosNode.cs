using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PosNode : IComparable<PosNode> {
    public int MapDataIndex;

    public IntVec2 Pos;

    public bool IsStartPoint;

    public bool IsEndPoint;

    public PosNode Parent;

    public int Length;//到当前的步数

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

    public bool IsSameNode(PosNode other)
    {
        return other.Pos == this.Pos && MapDataIndex == other.MapDataIndex;
    }

    public int CompareTo(PosNode other)
    {
        //这个接口用于寻路，比较消耗就可以了
        return totalCost.CompareTo(other.totalCost);
    }

    public float FastDistance(Vector3 vec3) {
        return Vector3.Distance(vec3, new Vector3() {x = Pos.X, y = Pos.Y});
    }

    public Section ToSection()
    {
        return MapController.Instance.Map.GetMapDataByIndex(MapDataIndex).GetSectionByPosition(Pos);
    }

}

public class PathNodeComparer : IEqualityComparer<PosNode>
{
    public bool Equals(PosNode x, PosNode y)
    {
        if (x.GetType() != y.GetType()) return false;
        return x.IsSameNode(y);
    }

    public int GetHashCode(PosNode obj)
    {
        return HashCode.Combine(obj.MapDataIndex, obj.Pos);
    }
}