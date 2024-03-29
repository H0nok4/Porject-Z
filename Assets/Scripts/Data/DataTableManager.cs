using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEditor.Rendering;
using UnityEngine;

public partial class DataTableManager : Singleton<DataTableManager>
{

    public JobDefineHandler JobDefineHandler;

    public WorkGiverDefineHandler WorkGiverDefineHandler;

    public ThingDefineHandler ThingDefineHandler;

    private GameObject _thingObject;

    private Sprite _frameSprite;

    public Sprite FrameSprite
    {
        get
        {
            if (_frameSprite == null)
            {
                _frameSprite = Resources.Load<Sprite>("Sprite/GeneralFrame");
            }

            return _frameSprite;
        }
    }

    public GameObject ThingObject {
        get {
            if (_thingObject == null) {
                _thingObject = Resources.Load<GameObject>("GameObject/ThingObject");
            }

            return _thingObject;
        }
    }




    private MainPanel _mainPanel;

    public MainPanel MainPanel
    {
        get
        {
            if (_mainPanel == null)
            {
                _mainPanel = Resources.Load<MainPanel>("UIGameObject/MainPanel");
            }

            return _mainPanel;
        }
    }

    public void Init()
    {
        JobDefineHandler = Resources.Load<JobDefineHandler>("Defines/Handler/JobDefineHandler");
        WorkGiverDefineHandler = Resources.Load<WorkGiverDefineHandler>("Defines/Handler/WorkGiverDefineHandler");
        ThingDefineHandler = Resources.Load<ThingDefineHandler>("Defines/Handler/ThingDefineHandler");
    }
}
