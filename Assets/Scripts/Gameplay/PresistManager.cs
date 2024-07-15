using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public interface IPresetsThingController {
    public PresetType Type { get; }

    public void Init();

    public void Register(PresetsThing thing);

    public void UnRegister(PresetsThing thing);
}

public enum PresetType
{
    GenResourceContainer, //生成各种物资的野生容器
}

public abstract class PresetsThing : MonoBehaviour
{
    public abstract PresetType Type { get; }

    public void Start()
    {
        PresetsThingManager.Instance.Register(this);
    }

    public abstract void OnRegister();
}

public class PresetsResourceContainerController : IPresetsThingController
{
    public PresetType Type => PresetType.GenResourceContainer;
    
    public void Init()
    {
        
    }

    public void Register(PresetsThing thing)
    {

    }

    public void UnRegister(PresetsThing thing)
    {

    }
}



public class PresetsThingManager : Singleton<PresetsThingManager>
{
    //TODO:各种提前设置的建筑单位剧情触发器等的管理者
    //TODO:作为总管理者,还需要其他分类的接口

    private Dictionary<PresetType, IPresetsThingController> _regisgerControllers = new Dictionary<PresetType, IPresetsThingController>();

    public void Register(PresetsThing thing)
    {
        if (!_regisgerControllers.ContainsKey(thing.Type))
        {
            Logger.Instance?.LogError($"预设物体中没有存在:{thing.Type}类型的Controller");
            return;
        }

        _regisgerControllers[thing.Type].Register(thing);
    }

    public void UnRegister(PresetsThing thing)
    {
        if (!_regisgerControllers.ContainsKey(thing.Type)) {
            Logger.Instance?.LogError($"预设物体中没有存在:{thing.Type}类型的Controller");
            return;
        }

        _regisgerControllers[thing.Type].UnRegister(thing);
    }

    public void Init()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(IPresetsThingController).IsAssignableFrom(t) && t.IsClass);

        //TODO:注册Controller
        foreach (Type type in types) {
            IPresetsThingController instance = (IPresetsThingController)Activator.CreateInstance(type);
            if (_regisgerControllers.ContainsKey(instance.Type))
            {
                Logger.Instance?.LogError($"注册IPresetsThingController的时候发现重复的PresetType,目前已经存在:{_regisgerControllers[instance.Type].GetType()},还需要注册:{type}");
                continue;
            }

            _regisgerControllers.Add(instance.Type,instance);
        }
    }
}
