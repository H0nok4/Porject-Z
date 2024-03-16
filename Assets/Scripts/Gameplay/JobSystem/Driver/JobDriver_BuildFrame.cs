using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDriver_BuildFrame : JobDriver {
    private Frame frameBuilding => (Frame)Unit.JobTracker.Job.GetTarget(JobTargetIndex.A).Thing;
    public override IEnumerable<Work> MakeWorks() {
        //TODO:建造一个未完成的建筑,首先得先走到目标位置
        var moveTo = Work_MoveTo.MoveToCell(JobTargetIndex.B, PathMoveEndType.Touch);
        //TODO:需要考虑有时候是会移动的Thing,所以需要修改寻路类能够设置一个目标点后自动开始寻路并且在寻路过程中能够去获取最新的位置和路径
        moveTo.InitAction = delegate {
            Debug.Log($"前往目的地:{frameBuilding.Position}");
        };
        yield return moveTo;
        //TODO:然后不断减少建筑的剩余工作量

        //TODO:在完成后,将Frame替换成实际建筑
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {
        ReservationManager.Instance.Reserve(Unit, Job, Job.GetTarget(JobTargetIndex.A));
        return true;
    }
}
