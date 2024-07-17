using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class Thing_GenResourceContainer : ThingWithComponent
{
    //TODO:类似塔科夫的容器,打开的时候如果没有初始化过,就初始化一遍物体,然后每个物体都需要一定的时间来搜索刷新出来,没刷新的物体无法互动
    public bool Inited;
    
    public bool[] Showed;

    public ThingOwner<Thing> Container;

    public int UseJackpotDefineID;

    public IntVec2 ItemNumRange;
 
    public int ThingCount
    {
        get
        {
            if (Container == null)
            {
                return 0;
            }

            return Container.Count;
        }
    }

    public IEnumerable<Thing> GetInteractableThing()
    {
        if (Container == null)
        {
            yield break;
        }

        for (int i = 0; i < Showed.Length; i++)
        {
            if (!Showed[i])
            {
                yield break;
            }

            yield return Container.GetAt(i);
        }
    }

    public void Init()
    {
        if (Inited)
        {
            return;
        }
        Container = new ThingOwner<Thing>();
        var def = DataManager.Instance.GetJackpotDefineByID(UseJackpotDefineID);
        ItemNumRange = new IntVec2(def.MinItemCount, def.MaxItemCount);
        //TODO:需要一个随机物品生成器,根据配置在奖池中随机生成一些物品.
        var items = RandomThingGenerator.Instance.GenerateThingByJackpotID(UseJackpotDefineID,
            UnityEngine.Random.Range(ItemNumRange.X, ItemNumRange.Y));
        foreach (var thing in items)
        {
            Logger.Instance.Log($"容器中生成了一个ID为:{thing.Def.ID}的物品,名称为:{thing.Def.Name},数量为:{thing.Count}");
            Container.TryAdd(thing);
        }

        Showed = new bool[Container.Count];
        Inited = true;
    }
}