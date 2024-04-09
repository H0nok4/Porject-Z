
using System;
using System.Collections.Generic;
using ConfigType;
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

    public JobTargetInfo InfoC;

    public List<JobTargetInfo> InfoQueueA;

    public List<JobTargetInfo> InfoQueueB;
    public JobTargetInfo GetTarget(JobTargetIndex index)
    {
        switch (index)
        {
            case JobTargetIndex.A:
                return InfoA;
            case JobTargetIndex.B:
                return InfoB;
            case JobTargetIndex.C:
                return InfoC;
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
        switch (index)
        {
            case JobTargetIndex.A:
                InfoA = info;
                break; 
            case JobTargetIndex.B:
                InfoB = info;
                break;
            case JobTargetIndex.C:
                InfoC = info;
                break;
            default:
                    break;
        }
    }
}
