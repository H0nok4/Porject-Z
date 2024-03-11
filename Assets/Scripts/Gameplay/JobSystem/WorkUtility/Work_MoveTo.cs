using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Work_MoveTo {
    public static Work MoveToCell(JobTargetIndex targetIndex, PathMoveEndType endType) {
        Work moveWork = WorkMaker.MakeWork();
        moveWork.InitAction = delegate {
            var pawn = moveWork.Unit;
            pawn.PathMover.CurrentMovingPath = new PawnPath() { FindingPath = PathFinder.AStarFindPath(pawn, pawn.JobTracker.Job.InfoB.Section.CreatePathNode()), Using = true }; 
        };
        moveWork.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return moveWork;
    }
}