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

    public MapData MapData => MapController.Instance.Map.GetMapDataByIndex(MapDataIndex);

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

    public int GetMaxItemCountInPos()
    {
        //TODO:有的时候有建筑可以让该格子可以放更多个物品，比如物品架,后续实现
        return 1;
    }

    public bool InBound()
    {
        //位置一定大于0
        if (Pos.X < 0 || Pos.Y < 0)
        {
            return false;
        }

        if ((uint)Pos.X < MapData.Width) {
            return (uint)Pos.Y < MapData.Height;
        }
        return false;
    }

    public bool IsWalkable()
    {
        //TODO:先看位置是否可以行走
        if (!MapData.GetSectionByPosition(Pos).Walkable)
        {
            return false;
        }

        //TODO:看看是否有Thing不可行走
        var things = MapData.ThingMap.ThingsAt(Pos);
        foreach (var thing in things)
        {
            if (thing.Def.Passability == Traversability.Impassable)
            {
                return false;
            }
        }

        return true;
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