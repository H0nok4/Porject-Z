using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public abstract class Thing : IThing
{


    public Define_Thing Def;

    /// <summary>
    /// ��Ʒӵ�ж��������
    /// </summary>
    public Define_Thing ItemDef;
    public ThingObject GameObject { get; set; }

    private IntVec2 _position;

    private MapData _mapData;

    public int Count = 1;//�󲿷ֵ�������Ĭ��Ϊ1��ֻ����Ʒ���ܻᳬ��1

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
    }

    public void SetPosition(IntVec2 pos, int mapDataIndex = -1)
    {
        if (Spawned)
        {
            if (mapDataIndex != -1 && _mapData != null) {
                if (mapDataIndex != MapData.Index) {
                    _mapData.UnRegisterThing(this);
                    _mapData.UnRegisterThingMapPos(this);
                    _mapData = MapData;
                }
                else {
                    _mapData.UnRegisterThingMapPos(this);
                }

            }
        }
        _position.X = pos.X;
        _position.Y = pos.Y;
        if (Spawned)
        {
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

    /// <summary>
    /// �ƶ��ٶȵ���ÿ60Tick/1�����ƶ����ٸ񣬱���5�Ļ�����12Tick������һ��
    /// </summary>

    private float _moveSpeed = 1f;

    public ThingOwner HoldingOwner;
    /// <summary>
    /// TODO��������Ҫ�ĳɶ�����
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
            //TODO:����һ���µ�ͼ�㣬��Ҫ��ע��֮ǰ�ĵ�ͼ��Ȼ��ע�����ڵĵ�ͼ
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

            //TODO:�鿴λ���Ƿ��ڵ�ͼ�ĺϷ�λ����

            return true;
        }
    }

    /// <summary>
    /// ������Ʒ����ͼ�ϵ�ʱ�����
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
            Debug.LogError("��Ҫ����һ��ʹ���е�����");
            return;
        }

        if (!Def.IsFrame)
        {
            GameObject.SetSprite(Def.ThingSprite);
        }

        IsDestroyed = false;
        MapData = mapData;
        SetPosition(_position);
        MapController.Instance.Map.ListThings.Add(this);

    }

    /// <summary>
    /// �ݻ������ʱ�����
    /// </summary>
    public virtual void DeSpawn()
    {
        if (IsDestroyed)
        {
            Debug.LogError("��Ҫ�ݻ�һ���Ѿ��ݻ��˵�����");
            return;
        }

        if (!Spawned)
        {
            Debug.LogError("��Ҫ�ݻ�һ��δ���ɵ�����");
            return;
        }

        MapController.Instance.Map.ListThings.Remvoe(this);
        Destroy();
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
            Debug.LogError("���Դݻ�һ������Ϊ�޷��ݻٵ�����");
            return;
        }

        if (IsDestroyed)
        {
            Debug.LogError("���Դݻ�һ���Ѿ����ݻٵ�����");
            return;
        }

        bool spawned = Spawned;
        //TODO:��Ϊ�Ѿ������ڵ�ͼ�ϵ����壬��Ҫ���⴦��һ�´ݻٵ�����
        if (spawned)
        {
            //TODO:�ݻ����壬�Ƴ��������
            if (HoldingOwner != null)
            {
                this.HoldingOwner.Remove(this);
            }
            _mapData.UnRegisterThing(this);
            _mapData.UnRegisterThingMapPos(this);
            UnityEngine.GameObject.Destroy(GameObject.GO);
            GameObject = null;
        }

        //TODO:�����ǰѡ���˸�����
        if (SelectThingManager.Instance.IsSelected(this))
        {
            //TODO:ȡ��ѡ��
            SelectThingManager.Instance.DeSelecte(this);
        }

    }


    /// <summary>
    /// ԭ������ֻ������Ʒ�õ������ǿ��ǵ��еĽ������Դ�����Ʒ�����Է��ڻ���
    /// </summary>
    /// <param name="thing"></param>
    public virtual void AbsorbStack(Thing thing) {

    }
    /// <summary>
    /// ԭ������ֻ������Ʒ�õ������ǿ��ǵ��еĽ������Դ�����Ʒ�����Է��ڻ���
    /// </summary>
    /// <param name="thing"></param>
    public virtual Thing SplitOff(int count) {
        if (count <= 0)
        {
            throw new ArgumentException($"������ָ�{count}����������Ʒ");
        }

        if (count >= Count)
        {
            if (count > Count)
            {
                Debug.LogError($"��Ҫ��������Ϊ{count}������Ʒ������ֻ��{Count}��");
                //count = Count;
            }

            //TODO:�ָ�����е���Ʒ���Ǹɴ�ֱ�Ӱ��������������������
            DespawnAndDeselect();
            //TODO:
            if (HoldingOwner.Contains(this))
            {
                HoldingOwner.Remove(this);
            }

            return this;
        }

        //TODO:����С�ڵ�ǰ����������Ҫ����һ���µ�����
        var newThing = ThingMaker.MakeNewThing(Def, ItemDef);
        newThing.Count = count;
        Count -= count;

        if (Spawned)
        {
            //TODO:�ڵ�ͼ�ϵ���Ʒ��Ҫ��������֮���,��Ҫ����ͼ����һ��ʱ��
            Debug.LogError("δʵ��-һ����Ʒ�����һ���µ���Ʒ����Ҫˢ�µ�ͼ����");
        }
        if (Def.UseHitPoint)
        {
            //TODO:�����������Ʒ����Ҫ�̳�ԭ��Ʒ��Ѫ��
            newThing.HP = HP;
        }
        return newThing;

    }

    private void DespawnAndDeselect()
    {
        //TODO:�����ǰ��Ʒ
    }

    public virtual bool AllowAbsorbStack(Thing thing) {
        return false;
    }


    public void PostMake()
    {
        //TODO:����������ҪΪThing���һ��UID
        if (Def.UseHitPoint)
        {
            HP = Def.MaxHitPoint;
        }
    }

}