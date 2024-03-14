using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using UnityEngine;

public abstract class Thing : IThing {
    public Define_Thing Def;
    public ThingObject GameObject { get; set; }

    private IntVec2 _position;

    private MapData _mapData;

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
    public float MoveSpeed => _moveSpeed;

    public bool IsDestoryed { get; set; }
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

    protected Thing(ThingObject gameObject, MapData mapData, IntVec2 position)
    {
        GameObject = gameObject;
        Position = position.Copy();
        MapData = mapData;
        IsDestoryed = false;
    }


    public virtual void Tick() {

    }

    public virtual void PostMapInit()
    {

    }

    public virtual void TakeDamage()
    {

    }

}