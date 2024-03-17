using System.Collections.Generic;

public class PawnPath {
    public List<PosNode> FindingPath;

    /// <summary>
    /// 当前前往的格子索引
    /// </summary>
    public int CurMovingIndex;

    public bool End => CurMovingIndex == FindingPath.Count;

    public PosNode StartNode => Length > 0 ? FindingPath[0] : null;
    public int Length => FindingPath.Count;

    public PawnPath(List<PosNode> findingPath) {
        FindingPath = findingPath;
        CurMovingIndex = 0;
    }

    public PosNode GetCurrentPosition() {
        if (End) {
            return null;
        }

        return FindingPath[CurMovingIndex];
    }

    public PosNode GetNextPosition() {
        if (End) {
            return null;
        }

        return FindingPath[CurMovingIndex + 1];
    }
}