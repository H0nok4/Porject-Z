using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PresetsThing {
    
}

public interface PresetsThingController {
    public void Init();
}

public class PresetsResourceContainer : PresetsThing
{

}

public class PresetsResourceContainerController : PresetsThingController
{
    public void Init()
    {
        
    }
}



public class PresetsThingManager : MonoSingleton<PresetsThingManager>
{
    //TODO:各种提前设置的建筑单位剧情触发器等的管理者
    //TODO:作为总管理者,还需要其他分类的接口

}
