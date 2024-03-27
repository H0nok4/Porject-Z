using System.Collections.Generic;

public interface IBuildable {
    IReadOnlyList<DefineThingClassCount> NeedResources();

    ThingBuildableDefine EntityDefineToBuildComplete();
}