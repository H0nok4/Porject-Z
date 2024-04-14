using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager> {
        public readonly List<ViewBase> UIStack = new List<ViewBase>();
        public Canvas UICanvas;

        public void Init() {
            UICanvas = GameObject.Find("UICanvas")?.GetComponent<Canvas>();
        }

        public void Show(int uiUID) {

        }

        public ViewBase Show(Type uiPanelType)
        {
            var viewAttribute = uiPanelType.GetCustomAttributes(true)
                .First((type) => type.GetType() == typeof(ViewAttribute)) as ViewAttribute;
            if (viewAttribute == null)
            {
                Debug.LogError("打开的UIPanel没有ViewAttribute");
                return null;
            }

            var view = (ViewBase) Activator.CreateInstance(uiPanelType);

            view.Initialize(viewAttribute);

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

        //public T Find<T>() where T : FGUIView {

        //}

    }

}

