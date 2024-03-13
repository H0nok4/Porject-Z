using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Thing_Unit : Thing {

    public ThingUnit_JobTracker JobTracker;

    public ThingUnit_JobThinker JobThinker;

    public PathMover PathMover;

    public override void Tick()
    {

    }

    public Thing_Unit(GameObject gameObject, MapData mapData, IntVec2 position) : base(gameObject,mapData,position) {
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