using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    protected override void FireWeapon()
    {
        //Projectile creation
        for (int i = 0; i < 5; i++) CreateProjectile();

        //Recoil
        playerData.GetPlayerMovement().ApplyForce(playerData.GetPlayerAngle() + 180f, recoilStrength);
    }
}