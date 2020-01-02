using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : ProjectileBase
{
    float velocityMagnitude;
    float lifeSpan;

    Vector2 moveVector;
    Collider2D lastColliderHit;
    float actualRadius;
    bool safetyTrigger = true;

    public SpriteRenderer bulletGlowSprite;
    public LayerMask collisionMask;

    CircleCollider2D circleCollider;
    TrailRenderer trailRenderer;



    void Start()
    {
        //GetComponent<SpriteRenderer>().color = color;
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.startColor = color;
        trailRenderer.endColor = color;
        bulletGlowSprite.color = color;

        circleCollider = GetComponent<CircleCollider2D>();
        moveVector = direction * velocityMagnitude;
        actualRadius = circleCollider.radius * transform.localScale.x;

        //Figure out what to do if the bullet spawned inside of something
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius * transform.localScale.x);
        foreach (Collider2D collider in colliders)
        {
            //This bullet
            if (collider == circleCollider) continue;

            //Other player
            if (collider.CompareTag("Player") && collider.gameObject != owner.gameObject)
            {
                if (causeExplosion)
                    ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce, explosionRadius, owner, canHitOwner);
                else
                    collider.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(direction), damageForce, owner);
                Destroy(gameObject);
            }

            //Wall
            else if (collider.CompareTag("Wall"))
            {
                if (causeExplosion)
                    ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce, explosionRadius, owner, canHitOwner);
                Destroy(gameObject);
            }
        }

        LeanTween.delayedCall(gameObject, lifeSpan, () =>
        {
            if (causeExplosion)
                ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce, explosionRadius, owner, canHitOwner);
            Destroy(gameObject);
        });
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
                    ObjectPooler.instance.SpawnExplosionFromPool(hit.point, damage, damageForce, explosionRadius, owner, canHitOwner);

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
        //if (GameManager.instance.IsTransformOffCamera(transform)) Destroy(gameObject);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (other.gameObject != owner.gameObject || (canHitOwner && !safetyTrigger)))
        {
            if (causeExplosion)
                ObjectPooler.instance.SpawnExplosionFromPool(transform.position, damage, damageForce, explosionRadius, owner, canHitOwner);
            else
                other.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(moveVector.normalized), damageForce, owner);

            Destroy(gameObject);
        }
    }

    public void SetVelocityMagnitude(float temp) { velocityMagnitude = temp; }
    public void SetLifeSpan(float temp) { lifeSpan = temp; }
}