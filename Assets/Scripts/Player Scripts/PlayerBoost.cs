using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerBoost : MonoBehaviour
{
    public float boostPower;
    bool boosting;

    PlayerData playerData;
    PlayerMovement playerMovement;



    void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerMovement = GetComponent<PlayerMovement>();
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