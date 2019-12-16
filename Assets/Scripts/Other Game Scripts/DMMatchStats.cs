using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMatchStats : MonoBehaviour
{
    public static DMMatchStats instance;

    public List<PlayerData> matchPlayers;

    void Start()
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

    void Update()
    {
        
    }
}