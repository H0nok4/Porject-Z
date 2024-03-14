using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class MainPanel : UIPanel
{
    public BtnBuildWall BtnBuildWall;

    public override void InitInstance()
    {
        BtnBuildWall = (BtnBuildWall)GetUIComponentAtChildIndex(0);
    }

    public override void OnShow()
    {
        BtnBuildWall.OnClick = OnClickBtnBuildWall;
    }

    private void OnClickBtnBuildWall()
    {
        //TODO:接下来的左键点击都将会创建一个Frame物体和对应的工作
        DesignatorManager.Instance.DesignatorType = DesignatorType.Building;
    }
}
