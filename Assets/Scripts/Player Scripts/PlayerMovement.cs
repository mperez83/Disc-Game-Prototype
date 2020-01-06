using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerAngle;
    float knockbackMultiplier;
    PlayerData playerData;
    Rigidbody2D rb;



    void Start()
    {
        playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Update rotation based on player input
        Vector2 axis = new Vector2(Input.GetAxisRaw("P" + playerData.GetPlayerNum() + "_Joystick_L_Horizontal"), Input.GetAxisRaw("P" + playerData.GetPlayerNum() + "_Joystick_L_Vertical"));
        if (axis != Vector2.zero && axis.magnitude > 0.5f)
        {
            playerAngle = TrigUtilities.VectorToDegrees(axis);
        }

        //Handle input for if the player doesn't have a weapon
        if (Input.GetButtonDown("P" + playerData.GetPlayerNum() + "_Fire"))
        {
            if (!playerData.GetWeapon()) ApplyForceForward(1);
        }

        //Handle flying off into infinity
        /*if (GameManager.instance.IsTransformOffCamera(transform))
        {
            transform.position = new Vector2(0, 0);
            print("Zoop!");
        };*/
    }

    void FixedUpdate()
    {
        rb.rotation = -playerAngle;
    }



    public void ApplyForce(float angle, float force)
    {
        rb.AddForce(TrigUtilities.DegreesToVector(angle) * force);
    }

    public void ApplyForceForward(float force)
    {
        ApplyForce(playerAngle, force);
    }

    //Like ApplyForce, but is affected by the knockback multiplier
    public void ApplyKnockback(float angle, float force)
    {
        rb.AddForce(TrigUtilities.DegreesToVector(angle) * force * knockbackMultiplier);
    }

    public float GetPlayerAngle() { return playerAngle; }
    public float GetKnockbackMultiplier() { return knockbackMultiplier; }
    public void SetKnockbackMultiplier(float temp) { knockbackMultiplier = temp; }
    public Vector2 GetPlayerDirection() { return TrigUtilities.DegreesToVector(playerAngle); }
}