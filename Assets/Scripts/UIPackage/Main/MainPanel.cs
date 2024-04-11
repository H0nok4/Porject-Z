using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using Main;
using UI;
using UnityEngine;

[View("Main","MainView")]
public class MainPanel : FGUIBase
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
}
