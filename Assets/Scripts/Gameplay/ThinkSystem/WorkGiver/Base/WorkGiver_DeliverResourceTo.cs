using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public abstract class WorkGiver_DeliverResourceTo : WorkGiver_Scanner {


    private static List<DefineThingClassCount> MissingResources = new List<DefineThingClassCount>();

    public Job DeliverResourceJobFor(Thing_Unit unit, IBuildable build) {
        MissingResources.Clear();
        foreach (var defineCount in build.NeedResources()) {
            //TODO:找到地图上存在的允许互动的同类物品运过去
            var exitsThing = new List<Thing>();
            exitsThing.AddRange(MapController.Instance.Map.ListThings.GetThingsByThingDefine(defineCount.Def));
            if (exitsThing.Count > 0) {
                //TODO:先暂时直接获取所有物品数量来测试
                int avaliableItemCount = exitsThing.Sum((thing) => thing.Count);
                Debug.Log($"当前有{avaliableItemCount}个物品");
                var haulToContainerJob = JobMaker.MakeJob(DataManager.Instance.GetJobDefineByID(6));
                haulToContainerJob.SetTarget(JobTargetIndex.A, exitsThing[0]);
                exitsThing.RemoveAt(0);
                //haulToContainerJob.InfoQueueA = new List<JobTargetInfo>();
                //for (int i = 0; i < exitsThing.Count; i++)
                //{
                //    haulToContainerJob.InfoQueueA.Add(exitsThing[i]);
                //}
                haulToContainerJob.SetTarget(JobTargetIndex.B, (Thing)build);

                //TODO:可能会有多个需要资源的建筑，能的话就一趟拿完，尽量拿齐之后按顺序把资源放到蓝图那边
                var needResourcesBuilding =
                    FindNearbyNeeders(unit, defineCount, build, avaliableItemCount, out int needItemNum);

                needResourcesBuilding.Add((Thing)build);

                haulToContainerJob.InfoC = (Thing)build;
                haulToContainerJob.Count = defineCount.Count;
                haulToContainerJob.HaulMode = HaulMode.ToContainer;
                return haulToContainerJob;
            }

            MissingResources.Add(defineCount);

        }

        if (MissingResources.Count > 0) {
            Debug.LogError("没有找到材料来建造");
        }

        return null;
    }

    private HashSet<Thing> FindNearbyNeeders(Thing_Unit unit, DefineThingClassCount defineCount, IBuildable build, int avaliableItemCount, out int needItemCount) {
        //TODO:暂时用找到所有相同的建筑蓝图来测试
        needItemCount = defineCount.Count;
        var result = new HashSet<Thing>();
        var allThing = MapController.Instance.Map.ListThings.GetThingsByGroup(ThingRequestGroup.BuildingBlueprint);
        foreach (var thing in allThing) {
            if (needItemCount >= avaliableItemCount) {
                break;
            }

            if (!(thing is Blueprint blueprint)) {
                continue;
            }

            int otherThingNeedCount = BuildUtility.GetNeedItemCount((IBuildable)thing, defineCount.Def);
            if (otherThingNeedCount > 0) {
                result.Add(thing);
                needItemCount += otherThingNeedCount;
            }

        }

        return result;
    }
}