using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JobInQueue
{
    public Job Job;

    public JobInQueue(Job job) {
        Job = job;
    }

    public void Cleanup(Thing_Unit pawn, bool canReturnToPool) {
        //TODO:清楚掉的时候还需要清除Job预定的一些东西
        if (canReturnToPool) {
            JobMaker.ReturnJob(Job);
        }
        Job = null;
    }
}

public class JobQueue
{
    private List<JobInQueue> _queue = new List<JobInQueue>();

    public void EnqueueFirst(Job job) {
        _queue.Insert(0, new JobInQueue(job));
    }

    public void EnqueueLast(Job job) {
        _queue.Add(new JobInQueue(job));
    }

    public void Clear(Thing_Unit unit,bool returnPool)
    {
        for (int i = 0; i < _queue.Count; i++)
        {
            _queue[i].Cleanup(unit,returnPool);
        }
        _queue.Clear();
    }

    public bool Contains(Job job)
    {
        foreach (var jobInQueue in _queue)
        {
            if (jobInQueue.Job == job)
            {
                return true;
            }
        }

        return false;
    }

    public JobInQueue Get(Job job)
    {
        int index = _queue.FindIndex((data) => data.Job == job);
        if (index>=0)
        {
            var result = _queue[index];
            _queue.RemoveAt(index);
            return result;
        }

        return null;
    }

    public JobInQueue Dequeue()
    {
        if (_queue == null || _queue.Count <= 0)
        {
            return null;
        }

        var result = _queue[0];
        _queue.RemoveAt(0);
        return result;
    }

    public JobInQueue Peek()
    {
        return _queue[0];
    }


}