using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单层的地图数据
/// </summary>
public class MapData {
    public Section[,] Sections;

    public int Size => Sections.GetLength(0) * Sections.GetLength(1);

    public List<IThing> HandleThings;

    public IEnumerable<Section> EnumerableSections {
        get {
            for (int i = 0; i < Sections.GetLength(0); i++) {
                for (int j = 0; j < Sections.GetLength(1); j++) {
                    yield return Sections[i, j];
                }
            }
        }
    }


    //TODO:Thing应该有一个自己的管理器，能够快速查到某样物品是否存在和所在位置
    public void RegisterThing(IThing thing)
    {
        //TODO:添加并且做一些操作，比如更新ThingGrid
        HandleThings.Add(thing);
    }

    public void UnRegisterThing(IThing thing)
    {
        if (!HandleThings.Contains(thing))
        {

            return;
        }
        //TODO:删除并且做一些操作
        HandleThings.Remove(thing);
    }

    public Section IndexOf(int x, int y) => Sections[x, y];

    public Section IndexOf(IntVec2 pos) => Sections[pos.X, pos.Y];

    public MapData() {

    }
}