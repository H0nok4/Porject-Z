using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public static class Work_MoveTo {
    public static Work MoveToCell(JobTargetIndex targetIndex, PathMoveEndType endType) {
        Work moveWork = WorkMaker.MakeWork();
        moveWork.InitAction = delegate {
            var pawn = moveWork.Unit;
            Debug.Log($"开始行走，目标点为:{pawn.JobTracker.Job.InfoA.Position}");
            //pawn.PathMover.CurrentMovingPath = new PawnPath() { FindingPath = PathFinder.AStarFindPath(pawn, pawn.JobTracker.Job.InfoA.Section.CreatePathNode()), Using = true }; 
            pawn.PathMover.SetMoveTarget(pawn.JobTracker.Job.GetTarget(targetIndex).Position, endType);
        };
        moveWork.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return moveWork;
    }

    public static Work MoveToThing(JobTargetIndex targetIndex, PathMoveEndType endType)
    {
        Work moveWork = WorkMaker.MakeWork();
        moveWork.InitAction = delegate
        {

            var pawn = moveWork.Unit;
            pawn.PathMover.SetMoveTarget(pawn.JobTracker.Job.GetTarget(targetIndex).Thing, endType);
        };
        moveWork.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return moveWork;
    }

    //public static Work MoveToThingNotStand(JobTargetIndex targetIndex, PathMoveEndType endType) {
    //    Work moveWork = WorkMaker.MakeWork();
    //    moveWork.InitAction = delegate {
    //        var unit = moveWork.Unit;
    //        var job = unit.JobTracker.Job.GetTarget(targetIndex);


    //    };
    //}

    public static Work MoveOffToThing(JobTargetIndex jobTargetIndex)
    {
        Work moveOffWork = WorkMaker.MakeWork();
        moveOffWork.InitAction = delegate
        {
            var unit = moveOffWork.Unit;
            var thing = unit.JobTracker.Job.GetTarget(JobTargetIndex.A).Thing as Blueprint;
            if (thing == null || !unit.IsInside(thing))
            {
                unit.JobTracker.JobDriver.CanStartNextWork();
            }
            else if (CellFindUtility.TryFindPositionToTouch(thing,out var result))
            {
                //碰撞了，需要找到一个位置
                unit.PathMover.StartPath(new PawnPath(PathFinder.AStarFindPath(unit,result,PathMoveEndType.InCell)));
            }
            else
            {
                //没位置，直接结束工作
                unit.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
            }
        };
        moveOffWork.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return moveOffWork;
    }
}