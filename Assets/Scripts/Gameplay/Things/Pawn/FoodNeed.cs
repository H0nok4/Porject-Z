using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FoodNeed : Need{

    public HungryStageType HungryStage {
        get {
            if (CurValue == 0) {
                return HungryStageType.Starvation;
            }

            if (CurValuePercent < 0.2f) {
                return HungryStageType.WantFood;
            }

            if (CurValuePercent < 0.9f) {
                return HungryStageType.Nevermind;
            }

            return HungryStageType.Full;
        }
    }
    
    //TODO:需求不需要每Tick都更新,需要在单位中增加一个多少Tick更新一次的方法
    public override void Tick() {
        //TODO:饱食度会随时间降低
        //Rimworld中60000Tick为游戏内1天,目前先假设1天就会把饱食度掉到0
        CurValue -= 1 / 60000f;

        //TODO:当掉到0的时候,进入营养不良的状态
    }
}