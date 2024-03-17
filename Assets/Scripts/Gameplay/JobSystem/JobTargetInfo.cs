public struct JobTargetInfo {
    public Thing Thing;

    public Section Section;

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

            if (Section != null)
            {
                return true;
            }

            return false;
        }
    }

    public JobTargetInfo(Thing t)
    {
        Thing = t;
        Section = null;
    }

    public JobTargetInfo(Section section)
    {
        Thing = null;
        Section = section;
    }

    public static bool operator ==(JobTargetInfo left, JobTargetInfo right) {
        if (left.Thing != null || right.Thing != null) {
            return left.Thing == right.Thing;
        }

        if (left.Section != null || right.Section != null) {
            return left.Section == right.Section;
        }

        return true;
    }

    public static bool operator !=(JobTargetInfo left, JobTargetInfo right) {
        return !(left == right);
    }

    public static implicit operator JobTargetInfo(Thing t) {
        return new JobTargetInfo(t);
    }

    public static implicit operator JobTargetInfo(Section s) {
        return new JobTargetInfo(s);
    }
}