using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class PlayerBrake : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float brakePower;
    public Transform brakeAura;
    Vector3 vel;

    PlayerData playerData;
    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerData = GetComponent<PlayerData>();
        brakeAura.GetComponent<SpriteRenderer>().color = playerData.playerColors[playerData.GetPlayerNum() - 1];
    }

    void Update()
    {
        if (Input.GetButton("P" + playerData.GetPlayerNum() + "_Action"))
        {
            rb.velocity = rb.velocity * (1 - brakePower);
            brakeAura.localScale = Vector3.SmoothDamp(brakeAura.localScale, new Vector3(3, 3, 3), ref vel, 0.15f);
        }
        else
        {
            brakeAura.localScale = Vector3.SmoothDamp(brakeAura.localScale, new Vector3(1, 1, 1), ref vel, 0.15f);
        }
    }
}