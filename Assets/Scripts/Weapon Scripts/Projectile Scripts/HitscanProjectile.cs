using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanProjectile : ProjectileBase
{
    LineRenderer lineRend;
    float initialWidth;

    int vertexNum;
    Vector2 vertexStart;

    float decayTimerLength;
    float decayTimer;

    public LayerMask playerCollisionMask;
    public LayerMask wallCollisionMask;



    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2 + bounces;
        initialWidth = lineRend.startWidth;

        lineRend.startColor = color;
        lineRend.endColor = color;

        decayTimer = decayTimerLength;

        vertexNum = 0;
        vertexStart = transform.position;

        do
        {

            RaycastHit2D playerHit = Physics2D.Raycast(vertexStart, direction, Mathf.Infinity, playerCollisionMask);
            RaycastHit2D wallHit = Physics2D.Raycast(vertexStart, direction, Mathf.Infinity, wallCollisionMask);

            if (playerHit && (playerHit.collider.gameObject != owner || canHitOwner))
            {
                if (wallHit)
                {
                    if (wallHit.distance > playerHit.distance)
                    {
                        HandlePlayerHit(playerHit);
                        break;
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

            else if (wallHit)
            {
                HandleWallHit(wallHit);
            }

            else
            {
                lineRend.SetPosition(vertexNum, vertexStart);
                lineRend.SetPosition(vertexNum + 1, vertexStart + (direction * 25));

                lineRend.positionCount = 2 + vertexNum;
                break;
            }

        } while (bounces >= 0);
    }

    void Update()
    {
        float newWidth = initialWidth * (decayTimer / decayTimerLength);
        lineRend.startWidth = newWidth;
        lineRend.endWidth = newWidth;

        decayTimer -= Time.deltaTime;
        if (decayTimer <= 0) Destroy(gameObject);
    }



    void HandlePlayerHit(RaycastHit2D playerHit)
    {
        lineRend.SetPosition(vertexNum, vertexStart);
        lineRend.SetPosition(vertexNum + 1, playerHit.point);

        playerHit.transform.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(direction), damageForce);
        lineRend.positionCount = 2 + vertexNum;
    }

    void HandleWallHit(RaycastHit2D wallHit)
    {
        lineRend.SetPosition(vertexNum, vertexStart);
        lineRend.SetPosition(vertexNum + 1, wallHit.point);

        bounces--;
        vertexNum++;
        vertexStart = wallHit.point;
        direction = Vector2.Reflect(direction, wallHit.normal);
    }



    public void SetDecayTimerLength(float temp) { decayTimerLength = temp; }
}