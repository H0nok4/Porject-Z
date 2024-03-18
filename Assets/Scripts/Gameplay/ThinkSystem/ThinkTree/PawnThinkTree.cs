
public class PawnThinkTree : ThinkTreeDefine
{

    public static PawnThinkTree Instance => new PawnThinkTree();

    public PawnThinkTree()
    {
        //TODO:测试，后面需要用图形化工具来设计行为树，然后转换成文件保存
        Root = new ThinkNode_ConditionNoJob();
        var priority = new ThinkNode_Priority();
        Root.Children.Add(priority);
        priority.Children.Add(new JobGiver_GetWork());
        var randomChoose = new ThinkNode_RandomChoose();
        priority.Children.Add(randomChoose);
        randomChoose.Children.Add(new JobGiver_Blankly());
        randomChoose.Children.Add(new JobGiver_WalkAround());

    }
}