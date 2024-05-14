using System.Collections;
using System.Collections.Generic;
using ConfigType;
using EventSystem;
using FairyGUI;
using JetBrains.Annotations;
using Main;
using UI;
using UnityEngine;

namespace UI
{
    [CmdReg(EventDef.OnSelectThing)]
    [View("Main", "MainView")]
    public class MainPanel : FGUIView {
        public UI_MainView _main;
        public Thing TrackedThing =>
            SelectManager.Instance.SelectThings.Count > 0 ? SelectManager.Instance.SelectThings[0] : null;
        public override void OnShow() {
            _main.m_BtnPlaceThing.onClick.Set(OnClickBtnPlacingThing);

            RefreshListSelection();

        }

        public void HideAllPanel() {
            UIManager.Instance.Close(typeof(DesignView));
        }

        private void RefreshDesignPanel() {
            ////TODO:测试用,后面需要抽象
            //_main.m_ComDesignPanel.m_ListType.numItems = MainDesignList.Count;
            //for (int i = 0; i < MainDesignList.Count; i++) {
            //    var design = (UI_ComDesignatorType)_main.m_ComDesignPanel.m_ListType.GetChildAt(0);
            //    design.Refresh(MainDesignList[i]);
            //    design.onClick.Set(() => { RefreshListDesignators(design.DesignType.GetDesignators()); });
            //}
            //_main.m_ComDesignPanel.m_ListType.ResizeToFit();
        }



        private void RefreshListSelection() {
            var list = _main.m_ListMainSelection;
            var btnBuildCommand = list.GetChildAt(0);
            btnBuildCommand.onClick.Set(OnClickBtnBuildCommand);
        }

        private void OnClickBtnBuildCommand() {
            //_main.m_CtrlShowThingDes.SetSelectedIndex(0);
            //_main.m_CtrlShowDesignator.SetSelectedIndex(1);
            //TODO:打开建筑的界面
            UIManager.Instance.Show(DataManager.Instance.DesignViewType);
        }

        private void OnClickBtnPlacingThing() {
            DesignatorManager.Instance.DesignatorType = DesignatorType.Placing;
        }

        public override void Bind(GComponent component) {
            base.Bind(component);
            _main = (UI_MainView)component;
        }

        public override void Update() {
            //if (TrackedThing != null)
            //{
            //    UpdateCurTrackedThing();
            //}
            //else
            //{
            //    if (_main.m_CtrlShowThingDes.selectedIndex == 1)
            //    {
            //        _main.m_CtrlShowThingDes.SetSelectedIndex(0);
            //    }
            //}

            if (PlayerController.Instance.DragBox.IsDrag && PlayerController.Instance.DragBox.IsValid) {
                _main.m_CtrlShowDragBox.SetSelectedIndex(1);
                DrawDragBox();
            }
            else {
                if (_main.m_CtrlShowDragBox.selectedIndex == 1)
                    _main.m_CtrlShowDragBox.SetSelectedIndex(0);

            }
        }

        private void DrawDragBox() {
            var dragBox = PlayerController.Instance.DragBox;
            var dragBoxUI = _main.m_DragBox;
            var startPos = dragBox.StartDragUIPosition;
            var curPos = dragBox.CurDragPosition;
            if (startPos.x > curPos.x && startPos.y > curPos.y) {
                //当前在左上角,点击位置在右下角,UI位置为当前鼠标位置,长度为起始位置X -  当前位置X 高度为起始位置Y - 当前位置Y;
                dragBoxUI.SetPosition(curPos.x, curPos.y, 0);
                dragBoxUI.SetSize(startPos.x - curPos.x, startPos.y - curPos.y);
            }
            else if (startPos.x < curPos.x && startPos.y > curPos.y) {
                dragBoxUI.SetPosition(startPos.x, curPos.y, 0);
                dragBoxUI.SetSize(curPos.x - startPos.x, startPos.y - curPos.y);
            }
            else if (startPos.x < curPos.x && startPos.y < curPos.y) {
                dragBoxUI.SetPosition(startPos.x, startPos.y, 0);
                dragBoxUI.SetSize(curPos.x - startPos.x, curPos.y - startPos.y);
            }
            else {
                dragBoxUI.SetPosition(curPos.x, startPos.y, 0);
                dragBoxUI.SetSize(startPos.x - curPos.x, curPos.y - startPos.y);
            }
        }

        //private void UpdateCurTrackedThing()
        //{
        //    if (TrackedThing == null || !TrackedThing.Spawned)
        //    {
        //        _main.m_CtrlShowThingDes.SetSelectedIndex(0);
        //        return;
        //    }

        //    _main.m_CtrlShowThingDes.SetSelectedIndex(1);
        //    switch (TrackedThing.Def.Category) {
        //        case ThingCategory.Unit:
        //            //TODO:刷新单位
        //            _main.m_ComThingDes.RefreshAsUnit((Thing_Unit)TrackedThing);
        //            break;
        //        case ThingCategory.Building:
        //            if (TrackedThing.Def.IsFrame) {

        //                _main.m_ComThingDes.RefreshAsFrame((Thing_Building_Frame)TrackedThing);
        //            }
        //            else {
        //                Debug.Log("当前点击的是实际的建筑");
        //                _main.m_ComThingDes.RefreshAsBuilding(TrackedThing);
        //            }
        //            break;
        //        case ThingCategory.Mirage:
        //            _main.m_ComThingDes.RefreshAsBlueprint((Thing_Blueprint_Building)TrackedThing);
        //            break;
        //        case ThingCategory.Item:
        //            Debug.Log("当前点击的是物品");
        //            _main.m_ComThingDes.RefreshAsItem(TrackedThing);
        //            break;
        //        default:
        //            return;
        //    }
        //}

    }

}
