using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;
using UnityEngine.UIElements;
using ThinkSystem;
public abstract class Thing_Unit : ThingWithComponent , IThingHolder {
    private const int MaxTickPerMove = 600;

    public ThingUnit_JobTracker JobTracker;

    public ThingUnit_JobThinker JobThinker;

    public ThingUnit_WorkSetting WorkSetting;

    public ThingUnit_CarryTracker CarryTracker;

    public ThingUnit_NeedTracker NeedTracker;

    public ThingUnit_DraftTracker DraftTracker;

    public PathMover PathMover;

    public bool IsDraft
    {
        get => DraftTracker != null && DraftTracker.IsDraft;
        set
        {
            if (DraftTracker != null)
            {
                DraftTracker.IsDraft = value;
            }
        }
    }
    public int TickPerMoveDiagonal => TicksPerMove(true);
    public int TickPerMoveCardinal => TicksPerMove(false);
    /// <summary>
    /// 最基础的移动一格需要多少Tick
    /// </summary>
    /// <param name="cardinal"></param>
    /// <returns></returns>
    public int TicksPerMove(bool cardinal)
    {
        //比如5格的移速，等于在60Tick中可以走5格，等于12Tick每格
        var moveSpeed = MoveSpeed; 

        float perSecondMoveCount = GameTicker.TicksPerSecond / moveSpeed;

        if (perSecondMoveCount < 1)
        {
            //最少都得1Tick走1格
            perSecondMoveCount = 1;
        }

        if (cardinal)
        {
            //对角线需要*1.414
            perSecondMoveCount *= 1.414f;
        }

        return Math.Clamp(Mathf.FloorToInt(perSecondMoveCount), 1, MaxTickPerMove);
    }

    public override void Tick()
    {
        PathMover.Tick();
        NeedTracker.Tick();

        if (JobTracker != null) {
            JobTracker.JobTrackTick();
        }
    }

    public override void SpawnSetup(MapData mapData)
    {
        base.SpawnSetup(mapData);
        JobTracker = new ThingUnit_JobTracker(this);
        PathMover = new PathMover(this);
        JobThinker = new ThingUnit_JobThinker();
        JobThinker.ThinkTreeDefine = PawnThinkTree.Instance;
        ThingType = ThingCategory.Unit;
        WorkSetting = new ThingUnit_WorkSetting(this);
        CarryTracker = new ThingUnit_CarryTracker(this);
        NeedTracker = new ThingUnit_NeedTracker(this);
        DraftTracker = new ThingUnit_DraftTracker(this);
        GameTicker.Instance.RegisterThing(this);

        UpdateFogOfWar();
    }


    public override void DeSpawn()
    {
        base.DeSpawn();
        GameTicker.Instance.UnRegisterThing(this);
    }

    public IThingHolder Parent
    {
        get
        {
            return base.ParentOwner;
        }
    }

    public bool IsDead { get; set; }
    public bool IsDown { get; set; }
    public bool IsColonist { get; set; }
    public bool IsDrafted { get; set; }


    public void GetChildren(List<IThingHolder> outChildren)
    {
        //TODO:例如角色可能会有背包，身上的装备烂，手上携带的东西

    }

    public ThingOwner GetCurrentHoldingThings()
    {
        return null;
    }


    public bool IsInside(Thing thing)
    {
        //TODO:目前只有单格大小的物体，后续加入体积设定后需要重新做
        if (Position.MapDataIndex != thing.Position.MapDataIndex)
        {
            return false;
        }

        return this.Position.Pos.X == thing.Position.Pos.X && this.Position.Pos.Y == thing.Position.Pos.Y;
    }

    public void UpdateFogOfWar()
    {
        var posSet = FogOfWarUtility.GetUnitRangeVisiblePos(Position.Pos, Position.MapDataIndex, 3);
        FogManager.Instance.UpdateFOWUnit(this,Position.MapDataIndex,posSet);
    }
}