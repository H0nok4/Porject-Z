using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Thing : IThing {
    public GameObject GameObject { get; set; }

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

            GameObject.transform.position = new Vector3(_position.X, _position.Y);
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

    protected Thing(GameObject gameObject, MapData mapData, IntVec2 position)
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