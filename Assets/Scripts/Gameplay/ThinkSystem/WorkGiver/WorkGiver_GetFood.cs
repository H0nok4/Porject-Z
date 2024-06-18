using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

namespace ThinkSystem{
    public class WorkGiver_GetFood : WorkGiver_Scanner {
        public override ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.Item);

        public override Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false) {
            //TODO:如果当前的饱食度到达了饥饿状态,需要找到可以吃的食物
            if (thing is not Thing_Item {Ingestible: true} item || item.IngestibleEffect.RecoverHungry <= 0) {
                return null;
            }

            //TODO:如果没有食物的话可能会反复进入这个状态导致卡死,只能隔一段时间判断一次
            if (unit.NeedTracker.Food != null && unit.NeedTracker.Food.HungryStage == HungryStageType.Starvation && unit.NeedTracker.Food.CanTrySatisfied()) {
                var getFoodJob = JobMaker.MakeJob(DataManager.Instance.GetJobDefineByID(7));
                return getFoodJob;
            }

            return null;
        }
    }
}
