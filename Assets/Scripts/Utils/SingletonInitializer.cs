using UnityEngine;

public abstract class SingletonInitializer<T> where T : class, IInitializer, new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
                _instance.Initialize();
                return _instance;
            }
            else
            {
                return _instance;
            }
        }
    }
}
