using System;
using JetBrains.Annotations;
using UnityEngine;

public class MoveTargetInfo
{
    public PosNode CellTarget;

    public Thing ThingTarget;

    public PosNode Position
    {
        get
        {
            if (CellTarget != null)
            {
                return CellTarget;
            }

            if (ThingTarget.Spawned)
            {
                return ThingTarget.Position;
            }

            return null;
        }
    }

    public PosNode MapNode
    {
        get
        {
            if (IsTrackThing)
            {
                return ThingTarget.MapData.GetSectionByPosition(ThingTarget.Position.Pos).CreatePathNode();
            }

            return CellTarget;
        }
    }

    public PathMoveEndType EndType;

    public MoveTargetInfo(PosNode cellTarget, Thing thingTarget, PathMoveEndType endType) {
        CellTarget = cellTarget;
        ThingTarget = thingTarget;
        EndType = endType;
    }



    /// <summary>
    /// 因为物体可能会移动，所以在追踪Thing的时候需要不时更新位置
    /// </summary>
    public bool IsTrackThing => ThingTarget != null;
}

public class PathMover {
    public Thing_Unit RegisterPawn;

    public Map AllMap => MapController.Instance.Map;

    public PawnPath CurrentMovingPath;

    private Action _onComplete;

    private Action _onPathMoveFail;

    private MoveTargetInfo _targetInfo;

    public int MoveToNextPosTickTotal;

    public int MoveToNextPosTickLeft;

    public int PreRefreshTargetThingPositionTick;

    public IntVec2 PreTargetThingPosition;
    public MoveTargetInfo MoveTarget => _targetInfo;

    public bool IsMoving;

    public PathMover(Thing_Unit pawn) {
        RegisterPawn = pawn;
    }

    private void SetMoveTarget(MoveTargetInfo target)
    {
        
        _targetInfo = target;
        if (IsMoving) {
            //TODO:当前有正在行进的路线，需要

        }

        var node = MoveTarget.MapNode;
        var path = PathFinder.AStarFindPath(RegisterPawn, node, MoveTarget.EndType);
        StartPath(new PawnPath(path));
        //TODO:设置的时候更新一下路径

    }

    public void SetMoveTarget(PosNode posNode,PathMoveEndType endType)
    {
        if (_targetInfo != null && posNode == _targetInfo.Position) {
            //设置成相同的位置，如果正在移动的话就不需要更新
            //TODO:设置成相同的目标，如果正在移动的话就不需要更新
            Debug.LogError("设置成了相同的目标");
            return;
        }
        SetMoveTarget(new MoveTargetInfo(posNode, null, endType));
    }

    public void SetMoveTarget(Thing thing,PathMoveEndType endType)
    {
        if (_targetInfo != null && thing == _targetInfo.ThingTarget && IsMoving)
        {
            //TODO:设置成相同的目标，如果正在移动的话就不需要更新
            Debug.LogError("设置成了相同的目标");
            return;
        }


        SetMoveTarget(new MoveTargetInfo(null,thing, endType));
    }

    public void Tick() {
        //移动已经结束勒
        if (!IsMoving)
        {
            return;
        }

        if (MoveTarget == null || CurrentMovingPath == null)
        {
            Debug.LogError("移动未结束，但是没有目标");
            return;
        }

        if (MoveTarget.IsTrackThing)
        {
            //TODO:追踪一个Thing，需要不断检查Thing的位置是否变动，虽然不一定每一帧都要更新位置
            if (GameTicker.Instance.CurrentTick - PreRefreshTargetThingPositionTick >= 60)
            {
                //TODO:暂定每秒更新一次
                //TODO:后面还是要换成PosNode,因为可能会有上楼下楼Pos不变但是MapDataIndex改变的情况
                if (PreTargetThingPosition != MoveTarget.Position.Pos)
                {
                    //TODO:更新路径

                }
            }

        }
        
        TweenUnit();
    }

