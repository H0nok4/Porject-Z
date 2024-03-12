using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class UIList : UIComponent {
        public List<UIComponent> ChildList = new List<UIComponent>();
        private GameObject _contentGameObject;
        private GridLayoutGroup _contentLayoutGroup;

        [Header("列表使用的子物体（临时）")]
        public GameObject ListChildrenInstance;

        public int ChildCount {
            get => ChildList.Count;
            set
            {
                if (ChildList.Count == value)
                {
                    return;
                }

                if (ChildCount > value) {
                    int num = ChildCount - value;
                    for (int i = 0; i < num; i++) {
                        RemoveChildAt(ChildCount - 1);
                    }
                }
                else {
                    int num = value - ChildCount;
                    for (int i = 0; i < num; i++) {
                        AddChild();
                    }
                }
            }
        }

        public void AddChild(UIComponent child) {
            ChildList.Add(child);
            //TODO:需要加到Context物体下
            child.transform.SetParent(_contentGameObject.transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.localScale = Vector3.one;
        }

        public void AddChild() {
            if (ListChildrenInstance == null) {
                return;
            }

            var component = GameObject.Instantiate(ListChildrenInstance).GetComponent<UIComponent>();
            component.Init();
            AddChild(component);
        }

        public void AddChildAt(UIComponent child, int index) {
            ChildList.Insert(index, child);
        }

        public void AddChildAt(int index) {
            if (ListChildrenInstance == null) {
                return;
            }
            var component = GameObject.Instantiate(ListChildrenInstance).GetComponent<UIComponent>();
            ChildList.Insert(index, component);
            component.transform.SetParent(_contentGameObject.transform);
        }

        public void RemoveChild(UIComponent child) {
            var removed = ChildList.Remove(child);
            child.transform.SetParent(null);
            //TODO:需要投入到对象池中，或者直接销毁,这里先直接销毁
            if (removed) {
                Destroy(child.gameObject);
            }
        }

        public void RemoveChildAt(int index) {
            var child = ChildList[index];
            RemoveChild(child);
        }

        public void RemoveAllChild() {
            foreach (var uiComponent in ChildList) {
                RemoveChild(uiComponent);
            }
        }

        public override void InitInstance() {
            _contentGameObject = transform.GetChild(0).GetChild(0).gameObject;
            _contentLayoutGroup = _contentGameObject.GetComponent<GridLayoutGroup>();
            if (_contentLayoutGroup is null) {
                //TODO:后面要可以选择附加哪种排列组件
            }
            //TODO:清空一下现有的Child
            var childCount = _contentGameObject.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--) {
                var child = _contentGameObject.transform.GetChild(i).gameObject;
                child.transform.SetParent(null);
                Destroy(child);
            }

        }
    }

}
