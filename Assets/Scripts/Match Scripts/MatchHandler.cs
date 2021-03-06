﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler instance;

    public float matchTimerLength;
    float matchTimer;
    public Image timerBar;
    public CinemachineTargetGroup cmTargetGroup;

    public GameObject playerPrefab;
    public Transform corpseContainer;
    public Transform initialSpawnContainer;
    public Transform respawnPointContainer;



    void Awake()
    {
        instance = this;

        matchTimerLength = DMMatchSettings.instance.GetMatchTimer();
        matchTimer = matchTimerLength;

        int curSpawnPoint = 0;
        for (int i = 0; i < DMMatchSettings.instance.GetNumberOfPlayers(); i++)
        {
            Vector2 spawnPos = initialSpawnContainer.GetChild(curSpawnPoint).position;
            PlayerData newPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity).GetComponent<PlayerData>();
            newPlayer.SetPlayerNum(i + 1);

            //Apply player health setting
            newPlayer.maxHealth = DMMatchSettings.instance.GetPlayerHealth();
            
            //Apply bouncy players setting
            Rigidbody2D playerRB = newPlayer.GetComponent<Rigidbody2D>();
            PhysicsMaterial2D newMat = new PhysicsMaterial2D();
            newMat.friction = 0;
            newMat.bounciness = (DMMatchSettings.instance.GetBouncyPlayers()) ? 1 : 0;
            playerRB.sharedMaterial = newMat;

            //Apply speed multiplier setting
            PlayerBoost playerBoost = newPlayer.GetComponent<PlayerBoost>();
            playerBoost.boostPower *= DMMatchSettings.instance.GetSpeedMultiplier();

            //Apply drag multiplier setting
            playerRB.drag *= DMMatchSettings.instance.GetDragMultiplier();

            //Apply size multiplier setting
            newPlayer.transform.localScale *= DMMatchSettings.instance.GetSizeMultiplier();

            //Apply knockback multiplier setting
            PlayerMovement playerMovement = newPlayer.GetComponent<PlayerMovement>();
            playerMovement.SetKnockbackMultiplier(DMMatchSettings.instance.GetKnockbackMultiplier());

            //Add the player to the cinemachine
            cmTargetGroup.AddMember(newPlayer.transform, 1, 0);

            curSpawnPoint++;
            if (curSpawnPoint >= initialSpawnContainer.childCount) curSpawnPoint = 0;
        }

        DMMatchStats.instance.SetupPlayerStats();
    }

    void Update()
    {
        //Actual match timer
        matchTimer -= Time.deltaTime;
        if (matchTimer <= 0)
        {
            EndMatch();
        }

        //Timer bar fill amount
        timerBar.fillAmount = matchTimer / matchTimerLength;

        //Debug reset key
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Deathmatch");
        }
    }



    //At the end of a match, we take all of the data from the players in the scene and update their respective PlayerStats inside DMMatchStats
    public void EndMatch()
    {
        DMMatchStats.instance.UpdatePlayerStats();
        SceneManager.LoadScene("MatchResults");
    }
}