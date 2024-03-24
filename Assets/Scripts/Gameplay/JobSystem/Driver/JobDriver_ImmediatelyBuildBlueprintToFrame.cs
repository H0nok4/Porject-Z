using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class JobDriver_ImmediatelyBuildBlueprintToFrame : JobDriver {
    public override IEnumerable<Work> MakeWorks() {
        yield return Work_MoveTo.MoveToThing(JobTargetIndex.A, PathMoveEndType.Touch);
        //TODO:为了防止站在蓝图上建造，需要再走一边
        yield return Work_MoveTo.MoveOffToThing(JobTargetIndex.A);
        yield return Work_Build.BuildBlueprintToFrameIfNeed(JobTargetIndex.A);
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {
        return Unit.Reserve(Job.GetTarget(JobTargetIndex.A), Job);
    }
}