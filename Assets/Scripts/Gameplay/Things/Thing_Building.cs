using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Thing_Building : ThingWithComponent
{
    //TODO:建筑也分类型，比如普通的墙壁不需要频繁更新，但是像炮台因为需要自动寻找敌人所以需要加到Tick列表中更新
    public override int HP
    {
        set
        {
            int currentHP = HP;
            base.HP = value;
            //TODO:建筑血量改变事件
        }
    }

    //TODO:建筑分不需要电，需要电，产电的，通过携带的组件来分
    //public ThingComponent_Power PowerComponent => GetComponent<ThingComponent_Power>();


}
