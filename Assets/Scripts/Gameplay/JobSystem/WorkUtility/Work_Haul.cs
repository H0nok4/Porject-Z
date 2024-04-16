using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public static class Work_Haul {
    public static bool CanCarryThing(Thing_Unit unit, Thing haulThing)
    {
        if (!haulThing.Spawned)
        {
            Debug.LogError("想要拣起不存在的东西");
            return false;
        }

        if (haulThing.Count == 0)
        {
            Debug.LogError("想要捡起数量为0的东西");
            return false;
        }

        if (unit.JobTracker.Job.Count <= 0)
        {
            unit.JobTracker.Job.Count = 1;
        }

        return true;
    }

    public static Work CarryThingToContainer()
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            var unit = work.Unit;
            var targetThing = unit.JobTracker.Job.GetTarget(JobTargetIndex.B);
            work.Unit.PathMover.StartPath(new PawnPath(PathFinder.AStarFindPath(unit, targetThing.Thing.Position,PathMoveEndType.Touch)));
        };
        //TODO:添加失败条件
        work.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return work;
    }

    public static Work StartCarryThing(JobTargetIndex targetIndex,bool failIfCountLessThanNeed = false,bool reserve = true)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Thing_Unit unit = work.Unit;
            Job curJob = unit.JobTracker.Job;
            Thing targetThing = curJob.GetTarget(targetIndex).Thing;
            if (CanCarryThing(unit,targetThing))
            {
                int thingSpace = unit.CarryTracker.GetThingSpaceCountByDef(targetThing.Def);
                if (thingSpace == 0)
                {
                    throw new Exception("搬运工作想要搬运无法携带的东西");
                }

                if (failIfCountLessThanNeed && targetThing.Count < curJob.Count)
                {
                    unit.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
                }
                else
                {
                    int canCarryNum = Mathf.Min(thingSpace, curJob.Count, targetThing.Count);
                    if (canCarryNum <= 0)
                    {
                        throw new Exception("可搬运的数量为0");
                    }

                    var stackNum = targetThing.Count;
                    var carryNum = unit.CarryTracker.TryCarryThing(targetThing, canCarryNum, reserve);
                    if (carryNum == 0)
                    {
                        unit.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
                    }
                    //TODO:可能需要拿多个物体才能凑齐
                    //if (carryNum < stackNum)
                    //{
                    curJob.Count -= carryNum;

                    //}
                }

            }
        };

        return work;
    }

    public static Work PutHauledThingIntoContainer(JobTargetIndex containerIndex, JobTargetIndex reversContainerIndex, Action onGive = null)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Thing_Unit unit = work.Unit;
            Job curJob = unit.JobTracker.Job;
            if (unit.CarryTracker.CarriedThing == null)
            {
                Debug.LogError("有搬运物体的工作但是单位没有拿物体");
            }
            else
            {
                var thing = curJob.GetTarget(containerIndex).Thing;
                var thingOwner = thing.TryGetThingOwner();
                if (thingOwner != null)
                {
                    int unitCarryNum = unit.CarryTracker.CarriedThing.Count;
                    if (thing is IBuildable build)
                    {
                        unitCarryNum =
                            Mathf.Min(BuildUtility.GetNeedItemCount(build, unit.CarryTracker.CarriedThing.Def),
                                unitCarryNum);
                        if (reversContainerIndex != JobTargetIndex.None)
                        {
                            var reversContainer = curJob.GetTarget(reversContainerIndex).Thing;
                            if (reversContainer != null && reversContainer != thing)
                            {
                                int reversContainerThingNeedCount =
                                    BuildUtility.GetNeedItemCount((IBuildable)reversContainer,
                                        unit.CarryTracker.CarriedThing.Def);
                                unitCarryNum = Mathf.Min(unitCarryNum,
                                    unit.CarryTracker.CarriedThing.Count - reversContainerThingNeedCount);
                            }
                        }
                    }

                    var carriedThing = unit.CarryTracker.CarriedThing;
                    int addNum = unit.CarryTracker.ThingContainer.TryGiveToOtherContainer(carriedThing, thingOwner, unitCarryNum);
                    if (addNum != 0)
                    {
                        //TODO:转移成功,需要发送事件
                    }
                }
                else
                {
                    Debug.LogError("无法将物品搬运到容器中");
                }
            }

        };
        return work;
    }

    public static Work JumpToExtraHaulThingIfPossible(Work goToWork,JobTargetIndex jobTargetIndex) {
        Work extraWork = WorkMaker.MakeWork();
        extraWork.InitAction = () =>
        {
            var unit = extraWork.Unit;
            var curJob = unit.JobTracker.Job;
            var inQueueTarget = curJob.GetTargetList(jobTargetIndex);
            if (!inQueueTarget.IsNullOrEmpty() && curJob.Count > 0)
            {
                if (unit.CarryTracker.CarriedThing == null)
                {
                    Debug.LogError("当前没有拿着东西,按理说走到这里的话手上应该有东西的");
                }
                else if(unit.CarryTracker.GetThingSpaceCountByDef(unit.CarryTracker.CarriedThing.Def) > 0)
                {
                    //TODO:如果可以拿得动队列中的东西,则设置为目标然后走过去拿
                    for (int i = 0; i < inQueueTarget.Count; i++)
                    {
                        if (inQueueTarget[i].Thing.Def.ID == unit.CarryTracker.CarriedThing.Def.ID)
                        {
                            Debug.Log($"还可以继续拿东西，切换当前的目标为index={i}");
                            curJob.SetTarget(jobTargetIndex, inQueueTarget[i]);
                            inQueueTarget.RemoveAt(i);
                            unit.JobTracker.JobDriver.JumpToWork(goToWork);
                        }
                    }
                }
            }
        };

        return extraWork;
    }

    public static Work JumpToExtraHaulToContainerIfPossible(Work carryThingToContainerWork, JobTargetIndex jobTargetIndex)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = () =>
        {
            var unit = work.Unit;
            var curJob = unit.JobTracker.Job;
            var targets = curJob.GetTargetList(jobTargetIndex);
            if (!targets.IsNullOrEmpty() && unit.CarryTracker.CarriedThing is { Count: > 0 })
            {
                //var carriedThingNum = unit.CarryTracker.CarriedThing.Count;
                for (int i = 0; i < targets.Count; i++)
                {
                    var needResources = ((IBuildable)targets[i].Thing).NeedResources();
                    foreach (var needThing in needResources)
                    {
                        if (needThing.Def.ID == unit.CarryTracker.CarriedThing.Def.ID)
                        {
                            curJob.SetTarget(jobTargetIndex, targets[i]);
                            targets.RemoveAt(i);
                            unit.JobTracker.JobDriver.JumpToWork(carryThingToContainerWork);
                            return;
                        }
                    }
                }
            }
        };

        return work;
    }
}