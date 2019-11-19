using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    int playerNum;

    float playerAngle;
    Vector2 moveVector;
    public float speedLimit;

    public Transform weaponPivot;

    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Figure out which direction the player is pointing
        Vector2 axis = new Vector2(Input.GetAxisRaw("P" + playerNum + "_Joystick_L_Horizontal"), Input.GetAxisRaw("P" + playerNum + "_Joystick_L_Vertical"));
        if (axis != Vector2.zero && axis.magnitude > 0.5f)
        {
            playerAngle = TrigUtilities.VectorToDegrees(axis);
            transform.rotation = Quaternion.AngleAxis(-playerAngle, Vector3.forward);
            weaponPivot.rotation = transform.rotation;
        }

        //Handle shooting the gun
        if (Input.GetButtonDown("P" + playerNum + "_Fire"))
        {
            AddRecoilForce(2);
        }
    }



    public void AddRecoilForce(float force)
    {
        rb.AddForce(-(TrigUtilities.DegreesToVector(playerAngle) * force) * Time.deltaTime);
        if (rb.velocity.magnitude > speedLimit)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedLimit);
    }

    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }
}