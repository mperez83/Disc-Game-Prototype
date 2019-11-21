using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBase
{
    float velocityMagnitude;

    Vector2 moveVector;
    Collider2D lastColliderHit;
    float actualRadius;

    public LayerMask collisionMask;

    CircleCollider2D circleCollider;



    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        moveVector = direction * velocityMagnitude;
        GetComponent<SpriteRenderer>().color = color;
        actualRadius = circleCollider.radius * transform.localScale.x;
    }

    void Update()
    {
        //Generate the raycast
        Vector2 castOrigin = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, actualRadius, moveVector.normalized, moveVector.magnitude * Time.deltaTime, collisionMask);

        //Collision movement
        if (hit && lastColliderHit != hit.collider)
        {
            //If we hit a player
            if (hit.collider.CompareTag("Player") && (hit.collider.gameObject != owner || (hit.collider.gameObject == owner && canHitOwner)))
            {
                hit.transform.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(moveVector.normalized), damageForce);
                Destroy(gameObject);
            }

            //If we hit a wall
            else
            {
                if (bounces > 0)
                {
                    bounces--;

                    lastColliderHit = hit.collider;
                    transform.Translate(moveVector.normalized * hit.distance, Space.World);

                    //The magic ingredient for applying normal surface reflections
                    //v' = 2 * (v . n) * n - v;
                    moveVector = 2 * (Vector2.Dot(moveVector, hit.normal.normalized)) * hit.normal.normalized - moveVector;
                    moveVector *= -1;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        //Normal movement
        else
        {
            if (lastColliderHit != null) lastColliderHit = null;
            transform.Translate(moveVector * Time.deltaTime, Space.World);
        }

        //Detect if it went out into infinity
        if (GameManager.instance.IsTransformOffCamera(transform)) Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (other.gameObject != owner || canHitOwner))
        {
            other.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(moveVector.normalized), damageForce);
            Destroy(gameObject);
        }
    }

    public void SetVelocityMagnitude(float temp) { velocityMagnitude = temp; }
}