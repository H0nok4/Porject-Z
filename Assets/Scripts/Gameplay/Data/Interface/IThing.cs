using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThing
{
    //TODO:建筑 物体 单位全部都是Obj
    public GameObject GameObject { get; set; }
    public IntVec2 Position { get; set; }
    public bool IsDestoryed { get; set; }
    public ThingType ThingType { get; set; }
    public MapData MapData { get; set; }

    public void Tick();

}
