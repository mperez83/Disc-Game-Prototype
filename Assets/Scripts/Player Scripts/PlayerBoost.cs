using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerBoost : MonoBehaviour
{
    public float boostPower;
    bool boosting;

    PlayerMovement playerMovement;
    PlayerData playerData;



    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerData = GetComponent<PlayerData>();
    }

    void Update()
    {
        if (Input.GetButton("P" + playerData.GetPlayerNum() + "_Action"))
        {
            boosting = true;
        }
        else
        {
            boosting = false;
        }
    }

    void FixedUpdate()
    {
        if (boosting)
        {
            playerMovement.ApplyForceForward(boostPower);
        }
    }
}