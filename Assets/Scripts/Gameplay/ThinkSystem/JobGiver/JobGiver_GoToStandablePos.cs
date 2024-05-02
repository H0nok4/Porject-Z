using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

namespace ThinkSystem {
    public class JobGiver_GoToStandablePos : ThinkNode_JobGiver {
        public override Job TryGiveJob(Thing_Unit unit)
        {
            //TODO:检查当前站的位置是不是CanStand,不如不是的话,找到最近的一个CanStand位置
            if (unit.PathMover.IsMoving)
            {
                //已经在移动中了,不用找位置站
                return null;
            }

            if (!unit.Position.Standable())
            {
                //TODO:找到最近的地方站
                return FindStandablePos(unit.Position);
            }

            return null;
        }

        public Job FindStandablePos(PosNode startPos) {
            //TODO:通过迪杰斯特拉算法找到最近的一个可以站的点,然后移动过去
            var pos = MapUtility.GetFirstStandablePosByPosNode(startPos);

            return JobMaker.MakeJob(DataManager.Instance.GetJobDefineByID(3),pos);
        }
    }
}
