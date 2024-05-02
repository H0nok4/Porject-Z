using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkSystem {
    public class ThinkNode_ConditionDraft : ThinkNode_Condition {
        protected override bool Satisfied(Thing_Unit unit)
        {
            return unit.IsDrafted;
        }
    }
}
