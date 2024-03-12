using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIPanel : MonoBehaviour {
        public RectTransform Rect;

        private bool _inited = false;

        protected bool _disposed = false;

        public bool IsShow {
            get {
                return gameObject.activeSelf;
            }
        }

        public void Show() {
            if (!_inited) {
                Init();
            }

            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide() {
            OnHide();

            gameObject.SetActive(false);
        }

        private void Init() {
            InitInstance();
            Rect = GetComponent<RectTransform>();
            OnInit();

            _inited = true;
        }

        public void Dispose() {
            OnDispose();
            Destroy(gameObject);

            _disposed = true;
        }

        public virtual void OnInit() {

        }

        public virtual void OnDispose() {

        }

        public virtual void OnShow() {

        }

        public virtual void OnHide() {
        }

        public Image GetImageAtChildIndex(int index) {
            return transform.GetChild(index).GetComponent<Image>();
        }

        public TMP_Text GetTextAtChildIndex(int index) {
            return transform.GetChild(index).GetComponent<TMP_Text>();
        }

        public UIComponent GetUIComponentAtChildIndex(int index) {
            try {
                var uiComponent = transform.GetChild(index).GetComponent<UIComponent>();
                uiComponent.Init();
                return uiComponent;
            }
            catch (Exception e) {
                Debug.LogError($"获取UIComponent时出现错误，错误Index为：{index},{e}");
                throw;
            }

        }
        public UIList GetUIListAtChildIndex(int index) {
            var uiList = transform.GetChild(index).GetComponent<UIList>();
            uiList.Init();
            return uiList;
        }

        public abstract void InitInstance();
    }

}

