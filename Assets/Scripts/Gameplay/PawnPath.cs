using System.Collections.Generic;

public class PawnPath {
    public List<PathNode> FindingPath;

    /// <summary>
    /// 当前前往的格子索引
    /// </summary>
    public int CurMovingIndex = 0;

    /// <summary>
    /// 是否正在寻路
    /// </summary>
    public bool Using;

    public bool End => CurMovingIndex == FindingPath.Count;

    public PathNode StartNode => Length > 0 ? FindingPath[0] : null;
    public int Length => FindingPath.Count;

    public void Complete() {
        Using = false;
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