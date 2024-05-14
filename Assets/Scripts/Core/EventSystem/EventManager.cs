using System;
using UnityEngine;

namespace EventSystem
{
    public abstract class EventArgs {

    }

    public abstract class EventArgsWithParam : EventArgs {
        protected object[] Params;

        public T GetParam<T>(int index) {
            if (Params == null || Params.Length <= index) {
                return default;
            }

            if (Params[index] is not T) {
                Debug.LogError($"类型转换错误, index: {index}, Type: {typeof(T)}");
                return default;
            }

            return (T)Params[index];
        }
    }

    public static class EventManager {
        private static readonly EventCore _eventCore = new();

        public static void RegEventListener(string eventType, Action handler) {
            _eventCore.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T>(string eventType, Action<T> handler) {
            _eventCore.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T1, T2>(string eventType, Action<T1, T2> handler) {
            _eventCore.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler) {
            _eventCore.RegEventListener(eventType, handler);
        }

        public static void RegEventListener<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler) {
            _eventCore.RegEventListener(eventType, handler);
        }

        public static void UnRegEventListener(string eventType, Action handler) {
            _eventCore.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T>(string eventType, Action<T> handler) {
            _eventCore.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T1, T2>(string eventType, Action<T1, T2> handler) {
            _eventCore.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler) {
            _eventCore.UnRegEventListener(eventType, handler);
        }

        public static void UnRegEventListener<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler) {
            _eventCore.UnRegEventListener(eventType, handler);
        }

        public static void TriggerEvent(string eventType) {
            _eventCore.TriggerEvent(eventType);
        }

        public static void TriggerEvent<T>(string eventType, T arg1) {
            _eventCore.TriggerEvent(eventType, arg1);
        }

        public static void TriggerEvent<T1, T2>(string eventType, T1 arg1, T2 arg2) {
            _eventCore.TriggerEvent(eventType, arg1, arg2);
        }

        public static void TriggerEvent<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3) {
            _eventCore.TriggerEvent(eventType, arg1, arg2, arg3);
        }

        public static void TriggerEvent<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            _eventCore.TriggerEvent(eventType, arg1, arg2, arg3, arg4);
        }

    }
}
