using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Vector2 moveVector;
    Collider2D lastColliderHit;

    public LayerMask collisionMask;

    CircleCollider2D circleCollider;



    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        //Generate the raycast
        Vector2 castOrigin = transform.position;
        //castOrigin += moveVector.normalized * 0.015f;
        RaycastHit2D hit = Physics2D.CircleCast(castOrigin, circleCollider.radius, moveVector.normalized, moveVector.magnitude * Time.deltaTime, collisionMask);

        //Collision movement
        if (hit && lastColliderHit != hit.collider)
        {
            lastColliderHit = hit.collider;
            transform.Translate(moveVector.normalized * hit.distance, Space.World);

            //The magic ingredient for applying normal surface reflections
            //v' = 2 * (v . n) * n - v;
            moveVector = 2 * (Vector2.Dot(moveVector, hit.normal.normalized)) * hit.normal.normalized - moveVector;
            moveVector *= -1;
        }

        //Normal movement
        else
        {
            if (lastColliderHit != null) lastColliderHit = null;
            transform.Translate(moveVector * Time.deltaTime, Space.World);
        }
    }



    public void AddRecoilForce(float angle, float force)
    {
        //Convert our angle into a vector and apply it to our current moveVector
        moveVector -= TrigUtilities.DegreesToVector(angle) * force;
    }
}