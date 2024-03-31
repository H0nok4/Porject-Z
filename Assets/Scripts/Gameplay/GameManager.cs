using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TestPawnObject;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Instance.Init();
        ConfigType.DataManager.Instance.InitConfigs();
        MapController.Instance.InitMap(GameObject.Find("Grid"));
        PlayerController.Instance.ThingUnitPawnUnit = (Thing_Unit_Pawn)SpawnHelper.Spawn(DataManager.Instance.ThingDefineHandler.Pawn, new PosNode(){Pos = new IntVec2(0,0),MapDataIndex = 0}); 
        //MapController.Instance.Map.GetMapDataByIndex(0).RegisterThingHandle(PlayerController.Instance.PawnUnit);
        UIManager.Instance.Init();
        UIManager.Instance.Show(DataManager.Instance.MainPanel);
        //TestPawn.Init(0, new IntVec2(0, 0), false, ThingType.Unit);
        Application.targetFrameRate = 60;//TODO:先默认60帧节省资源，后面需要搞成TPS越高FPS越低
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.HandleInput();
        GameTicker.Instance.UpdateTick();
    }
}
