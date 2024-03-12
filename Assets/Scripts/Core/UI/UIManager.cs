using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager> {
        public readonly List<UIPanel> UIStack = new List<UIPanel>();
        public Canvas UICanvas;

        public void Init() {
            UICanvas = GameObject.Find("UICanvas")?.GetComponent<Canvas>();
        }

        public void Show(int uiUID) {

        }

        public void Show(UIPanel uiPanel) {
            var panelObject = GameObject.Instantiate(uiPanel, Vector3.zero, Quaternion.identity);
            
            if (UIStack.Count > 0) {
                var topPanel = UIStack.Last();
                topPanel.Hide();
            }
            UIStack.Add(panelObject);
            panelObject.Show();

            panelObject.Rect.SetParent(UICanvas.transform);
            panelObject.Rect.localPosition = Vector3.zero;
        }

        public void CloseCurrent() {
            if (UIStack.Count > 0) {
                var topPanel = UIStack[^1];
                UIStack.RemoveAt(UIStack.Count - 1);
                topPanel.Hide();
            }

            if (UIStack.Count > 0) {
                var topPanel = UIStack[^1];
                topPanel.Show();
            }
        }

        public T Find<T>() where T : UIPanel
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

