using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMatchSettings : MonoBehaviour
{
    public static DMMatchSettings instance;

    int numberOfPlayers = 1;
    float speedMultiplier = 1;
    float dragMultiplier = 1;
    float sizeMultiplier = 1;

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

    public void SetSpeedMultiplier(float temp) { speedMultiplier = temp; }
    public float GetSpeedMultiplier() { return speedMultiplier; }

    public void SetDragMultiplier(float temp) { dragMultiplier = temp; }
    public float GetDragMultiplier() { return dragMultiplier; }

    public void SetSizeMultiplier(float temp) { sizeMultiplier = temp; }
    public float GetSizeMultiplier() { return sizeMultiplier; }
}