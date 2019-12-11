using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualSwing : MeleeBase
{
    void Update()
    {
        if (Input.GetButtonDown("P" + owner.GetPlayerNum() + "_Fire"))
        {
            DeactivateHitbox();
            ClearDamagedPlayers();
            anim.Rebind();
            anim.Play("Attack");
        }
    }
}