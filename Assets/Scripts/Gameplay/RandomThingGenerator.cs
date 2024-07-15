using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConfigType;
using UnityEngine;

public class RandomThingGenerator : MonoSingleton<RandomThingGenerator>
{
    public IEnumerable<Thing> GenerateThingByJackpotID(int jackpotID, int num)
    {
        var def = DataManager.Instance.GetJackpotDefineByID(jackpotID);
        if (def == null)
        {
            Logger.Instance?.LogError($"没有找到ID为:{jackpotID}的配置");
            yield break;
        }

        var totalWeight = def.Weight.Sum();
        var randomNum = Random.Range(0, totalWeight);
        for (int j = 0; j < num; j++)
        {
            for (int i = 0; i < def.ItemIDList.Count; i++) {
                randomNum -= def.Weight[i];
                if (randomNum <= 0) {
                    //TODO:随机到这个物品,生成一个随机数量的Thing
                    Thing result = ThingMaker.MakeNewThing(DataManager.Instance.GetThingDefineByID(def.ItemIDList[i]),
                        Random.Range(def.MinCount[i], def.MaxCount[i]));

                    yield return result;
                }
            }
        }
    }
}
