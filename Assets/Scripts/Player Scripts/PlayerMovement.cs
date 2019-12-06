using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerData))]
public class PlayerMovement : MonoBehaviour
{
    PlayerData playerData;

    public bool useSpeedLimit;
    public float speedLimit;

    public Canvas playerCanvas;
    Rigidbody2D rb;



    void Start()
    {
        playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

    public void ApplyForceForward(float force)
    {
        ApplyForce(playerData.GetPlayerAngle(), force);
    }
}