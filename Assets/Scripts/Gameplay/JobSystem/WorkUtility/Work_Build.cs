
using ConfigType;

public static class Work_Build {
    public static Work BuildBlueprintToFrameIfNeed(JobTargetIndex blueprintIndex,JobTargetIndex buildFrameSetToTargetIndex = JobTargetIndex.None) {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Thing_Unit_Pawn unit = (Thing_Unit_Pawn)work.Unit;
            Job curJob = unit.JobTracker.Job;
            if (curJob.GetTarget(blueprintIndex).Thing is Blueprint blueprint)
            {
                bool needToUpdate = buildFrameSetToTargetIndex != JobTargetIndex.None &&
                                    curJob.GetTarget(buildFrameSetToTargetIndex).Thing == blueprint;
                if (blueprint.TryReplaceWithSolidThing(unit, out Thing createdThing, out bool jobEnd)) {
                    curJob.SetTarget(blueprintIndex, createdThing);
                    if (needToUpdate)
                    {
                        curJob.SetTarget(buildFrameSetToTargetIndex, createdThing);
                    }

                    if (createdThing is Thing_Building_Frame frame) {
                        unit.Reserve(frame, curJob);
                    }
                }

            }
        };
        return work;
    }
}