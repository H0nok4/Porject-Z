using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public class PresetGenResourceContainer : PresetsThing
{
    //TODO:����Ϸ��ʼ��ʱ��ע�ᵽ��������,Ȼ���ɹ�����ͳһ����
    public override PresetType Type => PresetType.GenResourceContainer;

    public int ThingID;

    public int JackpotID;

    public override void OnRegister()
    {
        //TODO:����һ��������Ԥ��λ��
        var mapLayer = GetComponentInParent<PresetsMapLayer>();
        if (mapLayer == null)
        {
            Logger.Instance?.LogError("Ԥ���������ڵ������Parent��û������PresetsMapLayer");
            return;
        }

        var container = SpawnHelper.Spawn(DataManager.Instance.GetThingDefineByID(ThingID),
            new PosNode(new IntVec2((int)gameObject.transform.position.x, (int)gameObject.transform.position.y),
                mapLayer.MapLayer));

        if (container is Thing_GenResourceContainer genContainer)
        {
            //TODO:ע��   
            genContainer.UseJackpotDefineID = JackpotID;
        }

        Logger.Instance?.Log($"����һ��ResContainer��:Pos = {container.Position} λ��");
        gameObject.SetActive(false);
    }

    public override void OnUnRegister()
    {
        
    }
}
