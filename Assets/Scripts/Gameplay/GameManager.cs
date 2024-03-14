using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TestPawnObject;
    // Start is called before the first frame update
    void Start()
    {
        MapController.Instance.InitMap(GameObject.Find("Grid"));
        PlayerController.Instance.pawn = new Pawn(new ThingObject(TestPawnObject), MapController.Instance.Map.GetMapDataByIndex(0), new IntVec2(0, 0));
        MapController.Instance.Map.GetMapDataByIndex(0).RegisterThing(PlayerController.Instance.pawn);
        //TestPawn.Init(0, new IntVec2(0, 0), false, ThingType.Unit);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.HandleInput();
        GameTicker.Instance.UpdateTick();
    }
}
