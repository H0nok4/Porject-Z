using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Work_MoveTo {
    public static Work MoveToCell(JobTargetIndex targetIndex, PathMoveEndType endType) {
        Work moveWork = WorkMaker.MakeWork();
        moveWork.InitAction = delegate {
            var pawn = moveWork.Unit;
            Debug.Log($"开始行走，目标点为:{pawn.JobTracker.Job.InfoA.Section.Position}");
            pawn.PathMover.CurrentMovingPath = new PawnPath() { FindingPath = PathFinder.AStarFindPath(pawn, pawn.JobTracker.Job.InfoA.Section.CreatePathNode()), Using = true }; 
        };
        moveWork.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return moveWork;
    }
}