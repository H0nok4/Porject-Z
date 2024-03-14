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
        //TODO:�������������������ᴴ��һ��Frame����Ͷ�Ӧ�Ĺ���
        DesignatorManager.Instance.DesignatorType = DesignatorType.Building;
    }
}
