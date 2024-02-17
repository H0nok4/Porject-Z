using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<T>();

                if (instance == null) {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString();
                    DontDestroyOnLoad(singletonObject);
                    
                }
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

}