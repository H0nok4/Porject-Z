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

        return result;
    }
}