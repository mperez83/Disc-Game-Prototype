using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeData : MonoBehaviour
{
    int score;
    public int winScore;



    void Start()
    {
        
    }

    void Update()
    {
        
    }



    public void GainCharge(int amount)
    {
        score += amount;
        if (score >= winScore)
        {
            ChargeMatchHandler.instance.EndMatch();
        }
    }
}