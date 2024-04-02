using System.Collections.Generic;
using ConfigType;

public interface IBuildable {
    IReadOnlyList<DefineThingClassCount> NeedResources();

    ThingDefine EntityDefineToBuildComplete();
}