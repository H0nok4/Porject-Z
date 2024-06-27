using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

namespace ThinkSystem{
    public class JobGiver_GetFood : ThinkNode_JobGiver {
        public override Job TryGiveJob(Thing_Unit unit) {
            //TODO:如果当前的饱食度到达了饥饿状态,需要找到可以吃的食物
            if (unit.NeedTracker.Food.HungryStage > HungryStageType.WantFood) {
                return null;
            }

            //TODO:如果没有食物的话可能会反复进入这个状态导致卡死,只能隔一段时间判断一次
            if (unit.NeedTracker.Food != null && unit.NeedTracker.Food.HungryStage <= HungryStageType.WantFood && unit.NeedTracker.Food.CanTrySatisfied()) {
                var getFoodJob = JobMaker.MakeJob(DataManager.Instance.GetJobDefineByID(7));
                var items = MapController.Instance.Map.ListThings.ThingsMatching(
                    new ThingRequest() {Group = ThingRequestGroup.Item});
                Thing_Item food = null;
                //TODO:遍历所有物品找到可以恢复饱食度的
                foreach (var thing in items) {
                    if (thing is Thing_Item item && item.Ingestible && item.IngestibleEffect.RecoverHungry > 0) {
                        food = item;
                        break;
                    }
                }

                if (food != null) {
                    getFoodJob.InfoA = food;
                    return getFoodJob;
                }

            }

            return null;
        }
    }
}
