namespace ThinkSystem
{
    public class PawnThinkTree : ThinkTreeDefine {

        public static PawnThinkTree Instance => new PawnThinkTree();

        public PawnThinkTree() {
            //TODO:测试，后面需要用图形化工具来设计行为树，然后转换成文件保存
            Root = new ThinkNode_Priority();
            var colonistNode = new ThinkNode_ConditionColonist();
            var draftNode = new ThinkNode_ConditionDraft();
            draftNode.Children.Add(new JobGiver_GoToStandablePos());
            draftNode.Children.Add(new JobGiver_WaitForCombat());
            colonistNode.Children.Add(draftNode);
            //TODO:征兆的默认只会站在原地等人战斗
            Root.Children.Add(colonistNode);
            var noJobNode = new ThinkNode_ConditionNoJob();
            Root.Children.Add(noJobNode);
            noJobNode.Children.Add(new JobGiver_GetWork());
            var randomChoose = new ThinkNode_RandomChoose();
            noJobNode.Children.Add(randomChoose);
            randomChoose.Children.Add(new JobGiver_Blankly());
            randomChoose.Children.Add(new JobGiver_WalkAround());

        }
    }
}
