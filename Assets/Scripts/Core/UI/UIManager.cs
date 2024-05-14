using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using UnityEngine;

namespace UI
{
    public struct UICreateData {
        public ViewAttribute viewAttribute;

        public CmdRegAttribute cmdAttribute;
    }
    public class UIManager : MonoSingleton<UIManager> {
        public readonly List<ViewBase> UIStack = new List<ViewBase>();
        public Canvas UICanvas;

        public void Init() {
            UICanvas = GameObject.Find("UICanvas")?.GetComponent<Canvas>();
        }

        public void Show(int uiUID) {

        }

        public ViewBase Show(Type uiPanelType) {
            var uiData = new UICreateData();

            var attributes = uiPanelType.GetCustomAttributes(true);

            foreach (var attribute in attributes) {
                if (attribute is ViewAttribute viewAttribute) {
                    uiData.viewAttribute = viewAttribute;
                }else if (attribute is CmdRegAttribute cmdAttribute) {
                    uiData.cmdAttribute = cmdAttribute;
                }
            }

            //var viewAttribute = uiPanelType.GetCustomAttributes(true)
            //    .First((type) => type.GetType() == typeof(ViewAttribute)) as ViewAttribute;
            if (uiData.viewAttribute == null)
            {
                Debug.LogError("打开的UIPanel没有ViewAttribute");
                return null;
            }

            var view = (FGUIView) Activator.CreateInstance(uiPanelType);

            view.Initialize(uiData.viewAttribute);

            //TODO:后面可以和ViewAttribute合并一下,只用取一次就可以

            if (uiData.cmdAttribute != null) {
                RegisterCmd(view, uiData.cmdAttribute);
            }


            UIStack.Add(view);

            OpenUI(view);

            return view;
            //var panelObject = GameObject.Instantiate(uiPanel, Vector3.zero, Quaternion.identity);
            
            //if (UIStack.Count > 0) {
            //    var topPanel = UIStack.Last();
            //    topPanel.Hide();
            //}
            //UIStack.Add(panelObject);
            //panelObject.Show();

            //panelObject.Rect.SetParent(UICanvas.transform);
            //panelObject.Rect.localPosition = Vector3.zero;
        }

        private void RegisterCmd(FGUIView view,CmdRegAttribute cmdAttribute) {
            foreach (var cmd in cmdAttribute.CmdArray) {
                EventDispatcher.RegEventListener<CmdData>(cmd,view.OnCmd);
            }
        }

        public void SendUIEvent(string cmdName,params object[] paramArray) {
            var cmdReg = new CmdData() { CmdName = cmdName, Param = new List<object>(paramArray) };
            EventDispatcher.TriggerEvent(cmdName,cmdReg);
        }

        public void Close(Type uiPanelType)
        {
            var ui = UIStack.Find((view) => view.GetType() == uiPanelType);
            if (ui == null)
                return;
            //TODO:后面还需要改造成关闭后显示出其他界面的时候需要一个事件
            CloseUI(ui);

        }

        public void Update()
        {
            foreach (var viewBase in UIStack)
            {
                viewBase.Update();
            }

        }
        private void OpenUI(ViewBase viewBase)
        {
            if (!viewBase.IsActive)
            {
                viewBase.Show();
            }
        }

        private void CloseUI(ViewBase viewBase)
        {
            if (viewBase.IsActive)
            {
                viewBase.Hide();
            }
        }

        public void CloseCurrent() {
            if (UIStack.Count > 0) {
                var topPanel = UIStack[^1];
                UIStack.RemoveAt(UIStack.Count - 1);
                CloseUI(topPanel);
            }

            if (UIStack.Count > 0) {
                var topPanel = UIStack[^1];
                OpenUI(topPanel);
            }
        }

        public T Find<T>() where T : ViewBase
        {
            foreach (var uiPanel in UIStack)
            {
                if (uiPanel is T)
                {
                    return (T)uiPanel;
                }
            }

            return null;
        }

    }

}

