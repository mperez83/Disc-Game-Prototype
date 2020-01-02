using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    //Player num stuff
    int playerNum;
    public Color[] playerColors;

    //Health stuff
    public int maxHealth;
    int health;
    public Image healthbarImage;
    public float respawnTime;

    //Score stuff
    int kills;
    int deaths;
    public GameObject textObjectPrefab;

    //References
    SpriteRenderer sr;
    PlayerMovement playerMovement;
    GameObject weapon;
    public AudioSource playerHurtAS;

    //Respawn stuff
    public GameObject respawnWeapon;
    public float spawnInvincDuration;
    bool invincible;

    //Corpse stuff
    public GameObject corpsePrefab;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        //Set color
        if (playerNum >= 1 && playerNum <= 4) sr.color = playerColors[playerNum - 1];
        else
        {
            Debug.LogError("Too many players tried to spawn!!");
            Destroy(gameObject);
            return;
        }

        health = maxHealth;
        if (respawnWeapon) GiveWeapon(Instantiate(respawnWeapon, transform.position, Quaternion.identity));
    }



    public void GiveWeapon(GameObject newWeapon)
    {
        if (weapon) Destroy(weapon);
        weapon = newWeapon;

        //Fix position of weapon
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;

        //If this is a gun, fix the angle
        if (newWeapon.GetComponent<GunBase>())
        {
            newWeapon.transform.localEulerAngles = new Vector3(0, 90, -90);
        }

        //Disable pickup script stuff
        newWeapon.GetComponent<BoxCollider2D>().enabled = false;
        newWeapon.GetComponent<WeaponPickup>().enabled = false;
    }

    public void TakeDamage(int damage, float damageAngle, float damageForce, PlayerData damageSource)
    {
        if (health > 0 && !invincible)
        {
            health -= damage;
            CMShakeHandler.instance.IncreaseShakeAmount(0.004f * damage);

            //Death
            if (health <= 0)
            {
                if (damageSource != this) damageSource.IncrementKills();
                deaths++;

                CMShakeHandler.instance.IncreaseShakeAmount(0.5f);

                //Spawn corpse
                GameObject newCorpse = Instantiate(corpsePrefab, transform.position, Quaternion.identity);
                newCorpse.GetComponent<Rigidbody2D>().AddForce(TrigUtilities.DegreesToVector(damageAngle) * damageForce * Time.deltaTime, ForceMode2D.Impulse);

                //Remove weapon
                if (weapon) Destroy(weapon.gameObject);

                //Remove player temporarily
                LeanTween.delayedCall(gameObject, respawnTime, () => {
                    Respawn();
                });
                gameObject.SetActive(false);
            }

            //Normal damage
            else
            {
                playerMovement.ApplyForce(damageAngle, damageForce);
                healthbarImage.fillAmount = ((float)health / maxHealth);
                playerHurtAS.PlayRandomize(0.25f);
            }
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);

        health = 100;
        healthbarImage.fillAmount = ((float)health / maxHealth);
        StartCoroutine(GiveIFrames());

        int randomChildIndex = Random.Range(0, MatchHandler.instance.respawnPointContainer.childCount);
        transform.position = MatchHandler.instance.respawnPointContainer.GetChild(randomChildIndex).position;

        if (respawnWeapon) GiveWeapon(Instantiate(respawnWeapon, transform.position, Quaternion.identity));

        //Generate respawn apparition
        GameObject spawnApparition = new GameObject("Player Spawn Apparition", typeof(SpriteRenderer));
        spawnApparition.transform.position = transform.position;

        SpriteRenderer apparitionSR = spawnApparition.GetComponent<SpriteRenderer>();
        apparitionSR.sprite = sr.sprite;
        apparitionSR.color = sr.color;

        LeanTween.scale(spawnApparition, spawnApparition.transform.localScale * 6, 0.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.alpha(spawnApparition, 0, 0.5f).setOnComplete(() =>
        {
            Destroy(spawnApparition);
        });
    }

    IEnumerator GiveIFrames()
    {
        invincible = true;
        float invincDuration = spawnInvincDuration;

        while (invincDuration > 0)
        {
            invincDuration -= Time.deltaTime;
            sr.enabled = !sr.enabled;
            yield return null;
        }

        sr.enabled = true;
        invincible = false;
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