using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargeMatchHandler : MonoBehaviour
{
    public static ChargeMatchHandler instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Charger Mode");
        }
    }

    public void EndMatch()
    {
        SceneManager.LoadScene("Charger Mode");
    }
}