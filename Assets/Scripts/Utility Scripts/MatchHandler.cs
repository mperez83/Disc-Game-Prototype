using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public GameObject playerPrefab;
    public int tempHowManyPlayers;
    public Transform[] spawnPoints;

    void Start()
    {
        int currentSpawnPoint = 0;

        for (int i = 0; i < tempHowManyPlayers; i++)
        {
            //Spawn and set the player's position
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoints[currentSpawnPoint].position, Quaternion.identity);
            currentSpawnPoint = (currentSpawnPoint < (spawnPoints.Length - 1)) ? currentSpawnPoint + 1 : 0;

            //Set player data
            newPlayer.GetComponent<PlayerData>().SetPlayerNum(i + 1);
        }
    }
}