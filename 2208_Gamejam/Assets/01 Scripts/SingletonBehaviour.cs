using UnityEngine;

public abstract class SingletonBehavior<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _inst;
    public static T Instance
    {
        get
        {
            if (_inst == null)
                _inst = FindObjectOfType<T>();

            return _inst;
        }
    }

    private void Awake()
    {
        if (_inst == null)
        {
            _inst = GetComponent<T>();
        }
        else if (_inst != this)
        {
            Destroy(gameObject);
            return;
        }

        OnAwake();
    }

    protected abstract void OnAwake();
}