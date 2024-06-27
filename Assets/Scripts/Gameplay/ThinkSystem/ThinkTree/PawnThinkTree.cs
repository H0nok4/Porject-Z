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

            var satisifyNode = new ThinkNode_Priority();
            satisifyNode.Children.Add(new JobGiver_GetFood());
            Root.Children.Add(satisifyNode);
            //TODO:添加满足各种食物,饥渴度,娱乐等需求的节点


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
