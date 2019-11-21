using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanProjectile : ProjectileBase
{
    LineRenderer lineRend;
    float initialWidth;

    float decayTimerLength;
    float decayTimer;

    public LayerMask collisionMask;



    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2 + bounces;
        initialWidth = lineRend.startWidth;

        decayTimer = decayTimerLength;

        int vertexNum = 0;
        Vector2 vertexStart = transform.position;
        Vector2 direction = TrigUtilities.DegreesToVector(angle);

        do
        {

            RaycastHit2D hit = Physics2D.Raycast(vertexStart, direction, Mathf.Infinity, collisionMask);

            //If we hit something, draw the bullet hitting whatever we hit
            if (hit)
            {
                //Draw our current hitscan start and end
                lineRend.SetPosition(vertexNum, vertexStart);
                lineRend.SetPosition(vertexNum + 1, hit.point);

                //If we hit the player, end the bullet
                if (hit.transform.CompareTag("Player"))
                {
                    hit.transform.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(direction), damageForce);
                    lineRend.positionCount = 2 + vertexNum;
                    break;
                }

                //Otherwise, calculate the next bounce (if it's the last bounce, we don't end up using this data)
                else
                {
                    bounces--;
                    vertexNum++;
                    vertexStart = hit.point;
                    direction = Vector2.Reflect(direction, hit.normal);
                }
            }

            //If we shot off into infinity, draw a really long bullet
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



    public void SetDecayTimerLength(float temp) { decayTimerLength = temp; }
}