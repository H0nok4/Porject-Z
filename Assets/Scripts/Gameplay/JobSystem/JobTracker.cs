using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEditor.PackageManager;
using UnityEngine;

public class ThingUnit_JobTracker
{
    private const int THINK_INTERVAL = 30;

    private int LastTimeThinkTick;

    public Thing_Unit Unit;

    public Job Job;

    public JobDriver JobDriver;

    public JobQueue JobQueue;

    public bool startingNewJob;

    //TODO:可以加一个队列，让单位可以按顺序工作

    public ThingUnit_JobTracker(Thing_Unit unit) {
        Unit = unit;
    }

    public void JobTrackTick()
    {
        if (LastTimeThinkTick + THINK_INTERVAL < GameTicker.Instance.CurrentTick) {
            //TODO:想想现在要做什么
            ThinkResult result = Unit.JobThinker.Root.GetResult(Unit);
            if (result.IsValid) {
                //TODO:有正经工作可以做
                if (CanStartNewJob(result)) {
                    StartJob(result.Job, JobEndCondition.ForceEnd, result.SourceNode,Unit.JobThinker.ThinkTreeDefine);
                }
            }

            LastTimeThinkTick = GameTicker.Instance.CurrentTick;
        }

        if (JobDriver != null)
        {
            JobDriver.Tick();
        }

        if (Job == null)
        {
            TryFindAndStartJob();
        }

    }


    public void StartJob(Job newJob,JobEndCondition currentJobEndCondition = JobEndCondition.None, ThinkNode jobGiverNode = null,ThinkTreeDefine tree = null,bool addJobThisTick = true,bool suspendCurrentJob = false,bool fromQueue = false)
    {
        startingNewJob = true;
        if (Job != null)
        {
            if (currentJobEndCondition == JobEndCondition.None)
            {
                Debug.LogError("当前工作的停止条件不应该为None");
                currentJobEndCondition = JobEndCondition.ForceEnd;
            }

            if (suspendCurrentJob)
            {
                //TODO:挂起当前的工作，等下做完了这个没有新的可以回来继续
                SuspendCurrentJob(currentJobEndCondition);
            }
            else
            {
                //TODO:直接结束当前的工作,当前工作会保留的一些目标也需要清除，例如工作的时候会提示以预留给XX的那种
                CleanCurrentJob();
            }

            return;
        }
        if (newJob == null)
        {
            Debug.LogError("开始了一个空的工作!");
            return;
        }

        Job = newJob;
        Job.JobFromThinkNode = jobGiverNode;
        JobDriver = Job.MakeDriver(Unit);
        newJob.JobFromThinkNode = jobGiverNode;
       
        if (JobDriver.TryMakeWorkReservations(true))
        {
            JobDriver.SetupWork();
            JobDriver.CanStartNextWork();
        }
        else
        {
            Debug.LogError("准备开始任务，设置预约时出现问题");
            EndCurrentJob(JobEndCondition.Error);
        }

        
    }

    /// <summary>
    /// 挂起当前的工作
    /// </summary>
    public void SuspendCurrentJob(JobEndCondition condition)
    {
        if (Job.JobDefine.CanSuspend)
        {
            //TODO:加入到预备的队列里
            JobQueue.EnqueueFirst(Job);
            CleanCurrentJob(false);
        }
        else
        {
            //不能挂起，直接清除掉
            CleanCurrentJob(true);
        }
    }

    /// <summary>
    /// 结束当前的工作
    /// </summary>
    /// <param name="condition">结束工作的理由类型</param>

    public void EndCurrentJob(JobEndCondition condition,bool startNewWork = true,bool returnPool = true)
    {
        Debug.LogWarning("结束当前的工作");
        CleanCurrentJob(returnPool);
        if (!startNewWork)
        {
            return;
        }

        TryFindAndStartJob();
    }

    private void TryFindAndStartJob()
    {
        if (Unit.JobThinker == null)
        {
            Debug.LogError("单位没有Thinker，无法尝试开始新的工作");
            return;
        }

        if (Job != null)
        {
            Debug.LogWarning("单位在有工作的情况下尝试开始新的工作,需要打断");
        }

        var result = ThinkNextStep(out ThinkTreeDefine define);
        if (result.IsValid)
        {
            StartJob(result.Job,JobEndCondition.None,result.SourceNode,define);
        }
    }

    public void CleanCurrentJob(bool canReturnPool = false)
    {
        Debug.LogWarning("清除当前的工作信息");
        if (Job == null)
        {
            return;
        }

        if (JobDriver != null)
        {
            JobDriver.Ended = true;
            JobDriver.Clean();
        }


        var cachedJob = Job;
        ClearJobDriver();
        Job = null;

        //TODO:如果手上拿着工作需要的物品，需要丢掉或者放回储藏区
        if (canReturnPool)
        {
            JobMaker.ReturnJob(cachedJob);
        }
    }

    private void ClearJobDriver()
    {
        if (JobDriver == null)
        {
            return;
        }

        JobDriver.ClearWorks();
        JobDriver = null;
    }

    private void ClearJobReservation() {
        ReservationManager.Instance.ClearReservationByJob(Unit, Job);
    }

    public bool CanStartNewJob(ThinkResult result) {
        if (Job == null) {
            return true;
        }

        if (Job == result.Job) {
            return false;
        }

        if (Job.JobDefine == result.Job.JobDefine) {
            return Job.JobFromThinkNode != result.SourceNode;
        }

        return true;
    }

    public ThinkResult ThinkNextStep() {
        if (Unit.JobThinker.Root == null) {
            return ThinkResult.NoJob;
        }

        return Unit.JobThinker.Root.GetResult(Unit);
    }

    public ThinkResult ThinkNextStep(out ThinkTreeDefine define)
    {
        var result = ThinkNextStep();
        if (result.Job != null)
        {
            define = Unit.JobThinker.ThinkTreeDefine;
            return result;
        }

        define = null;
        return result;
    }

    public void OnPathMoveEnd()
    {
        if (Job != null)
        {
            JobDriver.Event_PathMoveEnd();
        }
    }
}