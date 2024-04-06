using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class MainPanel : UIPanel
{
    public BtnBuildWall BtnBuildWall;
    public BtnPlacingThing BtnPlacingThing;

    public override void InitInstance()
    {
        BtnBuildWall = (BtnBuildWall)GetUIComponentAtChildIndex(0);
        BtnPlacingThing = (BtnPlacingThing)GetUIComponentAtChildIndex(1);
    }

    public override void OnShow()
    {
        BtnBuildWall.OnClick = OnClickBtnBuildWall;
        BtnPlacingThing.OnClick = OnClickBtnPlacingThing;
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
}
