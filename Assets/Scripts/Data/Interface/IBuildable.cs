using System.Collections.Generic;

public interface IBuildable {
    IReadOnlyList<DefineThingClassCount> NeedResources();

    Define_Thing EntityDefineToBuildComplete();
}