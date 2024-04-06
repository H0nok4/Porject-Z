using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigType
{
    [Serializable]
    public class EditableType
    {
        public string TypeName;
        private Type _typeInstance;
        public Type ToType()
        {
            if (_typeInstance == null)
            {
                _typeInstance = Type.GetType(TypeName);
            }

            return _typeInstance;
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
        None,
        A,
        B,
        C
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

    public partial class ThingDefine
    {
        public int ID; // ID 
        public string Name; //  
        public string Describe; //  
        public ConfigType.EditableType ThingClass; // 物体使用的类 
        public ThingCategory Category; // 类型 
        public bool Destroyable; // 是否可以被摧毁 
        public bool UseHitPoint; // 是否使用血量 
        public int StackLimit; // 堆叠限制数量 
        public int MaxHitPoint; // 血量上限 
        public bool EverHaulable; // 是否能够被搬运 
        public bool Rotatable; // 是否可以旋转 
        public List<int> WorkGiverDefineTypeID; // 使用的WorkGiverDefine的ID列表 
        public Traversability Passability; // 是否可通行性 
        public int MoveCost; // 在移动时所添加的额外消耗 
        public int SizeX; // 大小的宽度 
        public int SizeY; // 大小的高度 
        public string BlueprintSpritePath; // 作为蓝图存在时显示的图片路径 
        public string ThingSpritePath; // 作为物体存在时显示的图片路径 
        public string FrameSpritePath; // 作为结构存在时显示的图片路径 
        public int Workload; // 建筑建造的工作量 
        public List<int> BuildCostThingID; // 如果是建筑的建筑消耗物品ID列表 
        public List<int> BuildCostThingNum; // 如果是建筑的建筑消耗物品数量列表 
        public bool Selectable; // 物体是否可以被选中 
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