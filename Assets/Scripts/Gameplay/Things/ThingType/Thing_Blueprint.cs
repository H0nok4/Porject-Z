using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͼ�ͽ���ƽ��
/// </summary>
public abstract class Blueprint : ThingWithComponent   
{
    /// <summary>
    /// ��ͼ�׶ξͿ���ֱ�ӿ����ܵĹ�����
    /// </summary>
    public abstract float WorkTotal { get; }

    public virtual bool TryReplaceWithSolidThing(Thing_Unit_Pawn worker, out Thing createdThing, out bool jobEnd)
    {
        if (BuildUtility.FirstBlockingThing(this,worker) != null)
        {
            //TODO:�ж����赲�����ܽ���
            worker.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
            createdThing = null;
            jobEnd = true;
            return false;
        }

        createdThing = MakeSolidThing(out var shouldSelect);
        SpawnHelper.WipeExistingThings(Position,Rotation,createdThing.Def,DestroyType.Deconstruct);
        //TODO:������Ҫ���������Ϲ���С�˵���Ӫ
        Thing spawnedThing = SpawnHelper.Spawn(createdThing, Position);
        return true;
    }

    protected abstract Thing MakeSolidThing(out bool shouldSelect);

    public abstract List<DefineThingClassCount> NeedResources();


}
