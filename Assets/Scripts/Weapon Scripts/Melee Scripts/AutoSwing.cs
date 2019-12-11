using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSwing : MeleeBase
{
    void Update()
    {
        if (Input.GetButton("P" + owner.GetPlayerNum() + "_Fire"))
        {
            anim.Play("Attack");
        }
    }
}