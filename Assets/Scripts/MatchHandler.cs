using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public GameObject playerPrefab;
    public int tempHowManyPlayers;
    public Transform[] spawnPoints;
    public Color[] playerColors;

    void Start()
    {
        int currentSpawnPoint = 0;

        for (int i = 0; i < tempHowManyPlayers; i++)
        {
            //Spawn and select the player's position
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoints[currentSpawnPoint].position, Quaternion.identity);
            currentSpawnPoint = (currentSpawnPoint < (spawnPoints.Length - 1)) ? currentSpawnPoint + 1 : 0;

            //Set the player colors
            if (i >= 0 && i <= 3) newPlayer.GetComponent<SpriteRenderer>().color = playerColors[i];
            else newPlayer.GetComponent<SpriteRenderer>().color = Color.white;

            //Set player data
            newPlayer.GetComponent<RigidbodyMovement>().SetPlayerNum(i + 1);
        }
    }

    void Update()
    {
        
    }
}