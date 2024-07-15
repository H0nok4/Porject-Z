using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoSingleton<Logger>
{
    public static bool Logable = false;

    private static Logger instance;

    public static Logger Instance {
        get {
            if (!Logable)
            {
                return null;
            }

            if (instance == null) {
                instance = FindObjectOfType<Logger>();

                if (instance == null) {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<Logger>();
                    singletonObject.name = typeof(Logger).ToString();
                    DontDestroyOnLoad(singletonObject);

                }
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = this as Logger;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void Log(string message)
    {
        if (!Logable)
        {
            return;
        }
        Debug.Log(message);
    }

    public void LogWarning(string message)
    {
        if (!Logable) {
            return;
        }
        Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        if (!Logable) {
            return;
        }
        Debug.LogError(message);
    }
}

