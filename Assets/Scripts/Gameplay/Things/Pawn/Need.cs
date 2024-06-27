using ConfigType;
using UnityEngine;

public abstract class Need {
    protected const int TrySatisfyInterval = 600;
    //TODO:人物相关的需求
    public NeedDefine NeedDef;

    protected Thing_Unit Unit;

    protected float _curValue;

    protected virtual float MaxValue => 1f;

    protected NeedType Type => NeedDef.Type;

    public long PreTrySatisfyTick;
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

    public abstract void Tick(int interval);

    public virtual void Init(NeedDefine define,Thing_Unit unit) {
        Unit = unit;
        NeedDef = define;
        CurValue = MaxValue;
    }
    public bool CanTrySatisfied() {
        //暂定每10秒检测一次
        bool result = PreTrySatisfyTick + TrySatisfyInterval < GameTicker.Instance.CurrentTick;
        if (result) {
            PreTrySatisfyTick = GameTicker.Instance.CurrentTick;
        }
        return result;
    }
}