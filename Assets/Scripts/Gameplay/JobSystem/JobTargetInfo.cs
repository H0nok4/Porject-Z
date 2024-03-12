using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct JobTargetInfo {
    public Thing Thing;

    public Section Section;

    public Thing_Unit Unit => Thing as Thing_Unit;

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

}