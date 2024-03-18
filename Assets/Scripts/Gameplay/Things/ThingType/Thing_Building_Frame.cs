using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : Thing_Building
{
    public int WorkToBuild = 10;

    public float CurrentWorkCount = 0;

    public override void SpawnSetup(MapData mapData)
    {
        base.SpawnSetup(mapData);
        //TODO:����ǰ����ͼ����Ϊdef���õ�Frame��ͼ
        WorkToBuild = Def.Workload;
        GameObject.SetSprite(Def.FrameSprite);
    }

    public void CompleteBuild(Thing_Unit unit)
    {
        SpawnHelper.Spawn(DataTableManager.Instance.ThingDefineHandler.WallInstance, this.Position, this.MapData.Index);
        DeSpawn();
    }
}
