using System.Collections.Generic;

public class PawnPath {
    public List<PathNode> FindingPath;

    /// <summary>
    /// 当前前往的格子索引
    /// </summary>
    public int CurMovingIndex;

    public bool End => CurMovingIndex == FindingPath.Count;

    public PathNode StartNode => Length > 0 ? FindingPath[0] : null;
    public int Length => FindingPath.Count;

    public PawnPath(List<PathNode> findingPath) {
        FindingPath = findingPath;
        CurMovingIndex = 0;
    }

    public PathNode GetCurrentPosition() {
        if (End) {
            return null;
        }

        return FindingPath[CurMovingIndex];
    }

    public PathNode GetNextPosition() {
        if (End) {
            return null;
        }

        return FindingPath[++CurMovingIndex];
    }
}