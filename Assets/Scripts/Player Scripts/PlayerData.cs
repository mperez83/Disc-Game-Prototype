using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public int health;
    int maxHealth;
    public Image healthbarImage;

    public int playerNum;
    public Color[] playerColors;

    SpriteRenderer sr;
    PlayerMovement playerMovement;
    GameObject weapon;

    public Transform respawnPointContainer;

    public GameObject corpsePrefab;
    public Transform corpseContainer;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        //Set color
        if (playerNum >= 1 && playerNum <= 4) sr.color = playerColors[playerNum - 1];
        else sr.color = Color.gray;

        maxHealth = health;
    }



    public void GiveWeapon(GameObject newWeapon)
    {
        if (weapon) Destroy(weapon.gameObject);
        weapon = newWeapon;

        //Fix position of weapon
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;
    }

    public void TakeDamage(int damage, float damageAngle, float damageForce)
    {
        //Subtract from health
        health -= damage;

        //Death
        if (health <= 0)
        {
            GameObject newCorpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity);
            newCorpse.GetComponent<Rigidbody2D>().AddForce(TrigUtilities.DegreesToVector(damageAngle) * damageForce * Time.deltaTime, ForceMode2D.Impulse);
            LeanTween.delayedCall(gameObject, 2, () => {
                Respawn();
            });
            gameObject.SetActive(false);
        }

        //Normal damage
        else
        {
            playerMovement.ApplyForce(damageAngle, damageForce);
            healthbarImage.fillAmount = ((float)health / maxHealth);
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);
        health = 100;
        healthbarImage.fillAmount = ((float)health / maxHealth);

        int randomChildIndex = Random.Range(0, respawnPointContainer.childCount);
        transform.position = respawnPointContainer.GetChild(randomChildIndex).position;
    }



    //Getters/Setters
    public int GetPlayerNum() { return playerNum; }
    public void SetPlayerNum(int num) { playerNum = num; }

    public GameObject GetWeapon() { return weapon; }
    public PlayerMovement GetPlayerMovement() { return playerMovement; }
    public SpriteRenderer GetSpriteRenderer() { return sr; }
}