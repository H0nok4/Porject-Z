using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public static class Work_Haul {
    public static bool CanCarryThing(Thing_Unit unit, Thing haulThing)
    {
        if (!haulThing.Spawned)
        {
            Debug.LogError("想要拣起不存在的东西");
            return false;
        }

        if (haulThing.Count == 0)
        {
            Debug.LogError("想要捡起数量为0的东西");
            return false;
        }

        if (unit.JobTracker.Job.Count <= 0)
        {
            unit.JobTracker.Job.Count = 1;
        }

        return true;
    }
    public static Work StartCarryThing(JobTargetIndex targetIndex)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Thing_Unit unit = work.Unit;
            Job curJob = unit.JobTracker.Job;
            Thing targetThing = curJob.GetTarget(targetIndex).Thing;
            if (CanCarryThing(unit,targetThing))
            {
                
            }
        };

        return work;
    }
}