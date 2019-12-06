using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    protected override void FireWeapon()
    {
        //Projectile creation
        for (int i = 0; i < 5; i++) CreateProjectile();

        //Recoil
        owner.GetPlayerMovement().ApplyForce(owner.GetPlayerAngle() + 180f, recoilStrength);
    }
}