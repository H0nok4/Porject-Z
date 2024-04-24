using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TestPawnObject;

    public void Init()
    {
        DataManager.Instance.Init();
        UIPackageManager.FGUIBindAll();
        ConfigType.DataManager.Instance.InitConfigs();
        UIManager.Instance.Init();
        MapController.Instance.InitMap(GameObject.Find("Grid"));
        Application.targetFrameRate = 60;//TODO:先默认60帧节省资源，后面需要搞成TPS越高FPS越低
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();

        PlayerController.Instance.ThingUnitPawnUnit2 = (Thing_Unit_Pawn)SpawnHelper.Spawn(DataManager.Instance.GetThingDefineByID(1), new PosNode() { Pos = new IntVec2(0, 1), MapDataIndex = 0 });

        PlayerController.Instance.ThingUnitPawnUnit = (Thing_Unit_Pawn)SpawnHelper.Spawn(DataManager.Instance.GetThingDefineByID(1), new PosNode(){Pos = new IntVec2(0,0),MapDataIndex = 0});
        UIManager.Instance.Show(DataManager.Instance.MainPanelType);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.Update();
        GameTicker.Instance.UpdateTick();
    }
}