    public void TweenUnit()
    {
        //TODO:如果下一格的位置为门，需要等待门完全打开才能进入

        
        MoveToNextPosTickLeft--;

        if (MoveToNextPosTickLeft <= 0)
        {
            //TODO:已经走到下一格格子了
            TryEnterTile(CurrentMovingPath.GetCurrentPosition());
        }
        else
        {
            float movePercent = 1 - (MoveToNextPosTickLeft / (float)MoveToNextPosTickTotal);
            RegisterPawn.GameObject.GO.transform.position = Vector3.Lerp(RegisterPawn.Position.Pos.ToVector3(),
                 CurrentMovingPath.GetCurrentPosition().Pos.ToVector3(), movePercent);
        }
    }

    public void StartPath(PawnPath path)
    {


        //TODO:
        if (IsMoving) {
            //TODO:下个移动点还是保持不变,所以时间也不需要变
            path.FindingPath.Insert(0, CurrentMovingPath.GetCurrentPosition());
            CurrentMovingPath = path;
        }

        CurrentMovingPath = path;

        if (CurrentMovingPath.Length == 0) {
            IsMoving = false;
            RegisterPawn.JobTracker.OnPathMoveEnd();
            return;
        }

        //TODO:更新下一个位置，到下一个位置的总时间
        var nextPos = path.GetCurrentPosition();
        MoveToNextPosTickTotal = CalculateCostToNextPosition(nextPos.Pos);
        MoveToNextPosTickLeft = MoveToNextPosTickTotal;
        Debug.Log($"当前MoveTick需要：{MoveToNextPosTickTotal}");

        IsMoving = true;
    }

    public int CalculateCostToNextPosition(IntVec2 position)
    {
        return CalculateCostToNextPosition(RegisterPawn, position);
    }

    public int CalculateCostToNextPosition(Thing_Unit unit, IntVec2 position)
    {
        //TODO:根据速度，地形计算到下一个位置的时间
        var baseMoveTick = (unit.Position.Pos.X != position.X && unit.Position.Pos.Y != position.Y) ? unit.TickPerMoveDiagonal : unit.TickPerMoveCardinal;
        //TODO:根据不同的地形会有不同的移速惩罚，对应就是加移动的Tick数量
        var moveToNextCelloFactor = GetMoveFactorAt(position);
        //TODO:格子上的物品，建筑会增加固定的移动Tick

        var result = baseMoveTick / moveToNextCelloFactor;

        if (result <= 0)
        {
            result = 1;
        }

        return Mathf.CeilToInt(result);
    }

    public int GetThingCostAt(IntVec2 pos,MapData map)
    {
        int result = 0;
        var things = map.ThingMap.ThingsAt(pos);
        foreach (var thing in things)
        {
            result += GetThingMoveCost(thing.Def);
        }

        return result;
    }

    private int GetThingMoveCost(Define_Buildable thing)
    {
        return thing.MoveCost;
    }

    public float GetMoveFactorAt(IntVec2 pos)
    {
        //TODO:后面不同的地形配置不同的移速因子
        return 1;
    }

    public void TryEnterTile(PosNode node) {
        //TODO:每次进入一个新的格子，需要发出事件

        //TODO:可能从其他地图进来的
        RegisterPawn.SetPosition(node.Pos.Copy(), node.MapDataIndex);

        //TODO:更新下一格的位置，如果下一格是门之类的需要等待打开门
        CurrentMovingPath.CurMovingIndex++;
        if (CurrentMovingPath.End) {
            IsMoving = false;
            RegisterPawn.JobTracker.OnPathMoveEnd();

        }
        else
        {
            MoveToNextPosTickTotal = CalculateCostToNextPosition(CurrentMovingPath.GetCurrentPosition().Pos);
            MoveToNextPosTickLeft = MoveToNextPosTickTotal;
            Debug.Log($"当前MoveTick需要：{MoveToNextPosTickTotal}");
        }
    }
}