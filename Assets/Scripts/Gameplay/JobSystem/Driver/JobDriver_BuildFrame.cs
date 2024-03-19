using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDriver_BuildFrame : JobDriver {
    private Thing_Building_WallFrame ThingBuildingWallFrameBuilding => (Thing_Building_WallFrame)Unit.JobTracker.Job.GetTarget(JobTargetIndex.A).Thing;
    public override IEnumerable<Work> MakeWorks() {
        //TODO:建造一个未完成的建筑,首先得先走到目标位置
        var moveTo = Work_MoveTo.MoveToThing(JobTargetIndex.A, PathMoveEndType.Touch);
        //TODO:需要考虑有时候是会移动的Thing,所以需要修改寻路类能够设置一个目标点后自动开始寻路并且在寻路过程中能够去获取最新的位置和路径
        yield return moveTo;
        //TODO:然后不断减少建筑的剩余工作量
        var work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Debug.Log("开始建造建筑");
        };
        work.TickAction = delegate
        {
            var unit = work.Unit;
            var frame = ThingBuildingWallFrameBuilding;
            float buildWorkCount = 1;//TODO:后面跟单位的建造速度有关
            //TODO:建造建筑涨技能经验

            //TODO:玩家可能会建造失败，随着建造完成度提高，失败的概率降低

            frame.CurrentWorkCount += buildWorkCount;

            if (frame.CurrentWorkCount >= frame.WorkToBuild)
            {
                //TODO：成功建造完成，在完成后,将Frame替换成实际建筑
                Debug.LogError("成功建造建筑");
                frame.CompleteBuild(unit);
                CanStartNextWork();
            }
        };
        work.CompleteMode = WorkCompleteMode.Delay;//建造默认就是站那边等着，然后给一个长的时间作为跳出条件
        work.NeedWaitingTick = 6000;//TODO:等这么久都还没造完，说明有问题

        yield return work;
    }

    public override bool TryMakeWorkReservations(bool errorOnFailed) {
        ReservationManager.Instance.Reserve(Unit, Job, Job.GetTarget(JobTargetIndex.A));
        return true;
    }
}
