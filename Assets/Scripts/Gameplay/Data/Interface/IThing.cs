using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThing
{
    //TODO:���� ���� ��λȫ������Obj
    public bool IsDestoryed { get; set; }

    public ThingType ThingType { get; set; }

}
