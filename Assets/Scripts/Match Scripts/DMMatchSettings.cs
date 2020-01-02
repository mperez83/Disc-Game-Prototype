using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMatchSettings : MonoBehaviour
{
    public static DMMatchSettings instance;

    int numberOfPlayers = 1;

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

    public void SetNumberOfPlayers(int temp) { numberOfPlayers = temp; }
    public int GetNumberOfPlayers() { return numberOfPlayers; }
}