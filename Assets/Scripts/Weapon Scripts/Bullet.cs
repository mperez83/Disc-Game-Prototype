using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    LineRenderer lineRend;
    float initialWidth;
    float angle;

    float decayTimerLength = 0.2f;
    float decayTimer;

    public LayerMask collisionMask;



    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        initialWidth = lineRend.startWidth;

        decayTimer = decayTimerLength;

        Vector2 direction = TrigUtilities.DegreesToVector(angle);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, collisionMask);
        if (hit)
        {
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, hit.point);

            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<PlayerData>().TakeDamage(1);
            }
        }
    }

    void Update()
    {
        float newWidth = initialWidth * (decayTimer / decayTimerLength);
        lineRend.startWidth = newWidth;
        lineRend.endWidth = newWidth;

        decayTimer -= Time.deltaTime;
        if (decayTimer <= 0) Destroy(gameObject);
    }



    //Getters/Setters
    public void SetAngle(float newAngle) { angle = newAngle; }
}