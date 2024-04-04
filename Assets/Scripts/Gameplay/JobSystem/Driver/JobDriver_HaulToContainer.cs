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
        //TODO:������һ�����ö��������Ĳ���ʱ����Ҫ�Ѷ����е����嶼�ӽ���
        Unit.ReserveAsManyAsPossible(Job.InfoQueueA, Job);
        return true;
    }
}
