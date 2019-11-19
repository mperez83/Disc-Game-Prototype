using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    int playerNum;
    float playerAngle;

    MovementController movementController;



    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    void Update()
    {
        //Figure out which direction the player is pointing
        Vector2 axis = new Vector2(Input.GetAxisRaw("P" + playerNum + "_Joystick_L_Horizontal"), Input.GetAxisRaw("P" + playerNum + "_Joystick_L_Vertical"));
        if (axis != Vector2.zero && axis.magnitude > 0.5f)
        {
            playerAngle = TrigUtilities.VectorToDegrees(axis);
            transform.rotation = Quaternion.AngleAxis(-playerAngle, Vector3.forward);
        }

        //Handle shooting the gun
        if (Input.GetButtonDown("P" + playerNum + "_Fire")) movementController.AddRecoilForce(playerAngle, 10);
    }



    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }
}