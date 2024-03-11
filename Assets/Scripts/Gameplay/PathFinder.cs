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
    public static List<PathNode> AStarFindPath(Pawn pawn, PathNode targetPosition, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(map.GetPathNodeByUnit(pawn), targetPosition, map);
    }

    public static List<PathNode> AStarFindPath(Thing_Unit pawn, PathNode targetPosition, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(map.GetPathNodeByUnit(pawn), targetPosition, map);
    }

    public static readonly List<IntVec2> DirVecList = new List<IntVec2>()
        { IntVec2.North, IntVec2.East, IntVec2.South, IntVec2.West };

    public static List<PathNode> AStarFindPath(PathNode startPos, PathNode endPos, Map useMap = null) {
        //A*寻路
        List<PathNode> result = SimplePool<List<PathNode>>.Get();
        result.Clear();
        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        var openList = SimplePool<PriorityQueue<PathNode>>.Get();
        openList.Clear();
        var closeList = new HashSet<PathNode>(new PathNodeComparer());
        closeList.Clear();
        
        openList.Enqueue(startPos);

        while (openList.Count > 0)
        {
            var node = openList.Dequeue();
            //将附近的格子加入到队列中
            var mapData = map.GetMapDataByIndex(node.MapDataIndex);

            foreach (var dir in DirVecList) {
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
                    newNode.Parent = node;
                    newNode.IsEndPoint = newNode.IsSameNode(endPos);
                    newNode.curCost = node.curCost + 1;
                    newNode.targetCost = GetTargetCost(newNode, endPos);
                    openList.Enqueue(newNode);
                    DebugDrawer.DrawBox(newNode.Pos);
                    if (newNode.IsSameNode(endPos)) {
                        //TODO:从这个Node的Parent开始找到一条路径到起始点
                        CreateResultPath(result, newNode);
                        return result;
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

        void CreateResultPath(List<PathNode> path,PathNode end)
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

        int GetTargetCost(PathNode node,PathNode endNode)
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

    public static List<PathNode> GetMoveableSectionByBFS(Thing_Unit unit,int maxLength)
    {
        var result = new List<PathNode>();
        var startPos = unit.MapData.GetSectionByPos(unit.Position);
        var queue = new Queue<PathNode>();
        queue.Enqueue(startPos.CreatePathNode());
        var closeList = new HashSet<PathNode>(new PathNodeComparer());
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