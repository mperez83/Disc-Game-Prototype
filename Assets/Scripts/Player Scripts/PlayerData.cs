using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    //Player num stuff
    public int playerNum;
    public Color[] playerColors;

    //Health stuff
    public int maxHealth;
    int health;
    public Image healthbarImage;

    //Score stuff
    int kills;
    int deaths;
    public GameObject textObjectPrefab;

    //References
    SpriteRenderer sr;
    PlayerMovement playerMovement;
    GameObject weapon;

    //Respawn stuff
    public Transform respawnPointContainer;

    //Corpse stuff
    public GameObject corpsePrefab;
    public Transform corpseContainer;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        //Set color
        if (playerNum >= 1 && playerNum <= 4) sr.color = playerColors[playerNum - 1];
        else
        {
            sr.color = Color.gray;
            Destroy(GetComponent<PlayerBoost>());
            Destroy(GetComponent<PlayerBrake>());
            Destroy(GetComponent<PlayerMovement>());
        }

        health = maxHealth;
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

    public void TakeDamage(int damage, float damageAngle, float damageForce, PlayerData damageSource)
    {
        if (health > 0)
        {
            health -= damage;
            CameraShakeHandler.instance.IncreaseShakeAmount(0.004f * damage);

            //Death
            if (health <= 0)
            {
                if (damageSource != this) damageSource.IncrementKills();
                deaths++;

                //Spawn corpse
                GameObject newCorpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity);
                newCorpse.GetComponent<Rigidbody2D>().AddForce(TrigUtilities.DegreesToVector(damageAngle) * damageForce * Time.deltaTime, ForceMode2D.Impulse);

                //Remove weapon
                if (weapon) Destroy(weapon.gameObject);

                //Remove player temporarily
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

    public int GetKills() { return kills; }
    public void IncrementKills()
    {
        kills++;
        TextFadeObject killText = Instantiate(textObjectPrefab, transform.position, Quaternion.identity).GetComponent<TextFadeObject>();
        killText.SetText(kills.ToString() + "!");
        killText.SetColor(sr.color);
    }

    public int GetDeaths() { return deaths; }

    public GameObject GetWeapon() { return weapon; }
    public PlayerMovement GetPlayerMovement() { return playerMovement; }
    public SpriteRenderer GetSpriteRenderer() { return sr; }
}