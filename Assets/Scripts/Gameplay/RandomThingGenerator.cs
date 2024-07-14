using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public class RandomThingGenerator : MonoSingleton<RandomThingGenerator>
{
    public Thing GenerateThingByJackpotID(int jackpotID, int num)
    {
        var def = DataManager.Instance.GetJackpotDefineByID(jackpotID);
        if (def == null)
        {
            Logger.Instance?.LogError($"没有找到ID为:{jackpotID}的配置");
            return null;
        }


        
        return null;
    }
}
