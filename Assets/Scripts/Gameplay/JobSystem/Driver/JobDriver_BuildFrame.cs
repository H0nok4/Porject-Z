using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDriver_BuildFrame : JobDriver
{
    public override IEnumerable<Work> MakeWorks() {
        //TODO:建造一个未完成的建筑,首先得先走到目标位置
        var moveTo = Work_MoveTo.MoveToCell()
        //TODO:然后不断减少建筑的剩余工作量
        //TODO:在完成后,将Frame替换成实际建筑
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {
        ReservationManager.Instance.Reserve(Unit, Job, Job.GetTarget(JobTargetIndex.A));
        return true;
    }
}
