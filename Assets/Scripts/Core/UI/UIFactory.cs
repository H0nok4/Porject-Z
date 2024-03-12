using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    public class UIFactory : MonoSingleton<UIFactory> {
        private Dictionary<Type, GameObject> UISet = new Dictionary<Type, GameObject>();

        public UIComponent CreateUIComponent<T>() where T : UIComponent {
            if (!UISet.ContainsKey(typeof(T))) {
                return null;
            }

            var component = GameObject.Instantiate(UISet[typeof(T)]).GetComponent<UIComponent>();
            component.Init();
            return component;
        }
    }

}
