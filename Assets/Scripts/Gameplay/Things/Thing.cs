using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using UnityEngine;

public abstract class Thing : IThing {
    public ThingObject GameObject { get; set; }

    public IntVec2 _position;
    public IntVec2 Position {
        get {
            return _position;
        }
        set {
            if (value == _position) {
                return;
            }

            _position.X = value.X;
            _position.Y = value.Y;

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
    public MapData MapData { get; set; }

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