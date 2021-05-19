using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    #region// singleton
    private static T instance;

    /// <summary>
    /// シングルトン
    /// </summary>
    public static T Instance => instance = instance
        ?? FindObjectOfType<T>() 
        ?? new GameObject(typeof(T).Name).AddComponent<T>();

    protected bool checkInstance()
    {
        if (Instance != this)
        {
            Destroy(this);
            return false;
        }
        DontDestroyOnLoad(this);
        return true;
    }
    #endregion

    protected virtual void Awake()
    {
        checkInstance();
    }
}
