using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Thing_Unit : Thing {
    private const int MaxTickPerMove = 600;

    public ThingUnit_JobTracker JobTracker;

    public ThingUnit_JobThinker JobThinker;

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

    }

    public Thing_Unit(ThingObject gameObject, MapData mapData, IntVec2 position) : base(gameObject,mapData,position) {
        JobTracker = new ThingUnit_JobTracker(this);
        PathMover = new PathMover(this);
        JobThinker = new ThingUnit_JobThinker();
        JobThinker.ThinkTreeDefine = PawnThinkTree.Instance;
        ThingType = ThingCategory.Unit;
        Spawn();
    }

    public void Spawn()
    {
        GameTicker.Instance.RegisterThing(this);
    }

    public void DeSpawn()
    {
        GameTicker.Instance.UnRegisterThing(this);
    }
}