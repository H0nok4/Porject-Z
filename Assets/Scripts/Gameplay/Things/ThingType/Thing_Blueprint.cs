using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

/// <summary>
/// 蓝图和建筑平级
/// </summary>
public abstract class Blueprint : ThingWithComponent ,IBuildable {
    /// <summary>
    /// 蓝图阶段就可以直接看到总的工作量
    /// </summary>
    public abstract float WorkTotal { get; }

    public virtual bool TryReplaceWithSolidThing(Thing_Unit_Pawn worker, out Thing createdThing, out bool jobEnd) {
        jobEnd = false;
        if (BuildUtility.FirstBlockingThing(this,worker) != null)
        {
            //TODO:有东西阻挡，不能建造
            worker.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
            createdThing = null;
            jobEnd = true;
            return false;
        }

        createdThing = MakeSolidThing();
        SpawnHelper.WipeExistingThings(Position,Rotation,createdThing.Def,DestroyType.Deconstruct);
        //TODO:后面需要给建筑附上工作小人的阵营
        Thing spawnedThing = SpawnHelper.Spawn(createdThing, Position,createdThing.Rotation);
        if (spawnedThing != null) {
            //TODO:如果选择了蓝图,在蓝图建造好的时候可以直接在选中建造好的建筑
        }

        Destroy();
        return true;
    }

    protected abstract Thing MakeSolidThing();

    public abstract IReadOnlyList<DefineThingClassCount> NeedResources();

    public abstract ThingDefine EntityDefineToBuildComplete();

    public override void SpawnSetup(MapData mapData)
    {
        base.SpawnSetup(mapData);
    }
}
