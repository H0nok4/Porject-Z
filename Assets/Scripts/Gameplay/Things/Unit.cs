using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class Thing_Unit : Thing {

    public ThingUnit_JobTracker JobTracker;

    public PathMover PathMover;

    public override void Tick()
    {
        //TODO:查看是否当前有寻路的
        PathMover.Tick();
    }

    public Thing_Unit(GameObject gameObject, MapData mapData, IntVec2 position) : base(gameObject,mapData,position) {
        JobTracker = new ThingUnit_JobTracker(this);
        PathMover = new PathMover(this);

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