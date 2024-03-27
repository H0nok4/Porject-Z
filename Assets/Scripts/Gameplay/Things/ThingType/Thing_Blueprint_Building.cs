
using System.Collections.Generic;

public class Thing_Blueprint_Building : Blueprint {
    public override float WorkTotal => Def.Workload;
    protected override Thing MakeSolidThing() {
        //TODO:第一次在这个蓝图上工作的时候，需要将当前蓝图转换为对应的Frame
        Thing_Building_Frame frame = (Thing_Building_Frame)ThingMaker.MakeNewThing(Def.FrameDef);
        return frame;
    }

    public override IReadOnlyList<DefineThingClassCount> NeedResources() {
        return Def.EntityBuildDef.CostList;
    }

    public override ThingBuildableDefine EntityDefineToBuildComplete()
    {
        return Def.EntityBuildDef;
    }
}