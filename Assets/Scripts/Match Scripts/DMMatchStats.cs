using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMatchStats : MonoBehaviour
{
    public static DMMatchStats instance;

    public Dictionary<int, PlayerStats> playerStats;

    [HideInInspector]
    public List<PlayerData> matchPlayers;



    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        playerStats = new Dictionary<int, PlayerStats>();
        matchPlayers = new List<PlayerData>();
    }



    public void SetupPlayerStats()
    {
        playerStats.Clear();
        matchPlayers.Clear();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            PlayerData player = players[i].GetComponent<PlayerData>();
            matchPlayers.Add(player);
        }
    }

    public void UpdatePlayerStats()
    {
        foreach (PlayerData player in matchPlayers)
        {
            //Add players to the dictionary if they don't exist in it yet
            PlayerStats temp;
            if (!playerStats.TryGetValue(player.GetPlayerNum(), out temp))
            {
                PlayerStats newPs = new PlayerStats();
                playerStats.Add(player.GetPlayerNum(), newPs);
            }

            //Update their stats in the dictionary
            PlayerStats pStats = playerStats[player.GetPlayerNum()];
            pStats.kills += player.GetKills();
            pStats.deaths += player.GetDeaths();
            playerStats[player.GetPlayerNum()] = pStats;
        }
    }
}



public struct PlayerStats
{
    public int kills;
    public int deaths;
}