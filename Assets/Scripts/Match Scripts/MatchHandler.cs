using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchHandler : MonoBehaviour
{
    public static MatchHandler instance;

    public float matchTimerLength;
    float matchTimer;
    public Image timerBar;



    void Awake()
    {
        instance = this;
        matchTimer = matchTimerLength;
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
        SceneManager.LoadScene("Results");
    }
}