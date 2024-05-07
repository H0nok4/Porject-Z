using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class PathFindNode : IComparable<PathFindNode> {
    public int MapDataIndex;

    public IntVec2 Pos;

    public bool IsStartPoint;

    public bool IsEndPoint;

    public PathFindNode Parent;

    public int Length;//到当前的步数

    public int curCost;//从起始点到这个点的消耗

    public int targetCost;//从这个点到终点的消耗

    //消耗总计
    public int totalCost {
        get {
            return curCost + targetCost;
        }
    }

    public PathFindNode() {

    }


    public PathFindNode(IntVec2 pos, int mapIndex) {
        Pos = pos;
        MapDataIndex = mapIndex;
    }

    public void Clear() {
        IsStartPoint = false;
        IsEndPoint = false;
        Parent = null;
        Length = 0;
        curCost = 0;
        targetCost = 0;
    }

    public MapData MapData => MapController.Instance.Map.GetMapDataByIndex(MapDataIndex);

    public bool IsSameNode(PathFindNode other) {
        return other.Pos.X == this.Pos.X && other.Pos.Y == this.Pos.Y && MapDataIndex == other.MapDataIndex;
    }

    public bool IsSameNode(PosNode other) {
        return other.Pos.X == this.Pos.X && other.Pos.Y == this.Pos.Y && MapDataIndex == other.MapDataIndex;
    }

    public int CompareTo(PathFindNode other) {
        //这个接口用于寻路，比较消耗就可以了
        return totalCost.CompareTo(other.totalCost);
    }

    public float FastDistance(Vector3 vec3) {
        return Vector3.Distance(vec3, new Vector3() { x = Pos.X, y = Pos.Y });
    }

    public PathFindNode DeepCopy() {
        return new PathFindNode(Pos.Copy(), MapDataIndex) { };
    }
}

public class PathFindNodeComparer : IEqualityComparer<PathFindNode> {
    public bool Equals(PathFindNode x, PathFindNode y) {
        return x.IsSameNode(y);
    }

    public int GetHashCode(PathFindNode obj) {
        return HashCode.Combine(obj.MapDataIndex, obj.Pos);
    }
}
