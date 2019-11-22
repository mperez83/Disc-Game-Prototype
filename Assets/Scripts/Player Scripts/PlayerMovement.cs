using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    PlayerData playerData;

    public bool useSpeedLimit;
    public float speedLimit;

    public float dodgePower;
    public float dodgeCooldownTimerLength;
    float dodgeCooldownTimer;
    public Image staminabarImage;

    public Canvas playerCanvas;

    Rigidbody2D rb;



    void Start()
    {
        playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dodgeCooldownTimer -= Time.deltaTime;
        if (dodgeCooldownTimer < 0) dodgeCooldownTimer = 0;

        staminabarImage.fillAmount = (dodgeCooldownTimerLength - dodgeCooldownTimer) / dodgeCooldownTimerLength;

        //Handle dodge input
        if (Input.GetButtonDown("P" + playerData.GetPlayerNum() + "_Dodge"))
        {
            if (dodgeCooldownTimer <= 0)
            {
                dodgeCooldownTimer = dodgeCooldownTimerLength;
                rb.velocity = playerData.GetPlayerDirection() * dodgePower;
            }
        }

        //Handle flying off into infinity
        if (GameManager.instance.IsTransformOffCamera(transform))
        {
            transform.position = new Vector2(0, 0);
            print("Zoop!");
        };

        //Update position of personal canvas
        playerCanvas.transform.position = transform.position;
    }



    public void ApplyForce(float angle, float force)
    {
        rb.AddForce(TrigUtilities.DegreesToVector(angle) * force);

        if (useSpeedLimit)
        {
            if (rb.velocity.magnitude > speedLimit)
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedLimit);
        }
    }
}