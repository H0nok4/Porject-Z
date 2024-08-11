using ConfigType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobDriver_SearchContainerAndHaul : JobDriver {
    public override IEnumerable<Work> MakeWorks() {
        //TODO:需要分情况,如果目标没有被搜索过,或者搜索到一半,需要重新搜索一下

    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {

        //TODO:将目标容器加入到预定中
        if (ReservationManager.Instance.Reserve(Unit, Job, Job.InfoA)) {
            //Debug.Log("工作-成功加入目标点到预订列表中");
            return true;
        }

        return false;
    }
}