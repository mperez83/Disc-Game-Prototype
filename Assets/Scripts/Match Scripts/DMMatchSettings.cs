using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMatchSettings : MonoBehaviour
{
    public static DMMatchSettings instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
}