using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Thing_Building : ThingWithComponent
{
    //TODO:����Ҳ�����ͣ�������ͨ��ǽ�ڲ���ҪƵ�����£���������̨��Ϊ��Ҫ�Զ�Ѱ�ҵ���������Ҫ�ӵ�Tick�б��и���
    public override int HP
    {
        set
        {
            int currentHP = HP;
            base.HP = value;
            //TODO:����Ѫ���ı��¼�
        }
    }

    public Thing_Building(GameObject gameObject, MapData mapData, IntVec2 position) : base(gameObject, mapData, position)
    {
        ThingType = ThingCategory.Building;

        //TODO:��������ˢ��Sprite
    }

    //TODO:�����ֲ���Ҫ�磬��Ҫ�磬����ģ�ͨ��Я�����������
    //public ThingComponent_Power PowerComponent => GetComponent<ThingComponent_Power>();


}
