using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour,IThing
{
    public PathMover PathMover;

    public PawnPath FindingPath;

    private IntVec2 _position = IntVec2.Invalid;

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

    public PawnPath CurrentMovingPath;

    public PathMover(Pawn pawn)
    {
        RegisterPawn = pawn;
    }
}

public class PawnPath
{
    public List<PathNode> FindingPath;

    /// <summary>
    /// 当前前往的格子索引
    /// </summary>
    public int CurMovingIndex;

    /// <summary>
    /// 是否正在寻路
    /// </summary>
    public bool Using;

    public bool End => CurMovingIndex == FindingPath.Count;
    public PathNode GetNextPosition()
    {
        if (End)
        {
            return null;
        }

        return FindingPath[++CurMovingIndex];
    }
}
