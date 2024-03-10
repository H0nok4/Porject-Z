using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Playables;

public struct ThinkResult : IEquatable<ThinkResult>
{
    private Job _jobInstace;

    private ThinkNode _sourceNodeInstance;

    private bool _fromQueue;

    public Job Job => _jobInstace;

    public ThinkNode SourceNode => _sourceNodeInstance;

    public bool FromQueue => _fromQueue;

    public bool IsValid => Job != null;

    public static ThinkResult NoJob => new ThinkResult(null, null);

    public ThinkResult(Job job,ThinkNode sourceNode,bool fromeQueue = false)
    {
        _jobInstace = job;
        _sourceNodeInstance = sourceNode;
        _fromQueue = fromeQueue;
    }
    public bool Equals(ThinkResult other)
    {
        if (_jobInstace == other._jobInstace && _jobInstace == other._jobInstace) {
            return _fromQueue == other._fromQueue;
        }
        return false;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is ThinkResult result)) {
            return false;
        }
        return Equals(result);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}