using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRB : PlayerMovement
{
    public bool useSpeedLimit;
    public float speedLimit;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.IsPositionOutOfBounds(new Vector2(transform.position.x, transform.position.y)))
        {
            transform.position = new Vector2(0, 0);
            print("Zoop!");
        };
    }

    public override void ApplyForce(float angle, float force)
    {
        rb.AddForce(TrigUtilities.DegreesToVector(angle) * force * Time.deltaTime);

        if (useSpeedLimit)
        {
            if (rb.velocity.magnitude > speedLimit)
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedLimit);
        }
    }
}