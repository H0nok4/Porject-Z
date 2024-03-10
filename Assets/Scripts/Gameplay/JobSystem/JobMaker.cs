using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class JobMaker {

    public static Job MakeJob()
    {
        Job job = SimplePool<Job>.Get();

        return job;
    }
    public static Job MakeJob(JobDefine define)
    {
        Job job = MakeJob();
        job.JobDefine = define;

        return job;
    }

    public static void ReturnJob(Job job)
    {
        job.Reset();
        SimplePool<Job>.Return(job);
    }
}