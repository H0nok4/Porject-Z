using System.Collections.Generic;

public class Section
{
    public IntVec2 Position;

    public MapData ParentMap;

    public int MapIndex;

    public SectionType SectionType;
    //TODO:首先，分可以行走和不可行走
    public bool Walkable;
    //格子上可能有建筑或者掉落的道具
    public List<IThing> HandleThings = new List<IThing>();

    //TODO:后面可能得按类型分

    public void UnRegisterThing(IThing thing)
    {
        if (HandleThings.Contains(thing))
        {
            HandleThings.Remove(thing);
            ParentMap.UnRegisterThing(thing);
        }
    }

    public void RegisterThing(IThing thing)
    {
        if (HandleThings.Contains(thing))
        {
            return;
        }

        HandleThings.Add(thing);
        ParentMap.RegisterThing(thing);
    }

    public void Init()
    {

    }

    public PathNode CreatePathNode()
    {
        return new PathNode() { Pos = Position.Copy(), MapDataIndex = MapIndex };
    }
}