using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JobDriver_MoveTo : JobDriver  {
    public override IEnumerable<Work> MakeWorks() {
        var moveWork = Work_MoveTo.MoveToCell(JobTargetIndex.A, PathMoveEndType.InCell);
        moveWork.CompleteMode = WorkCompleteMode.PathMoveEnd;
        moveWork.InitAction = delegate
        {
            Debug.Log($"开始移动叻");
        };
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


        return true;
    }
}