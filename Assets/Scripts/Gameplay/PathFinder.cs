using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using ConfigType;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class PathFinder {
    public static List<PosNode> AStarFindPath(Thing_Unit_Pawn unit, PosNode targetPos, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(unit, map.GetPathNodeByUnit(unit), targetPos,PathMoveEndType.InCell, map);
    }

    public static List<PosNode> AStarFindPath(Thing_Unit pawn, PosNode targetPos, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(pawn,map.GetPathNodeByUnit(pawn), targetPos,PathMoveEndType.InCell, map);
    }

    public static List<PosNode> AStarFindPath(Thing_Unit pawn, PosNode targetPos, PathMoveEndType endType,
        Map useMap = null) {
        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(pawn,map.GetPathNodeByUnit(pawn), targetPos,endType, map);
    }

    public static readonly List<IntVec2> DirVecList = new List<IntVec2>()
        { IntVec2.North,IntVec2.NorthEast, IntVec2.East,IntVec2.EastSouth, IntVec2.South,IntVec2.SouthWest, IntVec2.West ,IntVec2.WestNorth};

    public static PathFindNodeComparer PathFindNodeComparer = new PathFindNodeComparer();
    /// <summary>
    /// 真正的寻路方法
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="endType"></param>
    /// <param name="useMap"></param>
    /// <returns></returns>
    public static List<PosNode> AStarFindPath(Thing_Unit finder,PosNode startPos, PosNode endPos,PathMoveEndType endType, Map useMap = null) {
        //A*寻路
        List<PosNode> result = SimplePool<List<PosNode>>.Get();
        result.Clear();
        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        bool hasCanTouchPos = false;
        if (endType == PathMoveEndType.Touch) {
            //TODO:需要获得目标位置附近是否有可以站立的位置，如果有的话则需要一个标志位
            var walkablePosAroundList = PlaceUtility.GetWalkablePosByBFS(endPos, 1);
            foreach (var posNode in walkablePosAroundList) {
                bool posCanStand = true; 
                foreach (var thing in endPos.MapData.ThingMap.ThingsAt(posNode.Pos)) {
                    if (thing.Def.Passability != Traversability.CanStand) {
                        posCanStand = false;
                    }
                }

                if (posCanStand) {
                    hasCanTouchPos = true;
                    break;
                }
            }
        }

        var openList = SimplePool<PriorityQueue<PathFindNode>>.Get();
        while (openList.Count > 0) {
            var clearNode = openList.Dequeue();
            SimplePool<PathFindNode>.Return(clearNode);
        }
        openList.Clear();
        var closeList = SimplePool<HashSet<PathFindNode>>.FreeItemsCount > 0 ? SimplePool<HashSet<PathFindNode>>.Get() : new HashSet<PathFindNode>(new PathFindNodeComparer());
        closeList.Clear();

        var startPathNode = new PathFindNode(startPos.Pos, startPos.MapDataIndex);
        openList.Enqueue(startPathNode);

        while (openList.Count > 0)
        {
            var node = openList.Dequeue();
            //将附近的格子加入到队列中wa
            var mapData = map.GetMapDataByIndex(node.MapDataIndex);

            foreach (var dir in DirVecList) {
                //需要实现一个类似象棋中会被绊马脚的机制，防止直接田字格在右边或者上边有建筑走斜边看着像是穿过建筑物
                if (mapData.GetSectionByPosition(node.Pos + dir) is { } section) {

                    if (!section.Walkable)
                    {
                        //必须要可以走的
                        continue;
                    }

                    if (mapData.ThingMap.ThingsAt(section.Position).Any((thing)=>thing.Def.Passability == Traversability.Impassable))
                    {
                        continue;
                    }



                    var newNode = section.CreatePathFindNode();
                    if (closeList.Contains(newNode, PathFindNodeComparer)) {
                        SimplePool<PathFindNode>.Return(newNode);
                        continue;
                    }

                    //TODO:有问题，之前触摸的结束位置是想要找到一个位置的新位置能够是目标地点，需要改一下
                    bool isSameNodeFlag = newNode.IsSameNode(endPos);

                    //触碰在走之前可以判断
                    if (isSameNodeFlag && endType == PathMoveEndType.Touch)
                    {
                        //TODO:如果这个位置可以当结束位置，需要判断是否可以站立在这里
                        if (hasCanTouchPos) {
                            if (mapData.ThingMap.ThingsAt(node.Pos).Any((thing) => thing != finder && thing.Def.Passability != Traversability.CanStand)) {
                                Debug.LogWarning($"这个位置{newNode.Pos}可以当结束位置，但是不可以站立,跳过");
                                continue;
                            }
                        }
                        //直接从当前位置创建一个路径
                        CreateResultPath(result, node);
                        ClearOpenList(openList);
                        ClearCloseSet(closeList);
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
                            ClearOpenList(openList);
                            ClearCloseSet(closeList);
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

            if (endType != PathMoveEndType.Touch || !node.IsSameNode(endPos))
            {
                closeList.Add(node);
            }
            
            
            //为了防止找了超级大的范围，需要一个跳出

            if (openList.Count >= 100000)
            {
                break;
            }
        }


        //找遍了地图没有找到终点，可能没有路径能够到达
        Debug.LogError("没有对应的路径到达目标点");

        ClearOpenList(openList);
        ClearCloseSet(closeList);
        return result;

        void CreateResultPath(List<PosNode> path,PathFindNode end)
        {
            var node = end;
            while (node.Parent != null)
            {
                path.Add(new PosNode(node.Pos,node.MapDataIndex));
                node = node.Parent;
            }
#if UNITY_EDITOR
            StringBuilder sb = new StringBuilder();
            sb.Append($"创建一个寻路路径，长度为：{path.Count},路径点为:");
            foreach (var posNode in path) {
                sb.Append($",{posNode.Pos}");
            }
            Debug.Log(sb);
#endif

            //因为是从终点开始加的，所以倒过来，后面需要用其他的数据结构优化一下
            path.Reverse();
        }

        int GetTargetCost(PathFindNode node, PosNode endNode)
        {
            //计算到目标点的消耗
            //如果是同一个地图，计算距离，如果非同一个地图，需要根据地图的层级差距来增加权重
            var deltaX = Math.Abs(endNode.Pos.X - node.Pos.X);
            var deltaY = Math.Abs(endNode.Pos.Y - node.Pos.Y);
            var distance = (deltaX + deltaY);
            var costBase = 10;
            if (node.MapDataIndex == endNode.MapDataIndex)
            {
                return distance * costBase;
            }

            return (distance * costBase) + (Math.Abs(endNode.MapDataIndex - node.MapDataIndex) * 20);//TODO:后面需要改成如果不在同一个平面上，需要对当前层向下或者向上的楼梯类位置加权移动
        }

    }

    private static void ClearCloseSet(HashSet<PathFindNode> set) {
        Debug.Log($"当前准备回收:{set.Count}个PosNode,引用池中总共有:{SimplePool<PathFindNode>.FreeItemsCount}个PosNode");
        foreach (var posNode in set) {
            SimplePool<PathFindNode>.Return(posNode);
        }
        set.Clear();
        SimplePool<HashSet<PathFindNode>>.Return(set);
    }

    private static void ClearOpenList(PriorityQueue<PathFindNode> openList) {
        Debug.Log($"当前准备回收:{openList.Count}个PosNode,引用池中总共有:{SimplePool<PathFindNode>.FreeItemsCount}个PosNode");
        while (openList.Count > 0) {
            SimplePool<PathFindNode>.Return(openList.Dequeue());
        }
        openList.Clear();
        SimplePool<PriorityQueue<PathFindNode>>.Return(openList);
    }

    public static readonly Dictionary<IntVec2, IntVec2[]> IsDirWillTripByThing = new Dictionary<IntVec2, IntVec2[]>()
    {
        { IntVec2.EastSouth, new IntVec2[] { IntVec2.East, IntVec2.South } },
        { IntVec2.NorthEast, new IntVec2[] { IntVec2.North, IntVec2.East } },
        { IntVec2.SouthWest, new IntVec2[] { IntVec2.South, IntVec2.West } },
        { IntVec2.WestNorth, new IntVec2[] { IntVec2.West, IntVec2.North } }
    };

    private static bool IsTripByImpassableThing(IntVec2 dir, PathFindNode startPos)
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

    private static int CalculateNodeCurCost(PathFindNode node, PathFindNode newNode)
    {
        //TODO:后面需要根据位置移动的CostTick值来算Cost
        float costBase = 5;
        if (IsDiagonal(node,newNode))
        {
            costBase *= 1.414f;
        }

        return Mathf.FloorToInt(node.curCost + costBase);
    }

    private static bool IsDiagonal(PathFindNode node, PathFindNode newNode)
    {
        return node.Pos.X != newNode.Pos.X && node.Pos.Y != newNode.Pos.Y;
    }

    public static List<PosNode> GetMoveableSectionByBFS(Thing_Unit unit,int maxLength)
    {
        var result = new List<PosNode>();
        var startPos = unit.MapData.GetSectionByPos(unit.Position.Pos);
        var queue = new Queue<PosNode>();
        var startNode = startPos.CreatePathNode(true);
        startNode.Length = 0;
        queue.Enqueue(startNode);
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
                    var newNode = section.CreatePathNode(true);
                    if (closeList.Contains(newNode))
                    {
                        SimplePool<PosNode>.Return(newNode);
                        continue;
                    }
                    
                    newNode.Length = curNode.Length + 1;
                    if (newNode.Length > maxLength)
                    {
                        SimplePool<PosNode>.Return(newNode);
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