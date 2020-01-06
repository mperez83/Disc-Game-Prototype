using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchLobbyUI : MonoBehaviour
{
    public Slider numberOfPlayersSlider;
    public Slider speedMultSlider;
    public Slider dragMultSlider;
    public Slider playerSizeSlider;

    void Start()
    {
        numberOfPlayersSlider.value = DMMatchSettings.instance.GetNumberOfPlayers();
        speedMultSlider.value = DMMatchSettings.instance.GetSpeedMultiplier();
        dragMultSlider.value = DMMatchSettings.instance.GetDragMultiplier();
        playerSizeSlider.value = DMMatchSettings.instance.GetSizeMultiplier();
    }

    public void ChangeNumberOfPlayers(Slider slider)
    {
        DMMatchSettings.instance.SetNumberOfPlayers((int)slider.value);
    }

    public void ChangeSpeedMultiplier(Slider slider)
    {
        DMMatchSettings.instance.SetSpeedMultiplier(slider.value);
    }

    public void ChangeDragMultiplier(Slider slider)
    {
        DMMatchSettings.instance.SetDragMultiplier(slider.value);
    }

    public void ChangeSizeMultiplier(Slider slider)
    {
        DMMatchSettings.instance.SetSizeMultiplier(slider.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Deathmatch");
    }
}