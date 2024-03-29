using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class ThirstyNeed : Need{

    public ThirstyStageType ThirstyStage
    {
        get
        {
            if (CurValue <= 0)
            {
                return ThirstyStageType.Dry;
            }

            if (CurValue <= 0.2f)
            {
                return ThirstyStageType.NeedDrink;
            }

            if (CurValue <= 0.9f)
            {
                return ThirstyStageType.Nevermind;
            }

            return ThirstyStageType.Full;
        }
    }
    public override void Tick(int interval)
    {
        CurValue -= (1 / 60000f) * interval;

        //降低到干燥后，需要添加口渴的Debuff
    }
}