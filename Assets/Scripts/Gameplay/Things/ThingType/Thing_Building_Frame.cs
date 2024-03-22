

/// <summary>
/// 所有的建筑的框架都是一类的，只是有范围大小的区别
/// </summary>
public class Thing_Building_Frame : Thing_Building {
    public int WorkToBuild = 10;

    public float CurrentWorkCount = 0;

    public override void SpawnSetup(MapData mapData) {
        base.SpawnSetup(mapData);
        //TODO:将当前的贴图设置为def配置的Frame贴图
        WorkToBuild = Def.Workload;
        GameObject.SetSprite(Def.FrameSprite);
    }

    public void CompleteBuild(Thing_Unit unit) {
        //TODO:需要设置建筑的拥有派系
        SpawnHelper.Spawn(this.Def, this.Position);
        DeSpawn();
    }
}