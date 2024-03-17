
public struct ThingRequest {
    //TODO:后期会将Things分类，加快查询的速度，比如建造工作只需要扫描需要被建造的Things就可以了
    public Define_Thing ThingDefine;

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

    public static ThingRequest ForDefine(Define_Thing define)
    {
        ThingRequest result = default(ThingRequest);
        result.ThingDefine = define;
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
}

public enum ThingRequestGroup
{
    Undefined,
    BuildingFrame
}