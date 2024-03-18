using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class SelectThingManager : Singleton<SelectThingManager> {
    //TODO:管理玩家选中的物体，分单选和多选
    public List<object> SelectedObject = new List<object>();

    public bool IsSelected(object thing)
    {
        //判断物体是否被选择中
        return SelectedObject.Contains(thing);
    }

    public void DeSelecte(object thing)
    {
        //取消选中物体
        if (SelectedObject.Contains(thing))
        {
            SelectedObject.Remove(thing);
        }
    }

    public void Select(object thing)
    {
        //TODO:添加到选中物体列表中,如果是个Thing，需要特殊处理，例如在UI上刷新显示
        SelectedObject.Add(thing);
    }
}