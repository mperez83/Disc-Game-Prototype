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
        if (playerHit && (playerHit.collider.gameObject != owner || canHitOwner))
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

        playerHit.transform.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(direction), damageForce);
        lineRend.positionCount = 2 + vertexNum;

        fadingOut = true;

        if (causeExplosion)
            ObjectPooler.instance.SpawnExplosionFromPool(playerHit.point, damageForce * 5, explosionRadius);
    }

    void HandleWallHit(RaycastHit2D wallHit)
    {
        lineRend.SetPosition(vertexNum, vertexStart);
        lineRend.SetPosition(vertexNum + 1, wallHit.point);

        if (causeExplosion)
            if (explodeEveryBounce || bounces == 0)
                ObjectPooler.instance.SpawnExplosionFromPool(wallHit.point, damageForce * 5, explosionRadius);

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