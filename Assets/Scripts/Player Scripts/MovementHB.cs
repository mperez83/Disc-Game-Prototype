using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHB : PlayerMovement
{
    Collider2D lastColliderHit;

    public LayerMask collisionMask;

    CircleCollider2D circleCollider;
    Rigidbody2D rb;



    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Generate the raycast
        Vector2 castOrigin = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, circleCollider.radius, rb.velocity.normalized, rb.velocity.magnitude * Time.deltaTime, collisionMask);

        //Wall collisions
        if (hit && lastColliderHit != hit.collider)
        {
            lastColliderHit = hit.collider;
            transform.Translate(rb.velocity.normalized * hit.distance, Space.World);

            //The magic ingredient for applying normal surface reflections
            //v' = 2 * (v . n) * n - v;
            rb.velocity = 2 * (Vector2.Dot(rb.velocity, hit.normal.normalized)) * hit.normal.normalized - rb.velocity;
            rb.velocity *= -1;
        }

        //Normal movement
        else
        {
            if (lastColliderHit != null) lastColliderHit = null;
        }

        if (GameManager.instance.IsPositionOutOfBounds(new Vector2(transform.position.x, transform.position.y)))
        {
            transform.position = new Vector2(0, 0);
            print("Zoop!");
        };
    }



    public override void ApplyForce(float angle, float force)
    {
        rb.AddForce(TrigUtilities.DegreesToVector(angle) * force * Time.deltaTime);
    }
}