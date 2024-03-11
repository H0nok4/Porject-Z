using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobDriver_MoveTo : JobDriver  {
    public override IEnumerable<Work> MakeWorks() {
        var moveWork = Work_MoveTo.MoveToCell(JobTargetIndex.B, PathMoveEndType.InCell);
        yield return moveWork;
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {


        return true;
    }
}