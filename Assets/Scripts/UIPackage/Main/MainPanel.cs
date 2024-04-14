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

    public Thing TrackedThing;

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

    public override void Update()
    {
        if (TrackedThing != null)
        {
            UpdateCurTrackedThing();
        }

        if (PlayerController.Instance.DragBox.IsDrag && PlayerController.Instance.DragBox.IsValid)
        {
            _main.m_CtrlShowDragBox.SetSelectedIndex(1);
            DrawDragBox();
        }
        else
        {
            if (_main.m_CtrlShowDragBox.selectedIndex == 1)
                _main.m_CtrlShowDragBox.SetSelectedIndex(0);

        }
    }

    private void DrawDragBox()
    {
        var dragBox = PlayerController.Instance.DragBox;
        var dragBoxUI = _main.m_DragBox;
        var startPos = dragBox.StartDragUIPosition;
        var curPos = dragBox.CurDragPosition;
        if (startPos.x > curPos.x && startPos.y > curPos.y)
        {
            //当前在左上角,点击位置在右下角,UI位置为当前鼠标位置,长度为起始位置X -  当前位置X 高度为起始位置Y - 当前位置Y;
            dragBoxUI.SetPosition(curPos.x,curPos.y,0);
            dragBoxUI.SetSize(startPos.x - curPos.x,startPos.y - curPos.y);
        }else if (startPos.x < curPos.x && startPos.y > curPos.y)
        {
            dragBoxUI.SetPosition(startPos.x,curPos.y,0);
            dragBoxUI.SetSize(curPos.x - startPos.x,startPos.y - curPos.y);
        }else if (startPos.x < curPos.x && startPos.y < curPos.y)
        {
            dragBoxUI.SetPosition(startPos.x, startPos.y, 0);
            dragBoxUI.SetSize(curPos.x - startPos.x, curPos.y - startPos.y);
        }
        else
        {
            dragBoxUI.SetPosition(curPos.x, startPos.y, 0);
            dragBoxUI.SetSize(startPos.x - curPos.x, curPos.y - startPos.y);
        }
    }

    private void UpdateCurTrackedThing()
    {
        if (TrackedThing == null || !TrackedThing.Spawned)
        {
            TrackedThing = null;
            _main.m_CtrlShowThingDes.SetSelectedIndex(0);
            return;
        }

        _main.m_CtrlShowThingDes.SetSelectedIndex(1);
        switch (TrackedThing.Def.Category) {
            case ThingCategory.Unit:
                //TODO:刷新单位
                _main.m_ComThingDes.RefreshAsUnit((Thing_Unit)TrackedThing);
                break;
            case ThingCategory.Building:
                if (TrackedThing.Def.IsFrame) {
  
                    _main.m_ComThingDes.RefreshAsFrame((Thing_Building_Frame)TrackedThing);
                }
                else {
                    Debug.Log("当前点击的是实际的建筑");
                    _main.m_ComThingDes.RefreshAsBuilding(TrackedThing);
                }
                break;
            case ThingCategory.Mirage:
                _main.m_ComThingDes.RefreshAsBlueprint((Thing_Blueprint_Building)TrackedThing);
                break;
            case ThingCategory.Item:
                Debug.Log("当前点击的是物品");
                _main.m_ComThingDes.RefreshAsItem(TrackedThing);
                break;
            default:
                return;
        }
    }

    public void SetCurTrackedThing(Thing thing) {
        if (thing == null)
        {
            _main.m_CtrlShowThingDes.SetSelectedIndex(0);
            TrackedThing = null;
            return;
        }

        TrackedThing = thing;
        UpdateCurTrackedThing();

    }
}
