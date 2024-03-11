using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobGiver_WalkAround : ThinkNode_JobGiver {
    public override Job TryGiveJob(Thing_Unit unit) {
        Job walkAroundJob = JobMaker.MakeJob(Test_JobDefine_WalkAround.WalkAroundDefine);
        var canWalkSection = PathFinder.GetMoveableSectionByBFS(unit,3);
        walkAroundJob.InfoA = new JobTargetInfo()
            { Section = canWalkSection[UnityEngine.Random.Range(1, canWalkSection.Count)].ToSection() };
        return walkAroundJob;
    }
}