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

    public void EndMatch()
    {
        SceneManager.LoadScene("Results");
    }
}