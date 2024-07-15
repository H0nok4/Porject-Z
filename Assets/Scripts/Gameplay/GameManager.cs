using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Gameplay;
using ConfigType;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void Init()
    {
        UIPackageManager.FGUIBindAll();
        ConfigType.DataManager.Instance.InitConfigs();
        UIManager.Instance.Init();
        MapController.Instance.InitMap(GameObject.Find("Grid"));
        PresetsThingManager.Instance.Init();
        Application.targetFrameRate = 60;//TODO:先默认60帧节省资源，后面需要搞成TPS越高FPS越低
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();

        PlayerController.Instance.ThingUnitPawnUnit2 = (Thing_Unit_Pawn)SpawnHelper.Spawn(DataManager.Instance.GetThingDefineByID(1), new PosNode(new IntVec2(0, 1), 0) { });
        PlayerController.Instance.ThingUnitPawnUnit2.IsColonist = true;
        PlayerController.Instance.ThingUnitPawnUnit = (Thing_Unit_Pawn)SpawnHelper.Spawn(DataManager.Instance.GetThingDefineByID(1), new PosNode(new IntVec2(0, 0), 0) { });
        PlayerController.Instance.ThingUnitPawnUnit.IsColonist = true;
        UIManager.Instance.Show(DataManager.Instance.MainPanelType);
        FogManager.Instance.Init(1,512,512);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.Update();
        CameraController.Instance.HandleUpdate();
        GameTicker.Instance.UpdateTick();
    }

    void LateUpdate() {
        FogManager.Instance.Update();
    }
}
