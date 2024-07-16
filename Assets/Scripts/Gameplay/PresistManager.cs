using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PresetsThingManager : Singleton<PresetsThingManager>
{
    //各种提前设置的建筑单位剧情触发器等的管理者
    //作为总管理者,还需要其他分类的接口

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

        //注册Controller
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
