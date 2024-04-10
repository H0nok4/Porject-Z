using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public class JobDriver_HaulToContainer : JobDriver
{
    public override IEnumerable<Work> MakeWorks()
    {
        Work goToThingWork = Work_MoveTo.MoveToThing(JobTargetIndex.A, PathMoveEndType.Touch);
        Work haulThingWork = Work_Haul.StartCarryThing(JobTargetIndex.A);
        Work addedExtraHaulThingWork = Work_Haul.JumpToExtraHaulThingIfPossible(goToThingWork,JobTargetIndex.A);
        Work carryThingToContainerWork = Work_Haul.CarryThingToContainer();
        yield return goToThingWork;
        yield return haulThingWork;
        yield return addedExtraHaulThingWork;
        yield return carryThingToContainerWork;
        //TODO:放进容器中需要时间，所以可以有一个等待的时间

        yield return Work_Build.BuildBlueprintToFrameIfNeed(JobTargetIndex.B,JobTargetIndex.C);
        //TODO:把手上的东西放进去
        yield return Work_Haul.PutHauledThingIntoContainer(JobTargetIndex.B, JobTargetIndex.C);
        //TODO:之后做成可以按顺序放入多个物体的时候，需要找到下一个放入的目标
        yield return Work_Haul.JumpToExtraHaulToContainerIfPossible(carryThingToContainerWork, JobTargetIndex.B);
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed)
    {
        if (!Unit.Reserve(Job.GetTarget(JobTargetIndex.A),Job))
        {
            return false;
        }

        if (!Unit.Reserve(Job.GetTarget(JobTargetIndex.B),Job))
        {
            return false;
        }
        //TODO:后面做一次性拿多个建筑物的材料时，需要把队列中的物体都加进来
        Unit.ReserveAsManyAsPossible(Job.InfoListA, Job);
        return true;
    }
}
