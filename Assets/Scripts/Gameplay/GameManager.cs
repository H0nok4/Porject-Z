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
        FogManager.Instance.Init(1,512,512);

        //FogManager.Instance.UpdateFOWUnit(PlayerController.Instance.ThingUnitPawnUnit,0,
        //    new List<IntVec2>() {
        //        new IntVec2(0,0), new IntVec2(1, 0), new IntVec2(2, 0), new IntVec2(3, 0), new IntVec2(4, 0),
        //        new IntVec2(0,1),new IntVec2(1,1),new IntVec2(2,1),new IntVec2(3,1),new IntVec2(4,1),
        //        new IntVec2(0,2),new IntVec2(1,2),new IntVec2(2,2),new IntVec2(3,2),new IntVec2(4,2),
        //        new IntVec2(0,3),new IntVec2(1,3),new IntVec2(2,3),new IntVec2(3,3),new IntVec2(4,3),
        //        new IntVec2(0,4),new IntVec2(1,4),new IntVec2(2,4),new IntVec2(3,4),new IntVec2(4,4),
        //    });

        //var vec = ;
        //StringBuilder sb = new StringBuilder();
        //sb.AppendLine("视野范围内的点为:");
        //foreach (var intVec2 in vec)
        //{
        //    sb.Append($"{intVec2},");
        //    DebugDrawer.DrawBox(intVec2);
        //}
        //Debug.LogWarning(sb);
        
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
