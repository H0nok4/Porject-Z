using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Define_Thing : Define_Buildable
{
    public EditableType ThingClass;

    public ThingCategory Category;

    public bool Destroyable = true;

    public bool UseHitPoint;

    public int StackLimit = 1;

    public ItemProperties ItemProp;

    public bool IsItem => ItemProp != null;
    public bool MadeFromItem => ItemProp != null;
    //是否能够被搬运
    public bool EverHaulable;


    //TODO:临时，后面需要根据可以胜任的工作类型获得各个WorkGiver
    [NonSerialized]
    public List<WorkGiverDefine> WorkGiverDefineType;

    public bool IsFrame;

    public bool IsBlueprint;

    private Define_Thing _blueprintDefInstance;

    public Define_Thing BlueprintDef {
        get {
            if (_blueprintDefInstance == null)
                ThingUtility.CreateBlueprintDefToThingDef(this);

            return _blueprintDefInstance;
        }
        set => _blueprintDefInstance = value;
    }

    private Define_Thing _frameDefInstance;
    public Define_Thing FrameDef {
        get {
            if (_frameDefInstance == null)
            {
                ThingUtility.CreateFrameDefToThingDef(this);
            }


            return _frameDefInstance;
        }
        set => _frameDefInstance = value;
    }


}