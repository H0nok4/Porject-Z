using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigType
{
    [Serializable]
    public class EditableType
    {
        public string TypeName;
        public Type ToType()
        {
            return Type.GetType(TypeName);
        }
    }

    public enum DestroyType
    {
        None,
        Vanish,
        KillFinalize,
        Deconstruct,
        FailConstruction,
        Cancel
    }

    public enum HaulMode
    {
        None,
        ToCell,
        ToCellStorage,
        ToContainer
    }

    public enum HungryStageType
    {
        Starvation,
        WantFood,
        Nevermind,
        Full
    }

    public enum JobEndCondition
    {
        None,
        Ongoing,
        Successed,
        Incompletable,
        ForceEnd,
        Error
    }

    public enum JobTargetIndex
    {
        A,
        B
    }

    public enum JoyStageType
    {
        UnHappy,
        NeedJoy,
        Boring,
        Nevermind,
        Happy,
        VeryHappy
    }

    public enum PathMoveEndType
    {
        None,
        InCell,
        Touch
    }

    public enum PlaceSpotPriority
    {
        Unusable,
        Low,
        Medium,
        Prime
    }

    public enum RestStageType
    {
        Exhausted,
        NeedRest,
        Nevermind,
        Energetic
    }

    public enum RotationDirection
    {
        None,
        ClockWise,
        Opposite,
        Counterclockwise
    }

    public enum SectionType
    {
        Air,
        Floor,
        Wall,
        Stair
    }

    public enum SpriteFaceSide
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    public enum ThingCategory
    {
        None,
        Unit,
        Item,
        Building,
        Mirage
    }

    public enum ThingPlaceMode
    {
        Direct,
        Near
    }

    public enum ThingType
    {
        Building,
        LandItem,
        Unit
    }

    public enum ThirstyStageType
    {
        Dry,
        NeedDrink,
        Nevermind,
        Full
    }

    public enum TimeSpeed
    {
        Paused,
        Normal,
        Fast,
        Superfast,
        Ultrafast
    }

    public enum Traversability
    {
        CanStand,
        OnlyThrough,
        Impassable
    }

    public enum WipeMode
    {
        Vanish,
        Removal,
        VanishOrRemoval
    }

    public enum WorkCompleteMode
    {
        None,
        PathMoveEnd,
        Instant,
        Delay
    }

    public partial class JobDefine
    {
        public int ID; // ID 
        public string Name; //  
        public ConfigType.EditableType DriverClass; // DriverClass 
        public bool CanSuspend; // 可以中断 
    }

    public partial class WorkGiverDefine
    {
        public int ID; // ID 
        public string Name; //  
        public ConfigType.EditableType WorkGiverType; // WorkGiverType 
        public int WorkTypeID; // WorkTypeID 
        public bool ScanThings; // 扫描物体 
        public bool ScanSections; // 扫描格子 
    }
}