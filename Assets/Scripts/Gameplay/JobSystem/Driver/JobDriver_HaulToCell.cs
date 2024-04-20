using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public class JobDriver_HaulToCell : JobDriver
{
    public override IEnumerable<Work> MakeWorks()
    {
        //IndexA = 需要移动的物体 IndexB = 需要移动到的位置
        //TODO:需要考虑到可能不能一次性搬完
        var moveToThingWork = Work_MoveTo.MoveToThing(JobTargetIndex.A, PathMoveEndType.Touch);
        var carryThing = Work_Haul.StartCarryThing(JobTargetIndex.A);
        var moveToCell = Work_MoveTo.MoveToCell(JobTargetIndex.B, PathMoveEndType.Touch);
        //TODO:需要考虑到，可能目标位置是个容器，需要放在容器里面
        var putDownToCell = Work_Haul.PutHauledThingIntoCell(JobTargetIndex.B);
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
        Unit.ReserveAsManyAsPossible(Job.InfoListA, Job);
        return true;
    }
}
