

public class ThinkNode_ConditionNoJob : ThinkNode_Condition {
    protected override bool Satisfied(Thing_Unit unit) {
        if (unit.JobTracker.Job != null) {
            return false;
        }

        return true;
    }
}