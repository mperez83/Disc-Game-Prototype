using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchLobbyUI : MonoBehaviour
{
    public void ChangeNumberOfPlayers(Slider slider)
    {
        DMMatchSettings.instance.SetNumberOfPlayers((int)slider.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Deathmatch");
    }
}