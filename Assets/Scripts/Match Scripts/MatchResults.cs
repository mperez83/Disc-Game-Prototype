using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatchResults : MonoBehaviour
{
    List<int> playerNums;
    public RectTransform statsContainer;
    public GameObject resultStatsPrefab;

    void Start()
    {
        playerNums = new List<int>(DMMatchStats.instance.playerStats.Keys);
        for (int i = 0; i < playerNums.Count; i++)
        {
            TextMeshProUGUI statsText = Instantiate(resultStatsPrefab, statsContainer).GetComponent<TextMeshProUGUI>();
            statsText.text = "Player " + playerNums[i] + ": " + DMMatchStats.instance.playerStats[playerNums[i]].kills.ToString();
        }
    }
}