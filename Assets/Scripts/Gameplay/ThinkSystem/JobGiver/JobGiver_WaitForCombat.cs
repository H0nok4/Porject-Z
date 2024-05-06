using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSystem {
    public class JobGiver_WaitForCombat : ThinkNode_JobGiver{
        public override Job TryGiveJob(Thing_Unit unit)
        {
            //TODO:现在的表现就是站着发呆


            return JobMaker.MakeJob(ConfigType.DataManager.Instance.GetJobDefineByID(1));
        }
    }
}
