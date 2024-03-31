using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class JobGiver_WalkAround : ThinkNode_JobGiver {
    public override Job TryGiveJob(Thing_Unit unit) {
        Job walkAroundJob = JobMaker.MakeJob(ConfigType.DataManager.Instance.GetJobDefineByID(3));
        var canWalkSection = PathFinder.GetMoveableSectionByBFS(unit,5);
        walkAroundJob.InfoA = new JobTargetInfo()
            { Position = canWalkSection[UnityEngine.Random.Range(1, canWalkSection.Count)] };
        return walkAroundJob;
    }
}