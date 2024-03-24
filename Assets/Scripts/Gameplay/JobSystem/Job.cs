
using System;
using UnityEngine;

public class Job
{
    //TODO：Job配置
    public JobDefine JobDefine;
    public string Name { get; }

    public JobDriver CurrentDriver;

    public ThinkNode JobFromThinkNode;
    /// <summary>
    /// 强制进行的工作，会一直做到结束
    /// </summary>
    public bool IsForce;

    public int Count;

    public HaulMode HaulMode;

    public JobTargetInfo InfoA;

    public JobTargetInfo InfoB;

    public JobTargetInfo GetTarget(JobTargetIndex index)
    {
        switch (index)
        {
            case JobTargetIndex.A:
                return InfoA;
            case JobTargetIndex.B:
                return InfoB;
            default:
                return InfoA;
        }
    }

    public JobDriver MakeDriver(Thing_Unit unit)
    {
        JobDriver driver = (JobDriver)Activator.CreateInstance(JobDefine.DriverClass.ToType());
        if (driver == null) 
        {
            Debug.LogError("Job没有配置对应的Driver");
            return null;
        }

        driver.Unit = unit;
        driver.Job = this;
        return driver;
    }

    public void Reset()
    {
        //TODO:重置为默认的状态
        JobDefine = null;
        CurrentDriver = null;
        JobFromThinkNode = null;
        IsForce = false;
    }

    public void SetTarget(JobTargetIndex index, JobTargetInfo info)
    {
        if (index== JobTargetIndex.A)
        {
            InfoA = info;
        }
        else
        {
            InfoB = info;
        }
    }
}
