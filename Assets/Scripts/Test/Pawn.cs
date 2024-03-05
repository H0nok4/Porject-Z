using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour,IThing
{
    public PathMover PathMover;

    public PawnPath FindingPath;

    private IntVec2 _position = IntVec2.Invalid;

    private float _moveSpeed = 1f;
    public IntVec2 Position
    {
        get
        {
            return _position;
        }
        set
        {
            if (value == _position)
            {
                return;
            }

            _position.X = value.X;
            _position.Y = value.Y;

            transform.position = new Vector3(_position.X, _position.Y);
        }
    }


    public void Tick()
    {
        //TODO:查看是否当前有寻路的
        PathMover.Tick();
    }

    private void EnterPos() {

    }

    private void ExitPos() {

    }

    public Pawn(IntVec2 position, bool isDestoryed, ThingType thingType) {
        Position = position;
        IsDestoryed = isDestoryed;
        ThingType = thingType;
    }

    public bool IsDestoryed { get; set; }
    public ThingType ThingType { get; set; }
}

public class PathMover
{
    public Pawn RegisterPawn;

    public Map AllMap => MapController.Instance.Map;

    public PawnPath CurrentMovingPath;

    public PathMover(Pawn pawn)
    {
        RegisterPawn = pawn;
    }

    public void Tick() {
        if (CurrentMovingPath is {Using:false}) {
            //没有正在移动的路径,返回
            return;
        }

        //TODO:根据当前的路径点移动物体
        if (CurrentMovingPath.GetCurrentPosition() is {} currentNode) {
            if (currentNode.FastDistance(RegisterPawn.transform.position) > Mathf.Epsilon) {
                //TODO:还没有重合,将Pawn朝目标点移动

            }
        }
    }
}

public class PawnPath
{
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

    public PathNode GetCurrentPosition() {
        if (End) {
            return null;
        }

        return FindingPath[CurMovingIndex];
    }

    public PathNode GetNextPosition()
    {
        if (End)
        {
            return null;
        }

        return FindingPath[++CurMovingIndex];
    }
}
