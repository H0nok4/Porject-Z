using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public class JobDriver_HaulToContainer : JobDriver
{
    public override IEnumerable<Work> MakeWorks()
    {
        Work goToThingWork = Work_MoveTo.MoveToThing(JobTargetIndex.A, PathMoveEndType.Touch);

        yield return goToThingWork;
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed)
    {
        if (!Unit.Reserve(Job.GetTarget(JobTargetIndex.A),Job))
        {
            return false;
        }

        if (!Unit.Reserve(Job.GetTarget(JobTargetIndex.B),Job))
        {
            return false;
        }
        //TODO:后面做一次性拿多个建筑物的材料时，需要把队列中的物体都加进来
        Unit.ReserveAsManyAsPossible(Job.InfoQueueA, Job);
        return true;
    }
}
