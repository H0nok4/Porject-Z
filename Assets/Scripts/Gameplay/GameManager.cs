using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TestPawnObject;
    // Start is called before the first frame update
    void Start()
    {
        DataTableManager.Instance.Init();
        MapController.Instance.InitMap(GameObject.Find("Grid"));
        PlayerController.Instance.PawnUnit = (Pawn)SpawnHelper.Spawn(DataTableManager.Instance.ThingDefineHandler.Pawn, new IntVec2(0, 0),0); 
        //MapController.Instance.Map.GetMapDataByIndex(0).RegisterThing(PlayerController.Instance.PawnUnit);
        UIManager.Instance.Init();
        UIManager.Instance.Show(DataTableManager.Instance.MainPanel);
        //TestPawn.Init(0, new IntVec2(0, 0), false, ThingType.Unit);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.HandleInput();
        GameTicker.Instance.UpdateTick();
    }
}
