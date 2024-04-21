using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public static class WorkUtility_Reserve {
    public static Work ReserveTarget(JobTargetIndex index,int maxUnit = 1,int stackCount = -1)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = () =>
        {
            var job = work.Unit.JobTracker.Job;
            var target = job.GetTarget(index);
            if (!work.Unit.Reserve(target,job,maxUnit,stackCount))
            {
                work.Unit.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
            }
        };
        work.CompleteMode = WorkCompleteMode.Instant;
        work.AtomicWithPrevious = true;
        return work;
    }
}