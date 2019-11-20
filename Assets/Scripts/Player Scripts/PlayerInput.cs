using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    PlayerData player;
    PlayerMovement playerMovement;

    void Start()
    {
        player = GetComponent<PlayerData>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //Figure out which direction the player is pointing
        Vector2 axis = new Vector2(Input.GetAxisRaw("P" + player.GetPlayerNum() + "_Joystick_L_Horizontal"), Input.GetAxisRaw("P" + player.GetPlayerNum() + "_Joystick_L_Vertical"));
        if (axis != Vector2.zero && axis.magnitude > 0.5f)
        {
            player.SetPlayerAngle(TrigUtilities.VectorToDegrees(axis));
            transform.rotation = Quaternion.AngleAxis(-player.GetPlayerAngle(), Vector3.forward);
        }

        //Handle fire input
        if (Input.GetButtonDown("P" + player.GetPlayerNum() + "_Fire"))
        {
            //No weapon
            if (!player.GetWeapon()) playerMovement.AddRecoilForce(player.GetPlayerAngle(), 0.1f);

            //Weapon
            else player.GetWeapon().Shoot();
        }
    }
}