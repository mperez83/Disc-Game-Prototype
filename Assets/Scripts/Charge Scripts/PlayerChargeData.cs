using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeData : MonoBehaviour
{
    int score;



    void Start()
    {
        
    }

    void Update()
    {
        
    }



    public void GainCharge(int amount)
    {
        score += amount;
    }
}