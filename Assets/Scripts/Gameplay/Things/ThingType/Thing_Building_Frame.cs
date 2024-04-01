

using System.Collections.Generic;
using ConfigType;

/// <summary>
/// 所有的建筑的框架都是一类的，只是有范围大小的区别
/// </summary>
public class Thing_Building_Frame : Thing_Building, IBuildable, IThingHolder {
    public ThingOwner ResourcesContainer;

    public int WorkToBuild = 10;

    public float CurrentWorkCount = 0;

    public Thing_Building_Frame()
    {
        ResourcesContainer = new ThingOwner<Thing>(this);
    }

    public override void SpawnSetup(MapData mapData) {
        base.SpawnSetup(mapData);
        //TODO:将当前的贴图设置为def配置的Frame贴图
        WorkToBuild = EntityDefineToBuildComplete().Workload;
        GameObject.SetSprite(Def.FrameSprite);
    }

    public void CompleteBuild(Thing_Unit unit) {
        //TODO:需要设置建筑的拥有派系
        SpawnHelper.Spawn(this.EntityDefineToBuildComplete(), this.Position);
        //DeSpawn();
        Destroy();
    }

    public IReadOnlyList<DefineThingClassCount> NeedResources()
    {
        //TODO:目前还没有物品设定，后续在做
        return new List<DefineThingClassCount>();
    }

    public ThingDefine EntityDefineToBuildComplete()
    {
        return Def.EntityBuildDef;
    }

    public void GetChildren(List<IThingHolder> outChildren)
    {
        throw new System.NotImplementedException();
    }

    public ThingOwner GetHoldingThing()
    {
        return ResourcesContainer;
    }
}