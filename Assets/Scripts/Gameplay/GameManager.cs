using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pawn TestPawn;
    // Start is called before the first frame update
    void Start()
    {
        MapController.Instance.InitMap(GameObject.Find("Grid"));
        PlayerController.Instance.pawn = TestPawn;
        MapController.Instance.Map.GetMapDataByIndex(0).RegisterThing(TestPawn,TestPawn.Position);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController.Instance.HandleInput();
    }
}
