using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing_Item : ThingWithComponent
{
    //物品也分很多种,单纯的材料,装备,武器,道具.
    //道具分使用时选择目标,使用时自动选择自己,被动使用的类似医疗物资等.
    public bool Ingestible
    {
        get
        {
            return Def.IngestibleID != 0;
        }
    }

    public override void SpawnSetup(MapData mapData) {
        base.SpawnSetup(mapData);

        var com = GameObject.GO.AddComponent<DrawItemNum>();
        com.Init(this);
    }
}
