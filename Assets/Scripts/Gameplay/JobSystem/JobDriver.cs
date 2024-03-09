

using System.Collections.Generic;
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

    public PathMover PathMover => Unit.PathMover;

    public MapData MapData => Unit.MapData;

    public int DelayWaitingTickCount;

    public Work CurrentWork
    {
        get
        {
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
                }
            }
        }
    }

    public abstract IEnumerable<Work> MakeWorks();

    public void SetNextWork(Work work)
    {
        if (Works.IndexOf(work) is {} nextWorkIndex)
        {
            NextWorkIndex = nextWorkIndex;
        }
    }

    public void SetupWork()
    {
        //TODO:如果当前的工作完成了，设置新的工作
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

        CurrentWorkIndex = 0;
    }

    public void CanStartNextWork()
    {
        _canStartNextWork = true;
        TryStartNextWork();
    }

    private void TryStartNextWork()
    {
        if (NextWorkIndex >= 0)
        {
            CurrentWorkIndex = NextWorkIndex;
        }
        else
        {
            CurrentWorkIndex++;
        }

        _canStartNextWork = false;

        if (CurrentWork == null)
        {
            //TODO:没有工作了，当前的任务完成了
            EndJob();
        }
    }

    private void EndJob()
    {
        
    }
}