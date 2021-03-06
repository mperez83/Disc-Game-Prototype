﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    //Player num stuff
    int playerNum;
    public Color[] playerColors;
    Color color;

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
    public MeshRenderer meshRenderer;
    Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();

        //Set color
        if (playerNum >= 1 && playerNum <= 4)
        {
            color = playerColors[playerNum - 1];
            
            Material[] tempMaterialsArray = new Material[meshRenderer.materials.Length];
            System.Array.Copy(meshRenderer.materials, tempMaterialsArray, meshRenderer.materials.Length);

            Material newMat = new Material(meshRenderer.materials[1]);
            newMat.color = color;

            tempMaterialsArray[1] = newMat;
            meshRenderer.materials = tempMaterialsArray;
        }
        else
        {
            Debug.LogError("Too many players tried to spawn!! (playerNum was " + playerNum + ")");
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
        weapon.transform.localPosition = new Vector3(0, 0, -0.5f);
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
                newCorpse.transform.localScale = transform.localScale;

                Rigidbody2D corpseRB = newCorpse.GetComponent<Rigidbody2D>();
                corpseRB.AddForce(TrigUtilities.DegreesToVector(damageAngle) * damageForce * Time.deltaTime, ForceMode2D.Impulse);
                corpseRB.drag = rb.drag;
                corpseRB.sharedMaterial = rb.sharedMaterial;

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
                playerMovement.ApplyKnockback(damageAngle, damageForce);
                healthbarImage.fillAmount = ((float)health / maxHealth);
                playerHurtAS.PlayRandomize(0.25f);
            }
        }
    }

    void Respawn()
    {
        gameObject.SetActive(true);

        health = maxHealth;
        healthbarImage.fillAmount = ((float)health / maxHealth);
        StartCoroutine(GiveIFrames());

        int randomChildIndex = Random.Range(0, MatchHandler.instance.respawnPointContainer.childCount);
        transform.position = MatchHandler.instance.respawnPointContainer.GetChild(randomChildIndex).position;

        if (respawnWeapon) GiveWeapon(Instantiate(respawnWeapon, transform.position, Quaternion.identity));

        //Generate respawn apparition
        GameObject spawnApparition = new GameObject("Player Spawn Apparition", typeof(SpriteRenderer));
        spawnApparition.transform.position = transform.position;

        SpriteRenderer apparitionSR = spawnApparition.GetComponent<SpriteRenderer>();
        //apparitionSR.sprite = sr.sprite;
        //apparitionSR.color = sr.color;

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
            //sr.enabled = !sr.enabled;
            yield return null;
        }

        //sr.enabled = true;
        invincible = false;
    }



    //Getters/Setters
    public int GetPlayerNum() { return playerNum; }
    public void SetPlayerNum(int num) { playerNum = num; }

    public Color GetColor() { return color; }

    public int GetKills() { return kills; }
    public void IncrementKills()
    {
        kills++;
        TextFadeObject killText = Instantiate(textObjectPrefab, transform.position, Quaternion.identity).GetComponent<TextFadeObject>();
        killText.SetText(kills.ToString() + "!");
        killText.SetColor(color);
    }

    public int GetDeaths() { return deaths; }

    public GameObject GetWeapon() { return weapon; }
    public PlayerMovement GetPlayerMovement() { return playerMovement; }
}