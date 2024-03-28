using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RestNeed : Need
{

    public RestStageType RestStage
    {
        get
        {
            if (CurValue <= 0)
            {
                return RestStageType.Exhausted;
            }

            if (CurValue <= 0.2f)
            {
                return RestStageType.NeedRest;
            }

            if (CurValue <= 0.9f)
            {
                return RestStageType.Nevermind;
            }

            return RestStageType.Energetic;
        }
    }

    public override void Tick(int interval)
    {
        CurValue -= (1 / 60000f) * interval;

        if (RestStage == RestStageType.Exhausted)
        {
            ForceSleep();
        }
    }

    public void ForceSleep()
    {
        //当休息值降低到0的时候，会强制触发睡眠
        Debug.LogWarning("未实现-强制睡眠");
    }
}