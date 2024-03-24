

using System.Collections.Generic;

public static class AdjacentUtility {
    public static IEnumerable<PosNode> GetAroundThingPosition(Thing thing) {
        return GetAroundThingPosition(thing.Position.DeepCopy(), thing.Rotation, thing.Size);
    }

    public static IEnumerable<PosNode> GetAroundThingPosition(PosNode startPosition, Rotation rotation, IntVec2 size) {
        //TODO:后面有体积后需要返回每一格的周围的非自身的格数，现在就只返回单格的周围8格
        yield return new PosNode()
        {
            Pos = new IntVec2(startPosition.Pos.X - 1, startPosition.Pos.Y + 1),
            MapDataIndex = startPosition.MapDataIndex
        };
        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X, startPosition.Pos.Y + 1),
            MapDataIndex = startPosition.MapDataIndex
        };
        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X + 1, startPosition.Pos.Y + 1),
            MapDataIndex = startPosition.MapDataIndex
        };

        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X - 1, startPosition.Pos.Y),
            MapDataIndex = startPosition.MapDataIndex
        };

        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X + 1, startPosition.Pos.Y),
            MapDataIndex = startPosition.MapDataIndex
        };

        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X - 1, startPosition.Pos.Y - 1),
            MapDataIndex = startPosition.MapDataIndex
        };

        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X, startPosition.Pos.Y - 1),
            MapDataIndex = startPosition.MapDataIndex
        };

        yield return new PosNode() {
            Pos = new IntVec2(startPosition.Pos.X + 1, startPosition.Pos.Y - 1),
            MapDataIndex = startPosition.MapDataIndex
        };
    }
}