using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Thing_Unit
{
    //TODO:小人应该有体型，特性等额外的东西


    public override void Tick()
    {
        PathMover.Tick();

        if (JobTracker != null)
        {
            JobTracker.JobTrackTick();
        }
    }

    public Pawn(ThingObject gameObject, MapData mapData, IntVec2 position) : base(gameObject, mapData, position)
    {

    }
}


