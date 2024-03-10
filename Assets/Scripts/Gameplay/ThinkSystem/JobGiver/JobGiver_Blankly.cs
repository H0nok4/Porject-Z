using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobGiver_Blankly : ThinkNode_JobGiver {
    public override Job TryGiveJob(Thing_Unit unit)
    {
        Job blanklyJob = JobMaker.MakeJob(Test_JobDefine_Blankly.BlanklyJob);
        return blanklyJob;
    }
}