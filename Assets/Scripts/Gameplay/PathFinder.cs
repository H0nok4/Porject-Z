using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class PathFinder {
    public static List<PosNode> AStarFindPath(Pawn pawn, PosNode targetPos, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(map.GetPathNodeByUnit(pawn), targetPos,PathMoveEndType.InCell, map);
    }

    public static List<PosNode> AStarFindPath(Thing_Unit pawn, PosNode targetPos, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(map.GetPathNodeByUnit(pawn), targetPos,PathMoveEndType.InCell, map);
    }

    public static List<PosNode> AStarFindPath(Thing_Unit pawn, PosNode targetPos, PathMoveEndType endType,
        Map useMap = null) {
        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(map.GetPathNodeByUnit(pawn), targetPos,endType, map);
    }

    public static readonly List<IntVec2> DirVecList = new List<IntVec2>()
        { IntVec2.North,IntVec2.NorthEast, IntVec2.East,IntVec2.EastSouth, IntVec2.South,IntVec2.SouthWest, IntVec2.West ,IntVec2.WestNorth};

    /// <summary>
    /// 真正的寻路方法
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="endType"></param>
    /// <param name="useMap"></param>
    /// <returns></returns>
    public static List<PosNode> AStarFindPath(PosNode startPos, PosNode endPos,PathMoveEndType endType, Map useMap = null) {
        //A*寻路
        List<PosNode> result = SimplePool<List<PosNode>>.Get();
        result.Clear();
        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        var openList = SimplePool<PriorityQueue<PosNode>>.Get();
        openList.Clear();
        var closeList = new HashSet<PosNode>(new PathNodeComparer());
        closeList.Clear();
        
        openList.Enqueue(startPos);

        while (openList.Count > 0)
        {
            var node = openList.Dequeue();
            //将附近的格子加入到队列中
            var mapData = map.GetMapDataByIndex(node.MapDataIndex);

            foreach (var dir in DirVecList) {
                //需要实现一个类似象棋中会被绊马脚的机制，防止直接田字格在右边或者上边有建筑走斜边看着像是穿过建筑物


                if (mapData.GetSectionByPosition(node.Pos + dir) is { } section) {

                    if (!section.Walkable)
                    {
                        //必须要可以走的
                        continue;
                    }

                    var newNode = section.CreatePathNode();
                    if (closeList.Contains(newNode)) {
                        continue;
                    }


                    bool isSameNodeFlag = newNode.IsSameNode(endPos);

                    //触碰在走之前可以判断
                    if (isSameNodeFlag && endType == PathMoveEndType.Touch)
                    {
                        //直接从当前位置创建一个路径
                        CreateResultPath(result, node);
                        return result;
                    }


                    if (IsTripByImpassableThing(dir, node)) {
                        //被绊住了，不能走
                        continue;
                    }

                    newNode.Parent = node;
                    newNode.IsEndPoint = newNode.IsSameNode(endPos);
                    newNode.curCost = CalculateNodeCurCost(node,newNode);

                    newNode.targetCost = GetTargetCost(newNode, endPos);
                    openList.Enqueue(newNode);
                    DebugDrawer.DrawBox(newNode.Pos);

                    //TODO:根据寻路的终止条件,有不同的终点类型
                    if (isSameNodeFlag)
                    {
                        if (endType == PathMoveEndType.InCell) {
                            //从这个Node的Parent开始找到一条路径到起始点
                            CreateResultPath(result, newNode);
                            return result;
                        }
                    }

                }

            }

            //TODO:当前的位置可能还可以通到不相邻的位置，需要也添加到寻路路径上
            try
            {
                var curSection = mapData.GetSectionByPosition(node.Pos);
                if (curSection.SectionType == SectionType.Stair) {
                    //TODO:
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            closeList.Add(node);
            //为了防止找了超级大的范围，需要一个跳出

            if (openList.Count >= 100000)
            {
                break;
            }
        }


        //找遍了地图没有找到终点，可能没有路径能够到达
        Debug.LogError("没有对应的路径到达目标点");

        return result;

        void CreateResultPath(List<PosNode> path,PosNode end)
        {
            var node = end;
            while (node.Parent != null)
            {
                path.Add(node);
                node = node.Parent;
            }
            //因为是从终点开始加的，所以倒过来，后面需要用其他的数据结构优化一下
            path.Reverse();
        }

        int GetTargetCost(PosNode node,PosNode endNode)
        {
            //计算到目标点的消耗
            //如果是同一个地图，计算距离，如果非同一个地图，需要根据地图的层级差距来增加权重
            var deltaX = endNode.Pos.X - node.Pos.X;
            var deltaY = endNode.Pos.Y - node.Pos.Y;
            var distance = (int)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
            if (node.MapDataIndex == endNode.MapDataIndex)
            {
                return distance;
            }

            return distance + (Math.Abs(endNode.MapDataIndex - node.MapDataIndex) * 20);//TODO:后面需要改成如果不在同一个平面上，需要对当前层向下或者向上的楼梯类位置加权移动
        }

    }

    public static readonly Dictionary<IntVec2, IntVec2[]> IsDirWillTripByThing = new Dictionary<IntVec2, IntVec2[]>()
    {
        { IntVec2.EastSouth, new IntVec2[] { IntVec2.East, IntVec2.South } },
        { IntVec2.NorthEast, new IntVec2[] { IntVec2.North, IntVec2.East } },
        { IntVec2.SouthWest, new IntVec2[] { IntVec2.South, IntVec2.West } },
        { IntVec2.WestNorth, new IntVec2[] { IntVec2.West, IntVec2.North } }
    };

    private static bool IsTripByImpassableThing(IntVec2 dir, PosNode startPos)
    {


        if (dir != IntVec2.EastSouth && dir != IntVec2.NorthEast && dir != IntVec2.SouthWest && dir != IntVec2.WestNorth)
        {
            //TODO:非斜边，直接返回false
            return false;
        }



        //TODO:走斜边需要判断俩边是否有无法行走的建筑物
        var dirArray = IsDirWillTripByThing[dir];
        foreach (var vec in dirArray)
        {
            var mapData = MapController.Instance.Map.GetMapDataByIndex(startPos.MapDataIndex);


            //TODO:先判断地形是否可以走
            var section = mapData.GetSectionByPosition(startPos.Pos + vec);
            if (section == null)
            {
                continue;
            }

            if (!section.Walkable) {
                return true;
            }

            //再判断物体
            foreach (var things in mapData.ThingMap.ThingsAt(startPos.Pos + vec))
            {
                if (things.Def == null)
                {
                    Debug.LogError("判断物体是否可通行的时候遇到了空配置的物体");
                    continue;
                }

                if (things.Def.Passability == Traversability.Impassable)
                {
                    return true;
                }
            }
        }

        return false;

    }

    private static int CalculateNodeCurCost(PosNode node, PosNode newNode)
    {
        //TODO:后面需要根据位置移动的CostTick值来算Cost
        float costBase = 5;
        if (IsDiagonal(node,newNode))
        {
            costBase *= 1.414f;
        }

        return Mathf.FloorToInt(node.curCost + costBase);
    }

    private static bool IsDiagonal(PosNode node, PosNode newNode)
    {
        return node.Pos.X != newNode.Pos.X && node.Pos.Y != newNode.Pos.Y;
    }

    public static List<PosNode> GetMoveableSectionByBFS(Thing_Unit unit,int maxLength)
    {
        var result = new List<PosNode>();
        var startPos = unit.MapData.GetSectionByPos(unit.Position);
        var queue = new Queue<PosNode>();
        queue.Enqueue(startPos.CreatePathNode());
        var closeList = new HashSet<PosNode>(new PathNodeComparer());
        closeList.Clear();
        while (queue.Count > 0)
        {
            var curNode = queue.Dequeue();

            var mapData = MapController.Instance.Map.GetMapDataByIndex(curNode.MapDataIndex);
            foreach (var dir in DirVecList)
            {
                if (mapData.GetSectionByPosition(curNode.Pos + dir) is { } section) {
                    if (!section.Walkable) {
                        //必须要可以走的
                        continue;
                    }
                    var newNode = section.CreatePathNode();
                    if (closeList.Contains(newNode))
                    {
                        continue;
                    }
                    
                    newNode.Length = curNode.Length + 1;
                    if (newNode.Length > maxLength)
                    {
                        continue;
                    }

                    newNode.Parent = curNode;
                    queue.Enqueue(newNode);
                    DebugDrawer.DrawBox(newNode.Pos);
                }
            }

            result.Add(curNode);
            closeList.Add(curNode);
        }

        return result;
    }
}