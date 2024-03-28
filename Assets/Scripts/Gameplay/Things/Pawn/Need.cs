using UnityEngine;

public abstract class Need
{
    //TODO:人物相关的需求
    public NeedDefine NeedDef;

    protected Thing_Unit Unit;

    protected float _curValue;

    protected virtual float MaxValue => 1f;

    protected virtual float CurValue
    {
        get
        {
            return _curValue;
        }
        set
        {
            _curValue = Mathf.Clamp(value,0,MaxValue);
        }
    }

    public virtual float CurValuePercent
    {
        get
        {
            return _curValue / MaxValue;
        }
        set
        {
            _curValue = value * MaxValue;
        }
    }

    protected virtual bool IsFrozen
    {
        get
        {
            //TODO:有一些条件会让需求暂时不变

            return false;
        }
    }

    public abstract void Tick();

    public Need()
    {

    }

    public Need(Thing_Unit unit)
    {
        Unit = unit;
        Init();
    }

    public virtual void Init()
    {
        CurValue = MaxValue;
    }
}