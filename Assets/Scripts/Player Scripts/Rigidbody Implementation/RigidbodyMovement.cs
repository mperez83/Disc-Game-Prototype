using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    public float speedLimit;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddRecoilForce(float angle, float force)
    {
        rb.AddForce(-(TrigUtilities.DegreesToVector(angle) * force) * Time.deltaTime);
        if (rb.velocity.magnitude > speedLimit)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedLimit);
    }
}