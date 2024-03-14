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

    public Thing_Building(Define_Thing buildDef,ThingObject gameObject, MapData mapData, IntVec2 position) : base(gameObject, mapData, position)
    {
        ThingType = ThingCategory.Building;
        Def = buildDef;
        //TODO:根据配置刷新Sprite
    }

    //TODO:建筑分不需要电，需要电，产电的，通过携带的组件来分
    //public ThingComponent_Power PowerComponent => GetComponent<ThingComponent_Power>();


}
