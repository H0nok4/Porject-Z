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
    public List<Thing> HandleThings = new List<Thing>();

    //TODO:后面可能得按类型分

    public void UnRegisterThing(Thing thing)
    {
        if (HandleThings.Contains(thing))
        {
            HandleThings.Remove(thing);
            ParentMap.UnRegisterThingHandle(thing);
        }
    }

    public void RegisterThing(Thing thing)
    {
        if (HandleThings.Contains(thing))
        {
            return;
        }

        HandleThings.Add(thing);
        ParentMap.RegisterThingHandle(thing);
    }

    public void Init()
    {

    }

    private PosNode PosNodeInstance;

    public PosNode CreatePathNode(bool findPath = true)
    {
        if (findPath) {
            return new PosNode() { Pos = Position.Copy(), MapDataIndex = MapIndex };
        }

        if (PosNodeInstance == null) {
            PosNodeInstance = new PosNode() {Pos = Position.Copy(), MapDataIndex = MapIndex};
        }

        return PosNodeInstance;
    }
}