

using System.Collections.Generic;
using ConfigType;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 每种Job都需要不同的Driver去驱动一系列的Work来完成，Job是个目标，Driver是驱动力，Work是路径
/// </summary>
public abstract class JobDriver
{
    public Job Job;

    public Thing_Unit Unit;

    public List<Work> Works;

    public int CurrentWorkIndex = -1;

    public int NextWorkIndex = -1;

    private bool _canStartNextWork;

    public bool Ended;
    public PathMover PathMover => Unit.PathMover;
    public MapData MapData => Unit.MapData;

    public int DelayWaitingTickCount;

    public Work CurrentWork
    {
        get
        {
            if (Works == null)
            {
                return null;
            }

            if (CurrentWorkIndex < 0 || CurrentWorkIndex >= Works.Count)
                return null;

            return Works[CurrentWorkIndex];
        }
    }


    public void Tick()
    {
        DelayWaitingTickCount--;
        if (CurrentWork == null)
        {
            Debug.Log("当前没有工作，开始一个新的工作");
            CanStartNextWork();
        }
        else
        {
            if (CurrentWork.CompleteMode == WorkCompleteMode.Delay)
            {
                if (DelayWaitingTickCount <= 0)
                {
                    //TODO:等待完毕，进入下一个工作
                    CanStartNextWork();
                    return;
                }
            }

            if (_canStartNextWork)
            {
                TryStartNextWork();
                return;
            }

            if (CurrentWork.TickAction != null) {
                CurrentWork.TickAction.Invoke();
            }
        }
    }

    public abstract IEnumerable<Work> MakeWorks();

    public abstract bool TryMakeWorkReservations(bool errorOnFailed);

    public void SetNextWork(Work work)
    {
        if (Works.IndexOf(work) is {} nextWorkIndex)
        {
            NextWorkIndex = nextWorkIndex;
        }
    }

    public void Clean()
    {
        if (CurrentWork != null)
        {
            CurrentWork.Clean();
        }
    }

    public void SetupWork()
    {
        //TODO:如果当前的工作完成了，设置新的工作
        if (Works == null)
        {
            Works = new List<Work>();
        }

        Works.Clear();
        foreach (var work in MakeWorks())
        {
            work.Unit = Unit;
            if (work.CompleteMode == WorkCompleteMode.None)
            {
                work.CompleteMode = WorkCompleteMode.Instant;
                Debug.LogError($"正在进行中的工作没有结束的类型");
            }

            Works.Add(work);
        }

        CurrentWorkIndex = -1;
    }

    public void CanStartNextWork()
    {
        _canStartNextWork = true;
        TryStartNextWork();
    }

    private void TryStartNextWork()
    {

        if (CurrentWork != null)
        {
            CurrentWork.Clean();
        }
        if (NextWorkIndex >= 0)
        {
            CurrentWorkIndex = NextWorkIndex;
            NextWorkIndex = -1;
        }
        else
        {
            CurrentWorkIndex++;
        }

        _canStartNextWork = false;
        if (CurrentWork == null)
        {
            //TODO:没有工作了，当前的任务完成了
            EndJob(JobEndCondition.Successed);
            return;
        }

        DelayWaitingTickCount = CurrentWork.NeedWaitingTick;
        if (CheckCurrentToilEndOrFail())
        {
            return;
        }

        var curWork = CurrentWork;
        if (CurrentWork.InitAction != null) {
            CurrentWork.InitAction.Invoke();
        }


        if (!Ended && CurrentWork.CompleteMode == WorkCompleteMode.Instant && CurrentWork == curWork)
        {
            CanStartNextWork();
        }
    }

    public bool CheckCurrentToilEndOrFail()
    {
        //TODO:有一些工作可能会失败
        return false;
    }

    private void EndJob(JobEndCondition condition)
    {
        if (Unit.JobTracker.Job != Job)
        {
            return;
        }

        Unit.JobTracker.EndCurrentJob(condition);
    }


    public void ClearWorks()
    {
        if (Works == null)
        {
            return;
        }

        foreach (var work in Works)
        {
            //TODO:后面可以用对象池返回到池子里
            work.Clean();
            work.InPool = true;
            SimplePool<Work>.Return(work);
        }

        Works.Clear();
    }

    public virtual void Event_PathMoveEnd() {
        if (CurrentWork.CompleteMode == WorkCompleteMode.PathMoveEnd) {
            CanStartNextWork();
        }
    }
}