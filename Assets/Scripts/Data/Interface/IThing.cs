using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThing
{
    //TODO:建筑 物体 单位全部都是Obj
    public ThingObject GameObject { get; set; }
    public PosNode Position { get;}
    public bool IsDestroyed { get; set; }
    public ThingCategory ThingType { get; set; }
    public MapData MapData { get; set; }

    public void Tick();

    public void SpawnSetup(MapData mapData);

    public void DeSpawn();

}
