using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class PathFinder {
    public static List<PathNode> AStarFindPath(Pawn pawn, PathNode targetPosition, Map useMap = null) {
        //TODO:A*寻路

        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        return AStarFindPath(map.GetPathNodeByPawn(pawn), targetPosition, map);
    }

    public static List<PathNode> AStarFindPath(PathNode startPos, PathNode endPos, Map useMap = null) {
        //TODO:A*寻路
        List<PathNode> result = new List<PathNode>();
        Map map = MapController.Instance.Map;
        if (useMap != null) {
            map = useMap;
        }

        var openList = SimplePool<PriorityQueue<PathNode>>.Get();
        openList.Clear();
        var closeList = SimplePool<HashSet<PathNode>>.Get();
        closeList.Clear();
        
        openList.Enqueue(startPos);

        while (openList.Count > 0)
        {
            var node = openList.Dequeue();
            //TODO:将附近的格子加入到队列中
            

        }
        return result;

        void AddNode(PathNode parentNode)
        {
            var mapData = map.GetMapDataByIndex(parentNode.MapDataIndex);
            if (mapData.GetSectionByPosition(parentNode.Pos + IntVec2.North) != null)
            {
                
            }
        }
    }
}