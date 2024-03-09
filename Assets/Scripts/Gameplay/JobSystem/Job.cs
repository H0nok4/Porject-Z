
using System;
using UnityEngine;

public class Job
{
    //TODO：Job配置
    public JobDefine JobDefine;
    public string Name { get; }

    public JobDriver CurrentDriver;

    public Thing_Unit Unit;

    /// <summary>
    /// 强制进行的工作，会一直做到结束
    /// </summary>
    public bool IsForce;

    public JobDriver MakeDriver(Thing_Unit unit)
    {
        JobDriver driver = (JobDriver)Activator.CreateInstance(JobDefine.DriverClass);
        if (driver == null) 
        {
            Debug.LogError("Job没有配置对应的Driver");
            return null;
        }

        driver.Unit = unit;
        driver.Job = this;
        return driver;
    }

}
