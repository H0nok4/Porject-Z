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
            Logger.Instance?.LogError($"û���ҵ�IDΪ:{jackpotID}������");
            return null;
        }


        
        return null;
    }
}
