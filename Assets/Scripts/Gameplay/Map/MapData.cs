using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 单层的地图数据
/// </summary>
public class MapData
{
    public Tilemap TileMapObject;

    public MapDataHandleThingGameObjectManager ThingObjectManager;

    public Section[] Sections;

    public int Size => Width * Height;

    public int Width { get; set; }

    public int Height { get; set; }

    public List<IThing> HandleThings = new List<IThing>();


    public Section GetSectionByPos(int x, int y) {
        return Sections[y * Width + x];
    }

    public Section GetSectionByIndex(int index) {
        return Sections[index];
    }

    public void RegisterThing(IThing thing) {
        
    }

    public void SetSection(Section data,int x,int y) {
        Sections[y * Width + x] = data;
    }

    public bool Visible
    {
        get => TileMapObject != null && TileMapObject.isActiveAndEnabled;
        set
        {
            if (TileMapObject == null)
            {
                return;   
            }

            TileMapObject.gameObject.SetActive(value);
        }
    }

    //TODO:Thing应该有一个自己的管理器，能够快速查到某样物品是否存在和所在位置
    public void RegisterThing(IThing thing,IntVec2 position)
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

    public MapData() {

    }

    public Section GetSectionByPosition(IntVec2 targetPos)
    {
        if (targetPos.X < 0 || targetPos.X >= Width || targetPos.Y < 0 || targetPos.Y >= Height)
        {
            return null;
        }

        try {
            return GetSectionByPos(targetPos.X, targetPos.Y);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}

