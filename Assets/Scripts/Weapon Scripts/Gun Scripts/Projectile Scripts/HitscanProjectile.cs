using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanProjectile : ProjectileBase
{
    LineRenderer lineRend;
    float initialWidth;

    int vertexNum;
    Vector2 vertexStart;

    bool fadingOut;

    //Hitscan properties
    float decayTimerLength;
    float decayTimer;
    bool instantTravel;

    public LayerMask playerCollisionMask;
    public LayerMask wallCollisionMask;



    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        initialWidth = lineRend.startWidth;

        lineRend.startColor = color;
        lineRend.endColor = color;

        decayTimer = decayTimerLength;

        vertexNum = 0;
        vertexStart = transform.position;

        //Check if we spawned a bullet in something
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D collider in colliders)
        {
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

            //Everything else
            else continue;
        }

        if (instantTravel)
        {
            lineRend.positionCount = 2 + bounces;

            do
            {
                CastRays();
            } while (!fadingOut);
        }
        else
        {
            lineRend.positionCount = 2;
        }
    }

    void Update()
    {
        if (fadingOut)
        {
            float newWidth = initialWidth * (decayTimer / decayTimerLength);
            lineRend.startWidth = newWidth;
            lineRend.endWidth = newWidth;

            decayTimer -= Time.deltaTime;
            if (decayTimer <= 0) Destroy(gameObject);
        }
        else
        {
            CastRays();
        }
    }



    void CastRays()
    {
        RaycastHit2D playerHit = Physics2D.Raycast(vertexStart, direction, Mathf.Infinity, playerCollisionMask);
        RaycastHit2D wallHit = Physics2D.Raycast(vertexStart, direction, Mathf.Infinity, wallCollisionMask);

        //If we hit a player
        if (playerHit && (playerHit.collider.gameObject != owner.gameObject || canHitOwner))
        {
            if (wallHit)
            {
                if (wallHit.distance > playerHit.distance)
                {
                    HandlePlayerHit(playerHit);
                }
                else
                {
                    HandleWallHit(wallHit);
                }
            }
            else
            {
                HandlePlayerHit(playerHit);
            }
        }

        //If we hit a wall
        else if (wallHit)
        {
            HandleWallHit(wallHit);
        }

        //If we shot off into infinity
        else
        {
            lineRend.SetPosition(vertexNum, vertexStart);
            lineRend.SetPosition(vertexNum + 1, vertexStart + (direction * 25));

            lineRend.positionCount = 2 + vertexNum;
            fadingOut = true;
        }
    }

    void HandlePlayerHit(RaycastHit2D playerHit)
    {
        lineRend.SetPosition(vertexNum, vertexStart);
        lineRend.SetPosition(vertexNum + 1, playerHit.point);
        lineRend.positionCount = 2 + vertexNum;

        fadingOut = true;

        if (causeExplosion)
            ObjectPooler.instance.SpawnExplosionFromPool(playerHit.point, damage, damageForce, explosionRadius, owner, canHitOwner);
        else
            playerHit.transform.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(direction), damageForce, owner);
    }

    void HandleWallHit(RaycastHit2D wallHit)
    {
        lineRend.SetPosition(vertexNum, vertexStart);
        lineRend.SetPosition(vertexNum + 1, wallHit.point);

        if (causeExplosion)
            if (explodeEveryBounce || bounces == 0)
                ObjectPooler.instance.SpawnExplosionFromPool(wallHit.point, damage, damageForce, explosionRadius, owner, canHitOwner);

        bounces--;
        if (bounces >= 0)
        {
            if (instantTravel) vertexNum++;
            vertexStart = wallHit.point;
            direction = Vector2.Reflect(direction, wallHit.normal);
        }
        else
        {
            fadingOut = true;
        }
    }



    public void SetDecayTimerLength(float temp) { decayTimerLength = temp; }
    public void SetInstantTravel(bool temp) { instantTravel = temp; }
}