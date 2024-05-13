using System;
using UnityEngine;

namespace EventSystem
{
    public abstract class EventArgs {

    }

    public abstract class EventArgsWithParam : EventArgs {
        protected object[] Params;

        public T GetParam<T>(int idx) {
            if (Params == null || Params.Length <= idx) {
                return default;
            }

            if (Params[idx] is not T) {
                Debug.LogError($"类型转换错误, idx: {idx}, Type: {typeof(T)}");
                return default;
            }

            return (T)Params[idx];
        }
    }

    public static class EventDispatcher {
        private static readonly EventInternal EventInternal = new();

        public static void RegEventListener(string eventType, Action handler) {
            EventInternal.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T>(string eventType, Action<T> handler) {
            EventInternal.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T1, T2>(string eventType, Action<T1, T2> handler) {
            EventInternal.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler) {
            EventInternal.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler) {
            EventInternal.RegEventListener(eventType, handler);
        }

        public static void UnRegEventListener(string eventType, Action handler) {
            EventInternal.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T>(string eventType, Action<T> handler) {
            EventInternal.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T1, T2>(string eventType, Action<T1, T2> handler) {
            EventInternal.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler) {
            EventInternal.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler) {
            EventInternal.UnRegEventListener(eventType, handler);
        }

        public static void TriggerEvent(string eventType) {
            EventInternal.TriggerEvent(eventType);
        }

        public static void TriggerEvent<T>(string eventType, T arg1) {
            EventInternal.TriggerEvent(eventType, arg1);
        }

        public static void TriggerEvent<T1, T2>(string eventType, T1 arg1, T2 arg2) {
            EventInternal.TriggerEvent(eventType, arg1, arg2);
        }

        public static void TriggerEvent<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3) {
            EventInternal.TriggerEvent(eventType, arg1, arg2, arg3);
        }

        public static void TriggerEvent<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            EventInternal.TriggerEvent(eventType, arg1, arg2, arg3, arg4);
        }

        #region 参数过多时使用如下包装

        public static void RegEventListener<TEventArgs>(Action<TEventArgs> action) where TEventArgs : EventArgs {
            RegEventListener(typeof(TEventArgs).FullName, action);
        }

        public static void UnRegEventListener<TEventArgs>(Action<TEventArgs> action) where TEventArgs : EventArgs {
            UnRegEventListener(typeof(TEventArgs).FullName, action);
        }

        public static void TriggerEvent<TEventArgs>(TEventArgs args) where TEventArgs : EventArgs {
            TriggerEvent(typeof(TEventArgs).FullName, args);
        }

        #endregion
    }
}
