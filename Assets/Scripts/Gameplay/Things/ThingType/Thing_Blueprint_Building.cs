
using System.Collections.Generic;
using ConfigType;

public class Thing_Blueprint_Building : Blueprint {
    public override float WorkTotal => Def.Workload;
    protected override Thing MakeSolidThing() {
        //第一次在这个蓝图上工作的时候，需要将当前蓝图转换为对应的Frame
        //TODO:后面要搞个对象池来复用物体
        Thing_Building_Frame frame = (Thing_Building_Frame)ThingMaker.MakeNewThing(Def.FrameDef);
        return frame;
    }

    public override IReadOnlyList<DefineThingClassCount> NeedResources() {
        return Def.EntityBuildDef.CostList;
    }

    public override ThingDefine EntityDefineToBuildComplete()
    {
        return Def.EntityBuildDef;
    }
}