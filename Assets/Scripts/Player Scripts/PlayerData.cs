using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int health;
    int maxHealth;
    public Image healthbarImage;

    public int playerNum;
    float playerAngle;

    SpriteRenderer sr;
    PlayerMovement playerMovement;
    WeaponBase weapon;

    public SpriteRenderer directionIndicator;

    public Color[] playerColors;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        //Set color
        if (playerNum >= 1 && playerNum <= 4) sr.color = playerColors[playerNum - 1];
        else sr.color = Color.gray;

        directionIndicator.color = Color.yellow;

        maxHealth = health;
    }

    void Update()
    {
        //Update rotation based on player input
        Vector2 axis = new Vector2(Input.GetAxisRaw("P" + playerNum + "_Joystick_L_Horizontal"), Input.GetAxisRaw("P" + playerNum + "_Joystick_L_Vertical"));
        if (axis != Vector2.zero && axis.magnitude > 0.5f)
        {
            playerAngle = TrigUtilities.VectorToDegrees(axis);
            transform.rotation = Quaternion.AngleAxis(-playerAngle, Vector3.forward);
        }

        //Handle input for if the player doesn't have a weapon
        if (Input.GetButtonDown("P" + playerNum + "_Fire"))
        {
            if (!weapon) playerMovement.ApplyForce(playerAngle, 1f);
        }
    }



    public void GiveWeapon(GameObject newWeapon)
    {
        if (weapon) Destroy(weapon.gameObject);
        weapon = newWeapon.GetComponent<WeaponBase>();

        //Fix position of weapon
        newWeapon.transform.parent = transform;
        Vector2 playerDirection = TrigUtilities.DegreesToVector(playerAngle);
        //newWeapon.transform.position = transform.position + new Vector3(playerDirection.x, playerDirection.y, 0);
        newWeapon.transform.position = transform.position;
    }

    public void TakeDamage(int damage, float damageAngle, float damageForce)
    {
        //Subtract from health
        health -= damage;

        //Death
        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Normal damage
        else
        {
            playerMovement.ApplyForce(damageAngle, damageForce);
            healthbarImage.fillAmount = ((float)health / maxHealth);
        }
    }



    //Getters/Setters
    public int GetPlayerNum() { return playerNum; }
    public void SetPlayerNum(int num) { playerNum = num; }

    public float GetPlayerAngle() { return playerAngle; }
    public void SetPlayerAngle(float angle) { playerAngle = angle; }

    public Vector2 GetPlayerDirection() { return TrigUtilities.DegreesToVector(playerAngle); }

    public PlayerMovement GetPlayerMovement() { return playerMovement; }

    public WeaponBase GetWeapon() { return weapon; }

}