using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing_Item : ThingWithComponent
{
    //��ƷҲ�ֺܶ���,�����Ĳ���,װ��,����,����.
    //���߷�ʹ��ʱѡ��Ŀ��,ʹ��ʱ�Զ�ѡ���Լ�,����ʹ�õ�����ҽ�����ʵ�.
    public bool Ingestible
    {
        get
        {
            return Def.IngestibleID != 0;
        }
    }
}
