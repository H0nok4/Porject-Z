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