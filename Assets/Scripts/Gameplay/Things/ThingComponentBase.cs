using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ThingComponentBase {
    //TODO:Thing可以使用的组件，拥有的不同的组件构成了Thing的功能


    public ThingWithComponent Parent;

    public virtual void PostSpawnSetup(bool reSpawnAfterLoad)
    {

    }

    public virtual void PostDeSpawn(MapData mapData)
    {

    }

    public virtual void PostDestroy(DestroyType type,MapData map)
    {

    }

    public virtual void Tick()
    {

    }

    /// <summary>
    /// 原本觉得只会有物品用到，但是考虑到有的建筑可以储存物品，所以放在基类
    /// </summary>
    /// <param name="thing"></param>
    public virtual void AbsorbStack(Thing thing)
    {

    }
    /// <summary>
    /// 原本觉得只会有物品用到，但是考虑到有的建筑可以储存物品，所以放在基类
    /// </summary>
    /// <param name="thing"></param>
    public virtual void SplitOff(Thing thing)
    {

    }

    public virtual bool AllowAbsorbStack(Thing thing)
    {
        return false;
    }


}