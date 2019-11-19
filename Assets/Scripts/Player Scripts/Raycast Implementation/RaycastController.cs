using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;

    CircleCollider2D circleCollider;
    MovementController movementController;



    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        movementController = GetComponent<MovementController>();
    }

    public void UpdateRaycasts()
    {
        Vector2 moveVector = movementController.GetMoveVector();
        Vector2 direction = moveVector.normalized;

        float moveAngle = TrigUtilities.VectorToDegrees(moveVector) - 90;
        for (int i = 0; i < 10; i++)
        {
            //Movement raycast
            float radAngle = (moveAngle + (180f * (i / 10f))) * Mathf.Deg2Rad;
            Vector2 rayOrigin = transform.position + (new Vector3(Mathf.Sin(radAngle), Mathf.Cos(radAngle)) * circleCollider.radius);
            Debug.DrawRay(rayOrigin, moveVector * Time.deltaTime * 10f, Color.red);
        }
    }

    public void UpdateRaycastSingle()
    {
        Vector2 moveVector = movementController.GetMoveVector();
        float moveAngle = TrigUtilities.VectorToDegrees(moveVector);

        //Movement raycast
        Vector2 rayOrigin = transform.position + (new Vector3(Mathf.Sin(moveAngle * Mathf.Deg2Rad), Mathf.Cos(moveAngle * Mathf.Deg2Rad)) * circleCollider.radius);
        Vector2 direction = moveVector.normalized;
        Debug.DrawRay(rayOrigin, moveVector * Time.deltaTime * 10f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, moveVector.magnitude, collisionMask);
        if (hit)
        {
            movementController.SetMoveVector(moveVector);
        }
    }

    void Update()
    {
        
    }
}