using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Gameplay.ThinkSystem.JobGiver {
    public class JobGiver_GetWork : ThinkNode
    {
        public override ThinkResult GetResult(Thing_Unit unit)
        {
            var workGiverList = unit.WorkSetting.UsedWorkGivers;


            return ThinkResult.NoJob;
        }
    }
}
