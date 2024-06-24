using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class JobDriver_GetFoodAndEat : JobDriver{
    public override IEnumerable<Work> MakeWorks() {
        //TODO:移动到目标位置,捡起来,然后原地吃
        var reserveWork = WorkUtility_Reserve.ReserveTarget(JobTargetIndex.A);
        yield return reserveWork;
        yield return WorkUtility_MoveTo.MoveToThing(JobTargetIndex.A, PathMoveEndType.Touch);
        yield return WorkUtility_Haul.StartCarryThing(JobTargetIndex.A);
        //TODO:吃
        yield return WorkUtility_Food.StartEat(JobTargetIndex.A);

    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {
        if (!Unit.Reserve(Job.GetTarget(JobTargetIndex.A),Job))
        {
            return false;
        }

        return true;
    }
}