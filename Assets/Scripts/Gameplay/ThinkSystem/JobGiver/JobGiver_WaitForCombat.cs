using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSystem {
    public class JobGiver_WaitForCombat : ThinkNode_JobGiver{
        public override Job TryGiveJob(Thing_Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
