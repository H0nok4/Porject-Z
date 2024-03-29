using System.Collections.Generic;

public interface IBuildable {
    IReadOnlyList<DefineThingClassCount> NeedResources();

    ThingDefine EntityDefineToBuildComplete();
}