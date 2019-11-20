using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    int playerNum;
    int health = 25;

    float playerAngle;
    WeaponBase weapon;

    public Color[] playerColors;



    void Start()
    {
        //Set color
        if (playerNum >= 1 && playerNum <= 4) GetComponent<SpriteRenderer>().color = playerColors[playerNum - 1];
        else GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public void GiveWeapon(GameObject newWeapon)
    {
        if (transform.GetChild(0)) Destroy(transform.GetChild(0).gameObject);
        GameObject temp = Instantiate(newWeapon, transform);
        weapon = temp.GetComponent<WeaponBase>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    //Getters/Setters
    public int GetPlayerNum() { return playerNum; }
    public void SetPlayerNum(int num) { playerNum = num; }

    public float GetPlayerAngle() { return playerAngle; }
    public void SetPlayerAngle(float angle) { playerAngle = angle; }

    public WeaponBase GetWeapon() { return weapon; }

}