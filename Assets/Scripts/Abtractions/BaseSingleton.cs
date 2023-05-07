using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : BaseSingleton<T>
{
    private static T instance;

    private void Awake()
    {
        CreateInstance();
    }

    public static T GetInstance()
    {
        if (instance == null)
        {
            CreateInstance();
        }
        return instance;
    }

    private static void CreateInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<T>();
        }
        else if (instance != FindObjectOfType<T>())
        {
            Destroy(FindObjectOfType<T>());
        }
    }

}
