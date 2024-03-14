using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 单层的地图数据
/// </summary>
public class MapData
{
    public int Index;

    public Tilemap TileMapObject;

    public MapDataHandleThingGameObjectManager ThingObjectManager;

    public Section[] Sections;

    public int Size => Width * Height;

    public int Width { get; set; }

    public int Height { get; set; }

    public ThingMapManager ThingMap;

    public HashSet<IThing> HandleThings = new HashSet<IThing>();

    public Section GetSectionByPos(int x, int y) {
        return Sections[y * Width + x];
    }

    public Section GetSectionByPos(IntVec2 pos)
    {
        return Sections[pos.Y * Width + pos.X];
    }

    public Section GetSectionByIndex(int index) {
        return Sections[index];
    }


    public void RegisterThing(IThing thing) {
        if (HandleThings.Contains(thing))
        {
            return;
        }

        HandleThings.Add(thing);
        ThingObjectManager.Register(thing.GameObject);
    }

    public void RegisterThingMapPos(Thing thing)
    {
        //Thing应该有一个自己的管理器，能够快速查到某样物品是否存在和所在位置
        ThingMap.RegisterThing(thing);
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

    public void UnRegisterThing(IThing thing)
    {
        if (!HandleThings.Contains(thing))
        {
            return;
        }
        //TODO:删除并且做一些操作
        HandleThings.Remove(thing);
    }

    public void UnRegisterThingMapPos(Thing thing)
    {
        ThingMap.UnRegisterThing(thing);
    }

    public MapData(int index,GameObject thingHandleGameObject,int width,int height)
    {
        Index = index;
        Width = width;
        Height = height;
        ThingObjectManager = new MapDataHandleThingGameObjectManager(thingHandleGameObject);
        ThingMap = new ThingMapManager(this);

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

    public int PosToIndex(IntVec2 pos) {
        return (Width * pos.Y) + pos.X;
    }
}

