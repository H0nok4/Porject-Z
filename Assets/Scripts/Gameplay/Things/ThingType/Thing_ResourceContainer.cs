using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Thing_ResourceContainer : ThingWithComponent
{
    //TODO:类似塔科夫的容器,打开的时候如果没有初始化过,就初始化一遍物体,然后每个物体都需要一定的时间来搜索刷新出来,没刷新的物体无法互动
    public bool Inited;
    
    public bool[] Generated;

    public ThingOwner<Thing> Container;

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

        for (int i = 0; i < Generated.Length; i++)
        {
            if (!Generated[i])
            {
                yield break;
            }

            yield return Container.GetAt(i);
        }
    }

    public void Init()
    {
        Container = new ThingOwner<Thing>();
        //TODO:需要一个随机物品生成器,根据配置在奖池中随机生成一些物品.

    }
}