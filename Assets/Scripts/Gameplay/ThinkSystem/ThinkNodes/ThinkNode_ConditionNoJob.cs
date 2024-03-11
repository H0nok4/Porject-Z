using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Gameplay.ThinkSystem.ThinkNodes {
    public class ThinkNode_ConditionNoJob : ThinkNode_Condition {
        protected override bool Satisfied(Thing_Unit unit)
        {
            if (unit.JobTracker.Job != null)
            {
                return false;
            }

            return true;
        }
    }
}
