﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBase
{
    float velocityMagnitude;

    Vector2 moveVector;
    Collider2D lastColliderHit;
    float actualRadius;
    bool safetyTrigger = true;

    public LayerMask collisionMask;

    CircleCollider2D circleCollider;



    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        moveVector = direction * velocityMagnitude;
        GetComponent<SpriteRenderer>().color = color;
        actualRadius = circleCollider.radius * transform.localScale.x;

        //Figure out what to do if the bullet spawned inside of something
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius * transform.localScale.x);
        foreach (Collider2D collider in colliders)
        {
            //This bullet
            if (collider == circleCollider) continue;

            //Other player
            if (collider.CompareTag("Player") && collider.gameObject != owner)
            {
                if (causeExplosion)
                    ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce * 5, explosionRadius, (!canHitOwner) ? owner : null);
                else
                    collider.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(direction), damageForce);
                Destroy(gameObject);
            }

            //Wall
            else if (collider.CompareTag("Wall"))
            {
                if (causeExplosion)
                    ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce * 5, explosionRadius, (!canHitOwner) ? owner : null);
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        //Generate the raycast
        Vector2 castOrigin = transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, actualRadius, moveVector.normalized, moveVector.magnitude * Time.deltaTime, collisionMask);

        //Collision movement
        if (hit && lastColliderHit != hit.collider)
        {
            if (causeExplosion)
                if (explodeEveryBounce || bounces == 0)
                    ObjectPooler.instance.SpawnExplosionFromPool(hit.point, damage, damageForce * 5, explosionRadius, (!canHitOwner) ? owner : null);

            if (bounces > 0)
            {
                bounces--;

                lastColliderHit = hit.collider;
                transform.Translate(moveVector.normalized * hit.distance, Space.World);

                //The magic ingredient for applying normal surface reflections
                //v' = 2 * (v . n) * n - v;
                moveVector = 2 * (Vector2.Dot(moveVector, hit.normal.normalized)) * hit.normal.normalized - moveVector;
                moveVector *= -1;

                //Turn off owner safety if it was on
                if (safetyTrigger) safetyTrigger = false;
            }
            else
            {
                Destroy(gameObject);
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
        if (other.CompareTag("Player") && (other.gameObject != owner || (canHitOwner && !safetyTrigger)))
        {
            if (causeExplosion)
                ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce * 5, explosionRadius, (!canHitOwner) ? owner : null);
            else
                other.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(moveVector.normalized), damageForce);

            Destroy(gameObject);
        }
    }

    public void SetVelocityMagnitude(float temp) { velocityMagnitude = temp; }
}