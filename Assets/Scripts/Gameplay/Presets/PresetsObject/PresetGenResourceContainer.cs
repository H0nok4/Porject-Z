using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public class PresetGenResourceContainer : PresetsThing
{
    //TODO:在游戏开始的时候注册到管理器里,然后由管理器统一生成
    public override PresetType Type => PresetType.GenResourceContainer;

    public int ThingID;

    public int JackpotID;

    public override void OnRegister()
    {
        //TODO:生成一个物体在预制位置
        var mapLayer = GetComponentInParent<PresetsMapLayer>();
        if (mapLayer == null)
        {
            Logger.Instance?.LogError("预设物体所在的物体的Parent上没有设置PresetsMapLayer");
            return;
        }

        var container = SpawnHelper.Spawn(DataManager.Instance.GetThingDefineByID(ThingID),
            new PosNode(new IntVec2((int)gameObject.transform.position.x, (int)gameObject.transform.position.y),
                mapLayer.MapLayer));

        if (container is Thing_GenResourceContainer genContainer)
        {
            //TODO:注册   
            genContainer.UseJackpotDefineID = JackpotID;
        }

        Logger.Instance?.Log($"生成一个ResContainer在:Pos = {container.Position} 位置");
        gameObject.SetActive(false);
    }

    public override void OnUnRegister()
    {
        
    }
}
