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
    //TODO:������ǰ���õĽ�����λ���鴥�����ȵĹ�����
    //TODO:��Ϊ�ܹ�����,����Ҫ��������Ľӿ�

}
