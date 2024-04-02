
using System;
using ConfigType;

public struct ThingRequest {
    //TODO:后期会将Things分类，加快查询的速度，比如建造工作只需要扫描需要被建造的Things就可以了
    public ThingDefine ThingDefine;

    public ThingRequestGroup Group;

    public bool IsUndefined
    {
        get
        {
            if (ThingDefine == null)
            {
                return Group == ThingRequestGroup.Undefined;
            }

            return false;
        }
    }

    public static ThingRequest ForDefine(ThingDefine thingDefine)
    {
        ThingRequest result = default(ThingRequest);
        result.ThingDefine = thingDefine;
        result.Group = ThingRequestGroup.Undefined;
        return result;
    }

    public static ThingRequest ForGroup(ThingRequestGroup group)
    {
        ThingRequest result = default(ThingRequest);
        result.ThingDefine = null;
        result.Group = group;
        return result;
    }

    public override string ToString()
    {
        return $"Define = {ThingDefine},RequestGroup = {Group}";
    }
}

public static class ThingRequestGroupHelper
{
    public static readonly ThingRequestGroup[] AllGroups;

    static ThingRequestGroupHelper()
    {
        var values = Enum.GetValues(typeof(ThingRequestGroup));
        AllGroups = new ThingRequestGroup[values.Length];
        int index = 0;
        foreach (object value in values) {
            AllGroups[index] = (ThingRequestGroup)value;
            index++;
        }
    }

    public static bool Contains(this ThingRequestGroup group, ThingDefine thingDefine)
    {
        switch (group)
        {
            case ThingRequestGroup.Undefined:
                return false;
            case ThingRequestGroup.All:
                return true;
            case ThingRequestGroup.BuildingFrame:
                return thingDefine.IsFrame;
            case ThingRequestGroup.BuildingBlueprint:
                return thingDefine.IsBlueprint;
            case ThingRequestGroup.Item:
                return thingDefine.Category == ThingCategory.Item;
            default:
                return false;
        }
    } 
}

public enum ThingRequestGroup
{
    Undefined = 0,
    All = 1,
    BuildingFrame,
    BuildingBlueprint,
    Item
}