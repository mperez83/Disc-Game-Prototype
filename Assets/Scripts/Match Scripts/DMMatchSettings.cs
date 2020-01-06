using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMatchSettings : MonoBehaviour
{
    public static DMMatchSettings instance;

    int numberOfPlayers = 4;
    int playerHealth = 100;
    bool bouncyPlayers = true;
    float speedMultiplier = 1;
    float dragMultiplier = 1;
    float sizeMultiplier = 1;
    float knockbackMultiplier = 1;
    float matchTimer = 90;

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

    public void SetPlayerHealth(int temp) { playerHealth = temp; }
    public int GetPlayerHealth() { return playerHealth; }

    public void SetBouncyPlayers(bool temp) { bouncyPlayers = temp; }
    public bool GetBouncyPlayers() { return bouncyPlayers; }

    public void SetSpeedMultiplier(float temp) { speedMultiplier = temp; }
    public float GetSpeedMultiplier() { return speedMultiplier; }

    public void SetDragMultiplier(float temp) { dragMultiplier = temp; }
    public float GetDragMultiplier() { return dragMultiplier; }

    public void SetSizeMultiplier(float temp) { sizeMultiplier = temp; }
    public float GetSizeMultiplier() { return sizeMultiplier; }

    public void SetKnockbackMultiplier(float temp) { knockbackMultiplier = temp; }
    public float GetKnockbackMultiplier() { return knockbackMultiplier; }

    public void SetMatchTimer(float temp) { matchTimer = temp; }
    public float GetMatchTimer() { return matchTimer; }
}