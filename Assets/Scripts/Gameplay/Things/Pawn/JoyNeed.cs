using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JoyNeed : Need{

    /// <summary>
    /// 不同阶段对应不同的心情加成
    /// </summary>
    public JoyStageType JoyStage
    {
        get
        {
            if (CurValue <= 0f)
            {
                return JoyStageType.UnHappy;
            }

            if (CurValue <= 0.2f)
            {
                return JoyStageType.NeedJoy;
            }

            if (CurValue <= 0.4f)
            {
                return JoyStageType.Boring;
            }

            if (CurValue <= 0.6f)
            {
                return JoyStageType.Nevermind;
            }

            if (CurValue <= 0.8f)
            {
                return JoyStageType.Happy;
            }

            return JoyStageType.VeryHappy;
        }
    }
    public override void Tick(int interval)
    {
        CurValue -= (1 / 60000f) * interval;

        TryChangeMood();
    }

    public void TryChangeMood()
    {
        //根据当前的心情值变换心情加成
    }
}