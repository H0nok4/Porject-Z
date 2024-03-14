using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ThingMapManager {
    //如果每一种建筑层级都为独立的,会导致有大量的空间浪费,而且目标还是做多层地图,更是重量级
    //所以像环世界一样选择将所有的建筑层级放在一层,然后每一格可以装多个Thing,这样子就可以减少浪费
    private List<Thing>[] _thingMap;

    private MapData _map;

    public ThingMapManager(MapData map) {
        _map = map;
        _thingMap = new List<Thing>[map.Size];
        for (int i = 0; i < map.Size; i++) {
            _thingMap = new List<Thing>[2];//注册一个较小的初始大小
        }
    }

    public void RegisterThing(Thing thing) {
        //TODO:thing的大小可能不止1格,如果为多格的话,需要每一格都注册到对应的位置
        if (thing.Size.X == 1 && thing.Size.Y == 1) {
            //TODO:注册到位置
            RegisterThingAtCell(thing, thing.Position);
        }
    }

    public void UnRegisterThing(Thing thing) {
        if (thing.Size.X == 1 && thing.Size.Y == 1) {
            UnRegisterThingAtCell(thing,thing.Position);
        }
    }

    public void RegisterThingAtCell(Thing thing, IntVec2 pos) {
        //TODO:位置必须在地图上

        _thingMap[_map.PosToIndex(pos)].Add(thing);
    }

    public void UnRegisterThingAtCell(Thing thing, IntVec2 pos) {
        int index = _map.PosToIndex(pos);
        if (_thingMap[index].Contains(thing)) {
            _thingMap[index].Remove(thing);
        }
    }

    public IEnumerable<Thing> ThingsAt(IntVec2 pos) {
        foreach (var thing in _thingMap[_map.PosToIndex(pos)]) {
            yield return thing;
        }
    }

    public List<Thing> ThingsListAt(IntVec2 pos) {

        return _thingMap[_map.PosToIndex(pos)];
    }



}