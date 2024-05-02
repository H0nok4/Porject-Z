using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class JobDriver_MoveTo : JobDriver  {
    public override IEnumerable<Work> MakeWorks() {
        var moveWork = WorkUtility_MoveTo.MoveToCell(JobTargetIndex.A, PathMoveEndType.InCell);
        yield return moveWork;
        var endWork = WorkMaker.MakeWork();
        endWork.InitAction = delegate
        {
            Debug.Log("移动结束叻");
        };
        endWork.CompleteMode = WorkCompleteMode.Instant;
        yield return endWork;
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {

        //TODO:将目标点加入到预定中
        if (ReservationManager.Instance.Reserve(Unit,Job,Job.InfoA)) {
            Debug.Log("工作-成功加入目标点到预订列表中");
            return true;
        }

        return false;
    }
}