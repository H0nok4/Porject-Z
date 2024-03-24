public struct JobTargetInfo {
    public Thing Thing;

    public PosNode Position;

    public Thing_Unit Unit => Thing as Thing_Unit;

    public Thing_Building Building => Thing as Thing_Building;
    public bool IsValid
    {
        get
        {
            if (Thing != null)
            {
                return true;
            }

            if (Position != null)
            {
                return true;
            }

            return false;
        }
    }

    public JobTargetInfo(Thing t)
    {
        Thing = t;
        Position = null;
    }

    public JobTargetInfo(PosNode position)
    {
        Thing = null;
        Position = position;
    }

    public static bool operator ==(JobTargetInfo left, JobTargetInfo right) {
        if (left.Thing != null || right.Thing != null) {
            return left.Thing == right.Thing;
        }

        if (left.Position != null || right.Position != null) {
            return left.Position == right.Position;
        }

        return true;
    }

    public static bool operator !=(JobTargetInfo left, JobTargetInfo right) {
        return !(left == right);
    }

    public static implicit operator JobTargetInfo(Thing target) {
        return new JobTargetInfo(target);
    }

    public static implicit operator JobTargetInfo(PosNode pos) {
        return new JobTargetInfo(pos);
    }
}