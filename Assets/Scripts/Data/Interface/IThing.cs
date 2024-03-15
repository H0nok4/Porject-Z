using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThing
{
    //TODO:建筑 物体 单位全部都是Obj
    public ThingObject GameObject { get; set; }
    public IntVec2 Position { get; set; }
    public bool IsDestroyed { get; set; }
    public ThingCategory ThingType { get; set; }
    public MapData MapData { get; set; }

    public void Tick();

}
