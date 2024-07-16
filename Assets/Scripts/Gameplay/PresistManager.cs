using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class PresetsThingManager : Singleton<PresetsThingManager>
{
    //������ǰ���õĽ�����λ���鴥�����ȵĹ�����
    //��Ϊ�ܹ�����,����Ҫ��������Ľӿ�

    private Dictionary<PresetType, IPresetsThingController> _regisgerControllers = new Dictionary<PresetType, IPresetsThingController>();

    public void Register(PresetsThing thing)
    {
        if (!_regisgerControllers.ContainsKey(thing.Type))
        {
            Logger.Instance?.LogError($"Ԥ��������û�д���:{thing.Type}���͵�Controller");
            return;
        }

        _regisgerControllers[thing.Type].Register(thing);
    }

    public void UnRegister(PresetsThing thing)
    {
        if (!_regisgerControllers.ContainsKey(thing.Type)) {
            Logger.Instance?.LogError($"Ԥ��������û�д���:{thing.Type}���͵�Controller");
            return;
        }

        _regisgerControllers[thing.Type].UnRegister(thing);
    }

    public void Init()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        IEnumerable<Type> types = assembly.GetTypes().Where(t => typeof(IPresetsThingController).IsAssignableFrom(t) && t.IsClass);

        //ע��Controller
        foreach (Type type in types) {
            IPresetsThingController instance = (IPresetsThingController)Activator.CreateInstance(type);
            if (_regisgerControllers.ContainsKey(instance.Type))
            {
                Logger.Instance?.LogError($"ע��IPresetsThingController��ʱ�����ظ���PresetType,Ŀǰ�Ѿ�����:{_regisgerControllers[instance.Type].GetType()},����Ҫע��:{type}");
                continue;
            }

            _regisgerControllers.Add(instance.Type,instance);
        }
    }
}
