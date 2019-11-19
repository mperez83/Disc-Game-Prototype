using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    float playerAngle;
    Vector2 moveVector;

    public Transform weaponPivot;

    RaycastController raycastController;



    void Start()
    {
        raycastController = GetComponent<RaycastController>();
    }

    void Update()
    {
        //Figure out which direction the player is pointing
        Vector2 axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (axis != Vector2.zero && axis.magnitude > 0.5f)
        {
            playerAngle = TrigUtilities.VectorToDegrees(axis);
            transform.rotation = Quaternion.AngleAxis(-playerAngle, Vector3.forward);
            weaponPivot.rotation = transform.rotation;
        }

        //Handle shooting the gun
        if (Input.GetButtonDown("Fire"))
        {
            AddRecoilForce(2);
        }

        //raycastController.UpdateRaycasts();
        raycastController.UpdateRaycastSingle();

        //Actually move the player
        transform.Translate(moveVector * Time.deltaTime, Space.World);
    }



    public void AddRecoilForce(float force)
    {
        //Convert our angle into a vector and apply it to our current moveVector
        moveVector -= TrigUtilities.DegreesToVector(playerAngle) * force;
    }

    public Vector2 GetMoveVector()
    {
        return moveVector;
    }

    public void SetMoveVector(Vector2 newVector)
    {
        moveVector = newVector;
    }
}