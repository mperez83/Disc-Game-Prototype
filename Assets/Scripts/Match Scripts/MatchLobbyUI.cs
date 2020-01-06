using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MatchLobbyUI : MonoBehaviour
{
    public Slider numberOfPlayersSlider;
    public Slider playerHealthSlider;
    public Toggle bouncyPlayersToggle;
    public Slider speedMultSlider;
    public Slider dragMultSlider;
    public Slider playerSizeSlider;
    public Slider cameraShakeSlider;
    public Slider knockbackMultSlider;
    public Slider matchTimerSlider;

    void Start()
    {
        numberOfPlayersSlider.value = DMMatchSettings.instance.GetNumberOfPlayers();
        playerHealthSlider.value = DMMatchSettings.instance.GetPlayerHealth();
        bouncyPlayersToggle.isOn = DMMatchSettings.instance.GetBouncyPlayers();
        speedMultSlider.value = DMMatchSettings.instance.GetSpeedMultiplier();
        dragMultSlider.value = DMMatchSettings.instance.GetDragMultiplier();
        playerSizeSlider.value = DMMatchSettings.instance.GetSizeMultiplier();
        cameraShakeSlider.value = GameManager.instance.cameraShakeModifier;
        knockbackMultSlider.value = DMMatchSettings.instance.GetKnockbackMultiplier();
        matchTimerSlider.value = DMMatchSettings.instance.GetMatchTimer();
    }

    public void ChangeNumberOfPlayers(Slider slider)
    {
        DMMatchSettings.instance.SetNumberOfPlayers((int)slider.value);
    }

    public void ChangePlayerHealth(Slider slider)
    {
        DMMatchSettings.instance.SetPlayerHealth((int)slider.value);
    }

    public void ToggleBouncyPlayers(Toggle toggle)
    {
        DMMatchSettings.instance.SetBouncyPlayers(toggle.isOn);
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

    public void ChangeCameraShakeMultiplier(Slider slider)
    {
        GameManager.instance.cameraShakeModifier = slider.value;
    }

    public void ChangeKnockbackMultiplier(Slider slider)
    {
        DMMatchSettings.instance.SetKnockbackMultiplier(slider.value);
    }

    public void ChangeMatchTimer(Slider slider)
    {
        DMMatchSettings.instance.SetMatchTimer(slider.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Deathmatch");
    }
}