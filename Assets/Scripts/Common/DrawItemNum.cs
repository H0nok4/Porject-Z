using ConfigType;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawItemNum : MonoBehaviour
{
    public ThingWithComponent thingWithComponent;

    public void Init(ThingWithComponent thing)
    {
        thingWithComponent = thing;

    }

    void OnGUI() {
        if (thingWithComponent != null && thingWithComponent.Def != null && thingWithComponent.Def.Category == ThingCategory.Item && thingWithComponent.Count > 0) {
            //TODO:绘制一下数量
            var screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
            GUI.color = Color.red;
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter; // 设置文本对齐方式为中心
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y - 30, 70, 30), thingWithComponent.Count.ToString(), style);
            GUI.color = Color.white;
        }

    }
}
