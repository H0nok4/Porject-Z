using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MapController.Instance.InitMap(GameObject.Find("Grid"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
