using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkGiver
{
    public WorkGiverDefine Def;

    public virtual bool ShouldSkip(Thing_Unit unit, bool forced = false)
    {
        return false;
    }

    public virtual Job NonScanJob(Thing_Unit unit)
    {
        return null;
    }

    //TODO:���浥λ�����Ʋ�������ĳЩ����
}
