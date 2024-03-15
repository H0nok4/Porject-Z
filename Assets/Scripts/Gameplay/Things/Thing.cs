using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using UnityEngine;

public abstract class Thing : IThing {
    public Define_Thing Def;

    /// <summary>
    /// 物品拥有额外的配置
    /// </summary>
    public Define_Thing ItemDef;
    public ThingObject GameObject { get; set; }

    private IntVec2 _position;

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

    public IntVec2 Position {
        get {
            return _position;
        }
        set {
            if (value == _position) {
                return;
            }

            if (_mapData != null) {
                _mapData.UnRegisterThing(this);
                _mapData.UnRegisterThingMapPos(this);
            }

            _position.X = value.X;
            _position.Y = value.Y;

            if (_mapData != null) {
                _mapData.RegisterThing(this);
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


    private float _moveSpeed = 10f;

    public ThingOwner HoldingOwner;
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
            if (_mapData != null)
            {
                _mapData.UnRegisterThing(this);
                _mapData.UnRegisterThingMapPos(this);
            }
            _mapData = value;
            if (_mapData != null)
            {
                _mapData.RegisterThing(this);
                _mapData.RegisterThingMapPos(this);
            }
        }
    }

    public bool Spawned
    {
        get
        {
            if (MapData == null)
            {
                return false;
            }

            //TODO:查看位置是否在地图的合法位置上

            return true;
        }
    }

    protected Thing(ThingObject gameObject, MapData mapData, IntVec2 position)
    {
        GameObject = gameObject;
        Position = position.Copy();
        MapData = mapData;
        IsDestroyed = false;
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
        }

        //TODO:如果当前选中了该物体
        if (SelectThingManager.Instance.IsSelected(this))
        {
            //TODO:取消选中
            SelectThingManager.Instance.DeSelecte(this);
        }

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

}