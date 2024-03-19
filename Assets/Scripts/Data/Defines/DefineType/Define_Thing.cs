using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New ThingDefine", menuName = "Define/Create ThingDefine")]
public class Define_Thing : Define_Buildable
{
    public EditableType ThingClass;

    public ThingCategory Category;

    public bool Destroyable = true;

    public bool Rotatable = true;

    public bool UseHitPoint;

    public int StackLimit = 1;

    public Sprite ThingSprite;

    public Sprite FrameSprite;

    public ItemProperties ItemProp;

    public bool IsItem => ItemProp != null;
    public bool MadeFromItem => ItemProp != null;

    //TODO:临时，后面需要根据可以胜任的工作类型获得各个WorkGiver
    [NonSerialized]
    public List<WorkGiverDefine> WorkGiverDefineType;

    public bool IsFrame;

}