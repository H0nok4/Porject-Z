using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Thing_Unit : ThingWithComponent , IThingHolder {
    private const int MaxTickPerMove = 600;

    public ThingUnit_JobTracker JobTracker;

    public ThingUnit_JobThinker JobThinker;

    public ThingUnit_WorkSetting WorkSetting;

    public PathMover PathMover;
    public int TickPerMoveDiagonal => TicksPerMove(true);
    public int TickPerMoveCardinal => TicksPerMove(false);
    /// <summary>
    /// ��������ƶ�һ����Ҫ����Tick
    /// </summary>
    /// <param name="cardinal"></param>
    /// <returns></returns>
    public int TicksPerMove(bool cardinal)
    {
        //����5������٣�������60Tick�п�����5�񣬵���12Tickÿ��
        var moveSpeed = MoveSpeed; 

        float perSecondMoveCount = GameTicker.TicksPerSecond / moveSpeed;

        if (perSecondMoveCount < 1)
        {
            //���ٶ���1Tick��1��
            perSecondMoveCount = 1;
        }

        if (cardinal)
        {
            //�Խ�����Ҫ*1.414
            perSecondMoveCount *= 1.414f;
        }

        return Math.Clamp(Mathf.FloorToInt(perSecondMoveCount), 1, MaxTickPerMove);
    }

    public override void Tick()
    {
        PathMover.Tick();

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

        GameTicker.Instance.RegisterThing(this);
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
            return base.ParentHolder;
        }
    }


    public void GetChildren(List<IThingHolder> outChildren)
    {
        //TODO:�����ɫ���ܻ��б��������ϵ�װ���ã�����Я���Ķ���

    }

    public ThingOwner GetHoldingThing()
    {
        return null;
    }


    public bool IsInside(Thing thing)
    {
        //TODO:Ŀǰֻ�е����С�����壬������������趨����Ҫ������
        if (Position.MapDataIndex != thing.Position.MapDataIndex)
        {
            return false;
        }

        return this.Position.Pos.X == thing.Position.Pos.X && this.Position.Pos.Y == thing.Position.Pos.Y;
    }
}