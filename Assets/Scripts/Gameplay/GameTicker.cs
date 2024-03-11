using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class TimeSlower {
    private int forceNormalSpeedUntil;

    private const int ForceTicksStandard = 800;

    private const int ForceTicksShort = 240;

    public bool ForcedNormalSpeed {
        get {
            return GameTicker.Instance.CurrentTick < forceNormalSpeedUntil;
        }
    }

    public void SignalForceNormalSpeed() {
        forceNormalSpeedUntil = Mathf.Max(GameTicker.Instance.CurrentTick + 800);
    }

    public void SignalForceNormalSpeedShort() {
        forceNormalSpeedUntil = Mathf.Max(forceNormalSpeedUntil, GameTicker.Instance.CurrentTick + 240);
    }
}


public class GameTicker : Singleton<GameTicker>
{
    public int CurrentTick;

    private int _ticksThisFrame;

    private TimeSpeed _curTimeSpeed = TimeSpeed.Normal;

    public TimeSpeed _prePauseTimeSpeed;

    public TickList _tickList = new TickList();

    private Stopwatch _clock = new Stopwatch();

    private float realTimeToTickThrough;

    public bool ForcedNormalSpeed;

    public bool Paused {
        get {
            if (_curTimeSpeed != 0)
            {
                return false;
            }

            return true;
        }
    }

    public void SetTimeSpeed(TimeSpeed speed)
    {
        _curTimeSpeed = speed;
    }

    public void Pause()
    {
        _prePauseTimeSpeed = _curTimeSpeed;
        _curTimeSpeed = TimeSpeed.Paused;
    }


    public float TickRateMultiplier {
        get {
            if (ForcedNormalSpeed) {
                if (_curTimeSpeed == TimeSpeed.Paused) {
                    return 0f;
                }
                return 1f;
            }
            switch (_curTimeSpeed) {
                case TimeSpeed.Paused:
                    return 0f;
                case TimeSpeed.Normal:
                    return 1f;
                case TimeSpeed.Fast:
                    return 3f;
                case TimeSpeed.Superfast:
                    return 6f;
                default:
                    return 1f;
            }
        }
    }


    private float CurTimePerTick {
        get {
            if (TickRateMultiplier == 0f) {
                return 0f;
            }
            return 1f / (60f * TickRateMultiplier);
        }
    }

    public void UpdateTick()
    {
        _ticksThisFrame = 0;
        if (Paused)
        {
            return;
        }
        float curTimePerTick = CurTimePerTick;
        if (Mathf.Abs(Time.deltaTime - curTimePerTick) < curTimePerTick * 0.1f) {
            realTimeToTickThrough += curTimePerTick;
        }
        else {
            realTimeToTickThrough += Time.deltaTime;
        }
        float tickRateMultiplier = TickRateMultiplier;
        _clock.Reset();
        _clock.Start();
        while (realTimeToTickThrough > 0f && (float)_ticksThisFrame < tickRateMultiplier * 2f) {
            DoSingleTick();
            realTimeToTickThrough -= curTimePerTick;
            _ticksThisFrame++;
            if (Paused || (float)_clock.ElapsedMilliseconds > 45.454544f) {
                break;
            }
        }
        if (realTimeToTickThrough > 0f) {
            realTimeToTickThrough = 0f;
        }
    }

    public void RegisterThing(Thing thing)
    {
        _tickList.Register(thing);
    }

    public void UnRegisterThing(Thing thing)
    {
        _tickList.UnRegister(thing);
    }

    public void DoSingleTick() {
        CurrentTick++;
        _tickList.Tick();
    }

}

public class TickList
{
    public List<Thing> Tickable;

    public List<Thing> addList;

    public List<Thing> removeList;

    public TickList()
    {
        Tickable = new List<Thing>();
        addList = new List<Thing>();
        removeList = new List<Thing>();
    }

    public void Register(Thing thing)
    {
        addList.Add(thing);
    }

    public void UnRegister(Thing thing)
    {
        removeList.Add(thing);
    }

    public void Tick()
    {
        foreach (var thing in addList)
        {
            Tickable.Add(thing);
        }
        addList.Clear();
        foreach (var thing in removeList)
        {
            Tickable.Remove(thing);
        }
        removeList.Clear();

        foreach (var thing in Tickable)
        {
            try
            {
                thing.Tick();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Tick物体的时候出现了致命错误,错误为{e}");
            }
 
        }
    }
}