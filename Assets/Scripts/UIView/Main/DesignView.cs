using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using Main;
using UI;
using UnityEngine;

namespace UI
{
    [View("Main", "DesignView")]
    public class DesignView : FGUIView
    {
        public UI_DesignView _main;

        public readonly List<DesignTypeBase> MainDesignList = new List<DesignTypeBase>() { new Design_Building() };
        public override void OnShow()
        {
            base.OnShow();
            RefreshDesignPanel();
            _main.m_ListCommand.RemoveChildrenToPool();
        }


        private void RefreshDesignPanel() {
            ////TODO:测试用,后面需要抽象
            _main.m_ComDesignTypePanel.m_ListType.numItems = MainDesignList.Count;
            for (int i = 0; i < MainDesignList.Count; i++) {
                var design = (UI_ComDesignatorType)_main.m_ComDesignTypePanel.m_ListType.GetChildAt(0);
                design.Refresh(MainDesignList[i]);
                design.onClick.Set(() => { RefreshListDesignators(design.DesignType.GetDesignators()); });
            }
            _main.m_ComDesignTypePanel.m_ListType.ResizeToFit();
        }

        private void RefreshListDesignators(IEnumerable<DesignatorDecoratorBase> designators) {
            _main.m_CtrlShowListCommand.SetSelectedIndex(1);
            _main.m_ListCommand.RemoveChildrenToPool();
            foreach (var designatorDecoratorBase in designators) {
                var btnDesignType = (UI_BtnDesignatorType1)_main.m_ListCommand.AddItemFromPool();
                btnDesignType.m_TxtName.text = designatorDecoratorBase.Name;
                btnDesignType.m_LoaderIcon.url = designatorDecoratorBase.Sprite;
                btnDesignType.onClick.Set(designatorDecoratorBase.OnClick);
            }
            _main.m_ListCommand.ResizeToFit();
        }

        public override void Bind(GComponent component)
        {
            base.Bind(component);
            _main = (UI_DesignView)component;
        }
    }

}
