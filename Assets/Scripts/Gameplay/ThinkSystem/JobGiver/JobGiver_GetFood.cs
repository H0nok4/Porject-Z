using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

namespace ThinkSystem{
    public class JobGiver_GetFood : ThinkNode_JobGiver{
        public override Job TryGiveJob(Thing_Unit unit) {
            //TODO:如果当前的饱食度到达了饥饿状态,需要找到可以吃的食物
            if (unit.NeedTracker.Food != null && unit.NeedTracker.Food.HungryStage == HungryStageType.Starvation) {
                var getFoodJob = JobMaker.MakeJob(DataManager.Instance.GetJobDefineByID(7));
            }

            return null;
        }
    }
}
