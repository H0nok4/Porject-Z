using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class JobDriver_WalkAround : JobDriver {
    public override IEnumerable<Work> MakeWorks()
    {
        //TODO:开始闲逛时，JobGiver会随机选择使用广度优先（或者迪杰斯特拉）算法获取的半径3格的某个地方，然后将那个地方设置为闲逛的目标点
        var walkWork = Work_MoveTo.MoveToCell(JobTargetIndex.A, PathMoveEndType.InCell);
        yield return walkWork;
        var endWork = WorkMaker.MakeWork();
        endWork.InitAction = delegate {
            Debug.Log("移动结束叻");
        };
        endWork.CompleteMode = WorkCompleteMode.Instant;
        yield return endWork;
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed)
    {
        //TODO:后面需要将目标点加到预定中
        if (ReservationManager.Instance.Reserve(Unit, Job, Job.InfoA)) {
            Debug.Log("工作-成功加入目标点到预订列表中");
            return true;
        }
        return true;
    }

}