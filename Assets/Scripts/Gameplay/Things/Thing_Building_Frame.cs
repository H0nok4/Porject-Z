using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : Thing_Building
{
    public int WorkToBuild = 10;

    public float CurrentWorkCount;

    public Frame(Define_Thing def,ThingObject gameObject, MapData mapData, IntVec2 position) : base(def, gameObject, mapData, position)
    {
        //TODO:����ǰ����ͼ����Ϊdef���õ�Frame��ͼ
        GameObject.SetSprite(def.FrameSprite);
    }

    public void CompleteBuild(Thing_Unit unit)
    {
        
    }
}
