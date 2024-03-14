using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public partial class DataTableManager : Singleton<DataTableManager> {
    private GameObject _thingObject;

    public GameObject ThingObject {
        get {
            if (_thingObject == null) {
                _thingObject = Resources.Load<GameObject>("GameObject/ThingObject");
            }

            return _thingObject;
        }
    }

    public Define_Thing TempWallDefine = new Define_Thing()
    {
        Category = ThingCategory.Building, Destroyable = true,
        FrameSprite = Resources.Load<Sprite>("Sprite/Barricade_Frame"), MaxHitPoint = 10, Name = "TempWall",
        Passability = Traversability.Impassable, Rotatable = false,
        StackLimit = 1,ThingSprite = Resources.Load<Sprite>("Sprite/Barricade")
    };

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
}
