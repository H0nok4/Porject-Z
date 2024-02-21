using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class PathNode : IComparable<PathNode> {
    public int MapDataIndex;

    public IntVec2 Pos;

    public bool IsStartPoint;

    public bool IsEndPoint;

    public PathNode Parent;

    public int curCost;//从起始点到这个点的消耗

    public int targetCost;//从这个点到终点的消耗

    public int totalCost;//消耗总计

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