using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDriver_BuildFrame : JobDriver
{
    public override IEnumerable<Work> MakeWorks() {
        //TODO:����һ��δ��ɵĽ���,���ȵ����ߵ�Ŀ��λ��
        var moveTo = Work_MoveTo.MoveToCell()
        //TODO:Ȼ�󲻶ϼ��ٽ�����ʣ�๤����
        //TODO:����ɺ�,��Frame�滻��ʵ�ʽ���
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {
        ReservationManager.Instance.Reserve(Unit, Job, Job.GetTarget(JobTargetIndex.A));
        return true;
    }
}
