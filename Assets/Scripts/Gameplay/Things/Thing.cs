using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public abstract class Thing : IThing {


    public Define_Thing Def;
    /// <summary>
    /// 物品拥有额外的配置
    /// </summary>
    public Define_Thing ItemDef;
    public ThingObject GameObject { get; set; }

    private PosNode _position;

    private Rotation _rotation;

    private MapData _mapData;

    public int Count = 1;//大部分的数量都默认为1，只有物品可能会超过1

    public IntVec2 Size {
        get {
            if (Def == null)
            {
                return IntVec2.One;
            }
            return Def.Size;
        }
    }

    public PosNode Position {
        get
        {
            return _position;
        }
    }

    public Rotation Rotation {
        get {
            return _rotation;
        }
        set {
            _rotation = value;
        }
    }

    public void SetPosition(IntVec2 pos, int mapDataIndex)
    {
        if (Spawned)
        {
            if (mapDataIndex != -1 && _mapData != null) {
                if (mapDataIndex != MapData.Index) {
                    _mapData.UnRegisterThingHandle(this);
                    _mapData.UnRegisterThingMapPos(this);
                    _mapData = MapData;
                }
                else {
                    _mapData.UnRegisterThingMapPos(this);
                }
            }
        }

        if (_position != null)
        {
            _position.Pos.X = pos.X;
            _position.Pos.Y = pos.Y;
        }
        else
        {
            _position = new PosNode() { Pos = pos, MapDataIndex = mapDataIndex };
        }

        if (Spawned)
        {
            if (_mapData != null) {
                _mapData.RegisterThingHandle(this);
                _mapData.RegisterThingMapPos(this);
            }

            GameObject.SetPosition(_position);
        }

    }

    private int _hp;

    public virtual int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }

    /// <summary>
    /// 移动速度等于每60Tick/1秒能移动多少格，比如5的话等于12Tick就能走一格
    /// </summary>

    private float _moveSpeed = 1f;

    public ThingOwner HoldingOwner;

    public IThingHolder ParentHolder {
        get {
            if (HoldingOwner == null) {
                return null;
            }

            return HoldingOwner.Owner;
        }
    }

    /// <summary>
    /// TODO：后面需要改成读配置
    /// </summary>
    public float MoveSpeed => _moveSpeed;

    public bool IsDestroyed { get; set; }
    public ThingCategory ThingType { get; set; }

    public MapData MapData
    {
        get
        {
            return _mapData;
        }

        set
        {
            //TODO:进入一个新地图层，需要反注册之前的地图，然后注册现在的地图
            if (Spawned)
            {
                if (_mapData != null) {
                    _mapData.UnRegisterThingHandle(this);
                    _mapData.UnRegisterThingMapPos(this);
                }
            }

            _mapData = value;
            if (Spawned)
            {
                if (_mapData != null) {
                    _mapData.RegisterThingHandle(this);
                    _mapData.RegisterThingMapPos(this);
                }
            }
        }
    }

    public bool Spawned
    {
        get;
        set;
    }

    public PosNode PositionHeld
    {
        get
        {
            //TODO:如果在地上，则直接返回位置
            if (Spawned)
            {
                return Position;
            }
            //TODO:可能在单位的背包中

            return ((Thing)HoldingOwner.Owner).PositionHeld;
        }
    }

    /// <summary>
    /// 生成物品到地图上的时候调用
    /// </summary>
    /// <param name="mapData"></param>
    public virtual void SpawnSetup(MapData mapData)
    {
        if (IsDestroyed)
        {
            if (HP <= 0 && Def.UseHitPoint)
            {
                HP = 1;
            }
        }

        if (Spawned)
        {
            Debug.LogError("想要生成一个使用中的物体");
            return;
        }

        if (!Def.IsFrame)
        {
            GameObject.SetSprite(Def.ThingSprite);
        }

        IsDestroyed = false;
        MapData = mapData;
        MapController.Instance.Map.ListThings.Add(this);
        MapData.RegisterThingHandle(this);
        MapData.RegisterThingMapPos(this);
        Rotation = Rotation.Random;

        Spawned = true;

        SetPosition(_position.Pos, mapData.Index);
    }

    /// <summary>
    /// 摧毁物体的时候调用，只是删除地图上的物体，但是不会真正摧毁
    /// </summary>
    public virtual void DeSpawn()
    {
        if (IsDestroyed)
        {
            Debug.LogError("想要摧毁一个已经摧毁了的物体");
            return;
        }

        if (!Spawned)
        {
            Debug.LogError("想要摧毁一个未生成的物体");
            return;
        }

        MapData.UnRegisterThingHandle(this);
        MapData.UnRegisterThingMapPos(this);
        MapController.Instance.Map.ListThings.Remove(this);
        Spawned = false;
        //Destroy();
    }


    public virtual void Tick() {

    }

    public virtual void PostMapInit()
    {

    }

    public virtual void TakeDamage()
    {

    }

    public void Destroy(DestroyType type = DestroyType.None)
    {
        if (!Def.Destroyable)
        {
            Debug.LogError("尝试摧毁一个配置为无法摧毁的物体");
            return;
        }

        if (IsDestroyed)
        {
            Debug.LogError("尝试摧毁一个已经被摧毁的物体");
            return;
        }

        bool spawned = Spawned;
        //TODO:作为已经生成在地图上的物体，需要额外处理一下摧毁的流程
        if (spawned)
        {
            //TODO:摧毁物体，移除父物体等
            if (HoldingOwner != null)
            {
                this.HoldingOwner.Remove(this);
            }
            _mapData.UnRegisterThingHandle(this);
            _mapData.UnRegisterThingMapPos(this);
            MapController.Instance.Map.ListThings.Remove(this);
            UnityEngine.GameObject.Destroy(GameObject.GO);
            GameObject = null;
        }

        //TODO:如果当前选中了该物体
        if (SelectThingManager.Instance.IsSelected(this))
        {
            //TODO:取消选中
            SelectThingManager.Instance.DeSelecte(this);
        }

        GameTicker.Instance.UnRegisterThing(this);

        IsDestroyed = true;
    }


    /// <summary>
    /// 原本觉得只会有物品用到，但是考虑到有的建筑可以储存物品，所以放在基类
    /// </summary>
    /// <param name="thing"></param>
    public virtual void AbsorbStack(Thing thing) {

    }
    /// <summary>
    /// 原本觉得只会有物品用到，但是考虑到有的建筑可以储存物品，所以放在基类
    /// </summary>
    /// <param name="thing"></param>
    public virtual Thing SplitOff(int count) {
        if (count <= 0)
        {
            throw new ArgumentException($"不允许分割{count}个数量的物品");
        }

        if (count >= Count)
        {
            if (count > Count)
            {
                Debug.LogError($"想要分离数量为{count}个的物品，但是只有{Count}个");
                //count = Count;
            }

            //TODO:分割出所有的物品，那干脆直接把这个对象整个给你算了
            DespawnAndDeselect();
            //TODO:
            if (HoldingOwner.Contains(this))
            {
                HoldingOwner.Remove(this);
            }

            return this;
        }

        //TODO:数量小于当前的数量，需要创建一个新的物体
        var newThing = ThingMaker.MakeNewThing(Def, ItemDef);
        newThing.Count = count;
        Count -= count;

        if (Spawned)
        {
            //TODO:在地图上的物品需要更新数量之类的,需要给地图发出一个时间
            Debug.LogError("未实现-一个物品分离出一个新的物品，需要刷新地图数据");
        }
        if (Def.UseHitPoint)
        {
            //TODO:分离出来的物品，需要继承原物品的血量
            newThing.HP = HP;
        }
        return newThing;

    }

    private void DespawnAndDeselect()
    {
        //TODO:如果当前物品
    }

    public virtual bool AllowAbsorbStack(Thing thing) {
        return false;
    }


    public void PostMake()
    {
        //TODO:后续可能需要为Thing添加一个UID
        if (Def.UseHitPoint)
        {
            HP = Def.MaxHitPoint;
        }
    }

    public virtual bool CanStackWith(Thing thing)
    {
        if (IsDestroyed || thing.IsDestroyed)
        {
            return false;
        }

        if (this.Def.Category != ThingCategory.Item || thing.Def.Category != ThingCategory.Item)
        {
            return false;
        }

        return this.Def == thing.Def;
    }

    public bool TryStackWith(Thing placeThing)
    {
        if (!CanStackWith(placeThing))
        {
            return false;
        }

        int num = ThingUtility.GetCanStackNum(this, placeThing);

        Count += num;
        placeThing.Count -= num;

        //TODO:触发物品变动事件

        if (placeThing.Count <= 0)
        {
            //合并的物品数量为0，删除
            placeThing.Destroy();
            return true;
        }

        //没合并完
        return false;
    }


}