using System.Collections;
using System.Collections.Generic;
using ConfigType;
using FairyGUI;
using Main;
using UI;
using UnityEngine;

[View("Main","MainView")]
public class MainPanel : FGUIView
{
    public UI_MainView _main;
    public override void OnShow()
    {
        _main.m_BtnPlaceThing.onClick.Set(OnClickBtnPlacingThing);
        _main.m_BtnBuildWall.onClick.Set(OnClickBtnBuildWall);
        _main.m_CtrlShowThingDes.SetSelectedIndex(0);
    }

    private void OnClickBtnBuildWall()
    {
        //TODO:接下来的左键点击都将会创建一个Frame物体和对应的工作
        DesignatorManager.Instance.DesignatorType = DesignatorType.Building;
    }

    private void OnClickBtnPlacingThing()
    {
        DesignatorManager.Instance.DesignatorType = DesignatorType.Placing;
    }

    public override void Bind(GComponent component)
    {
        base.Bind(component);
        _main = (UI_MainView) component;
    }

    public void SetCurTrackedThing(Thing thing) {
        _main.m_CtrlShowThingDes.SetSelectedIndex(1);
        switch (thing.Def.Category) {
            case ThingCategory.Unit:
                //TODO:刷新单位
                _main.m_ComThingDes.RefreshAsUnit((Thing_Unit)thing);
            break;
            case ThingCategory.Building:
                if (thing.Def.IsFrame) {
                    Debug.Log("当前点击的是框架");
                    _main.m_ComThingDes.RefreshAsFrame((Thing_Building_Frame) thing);
                }
                else {
                    Debug.Log("当前点击的是实际的建筑");
                }
                break;
            case ThingCategory.Mirage:
                _main.m_ComThingDes.RefreshAsBlueprint((Thing_Blueprint_Building)thing);
                break;
            case ThingCategory.Item:
                Debug.Log("当前点击的是物品");
                _main.m_ComThingDes.RefreshAsItem(thing);
                break;
            default:
                return;
        }
    }
}
