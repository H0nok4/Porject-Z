using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : Thing_Building
{
    public int WorkToBuild = 10;


    public Frame(Define_Thing def,ThingObject gameObject, MapData mapData, IntVec2 position) : base(def, gameObject, mapData, position)
    {
        //TODO:将当前的贴图设置为def配置的Frame贴图
        GameObject.SetSprite(def.FrameSprite);
    }
}
