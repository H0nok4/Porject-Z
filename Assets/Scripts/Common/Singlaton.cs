

public class Singleton<T> where T : class, new()
{
    private static readonly object lockObject = new object();
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                lock (lockObject) {
                    if (_instance == null) {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }
    }

    protected Singleton()
    {
        OnCreateInstance();
    }

    public virtual void OnCreateInstance()
    {
        
    }
}