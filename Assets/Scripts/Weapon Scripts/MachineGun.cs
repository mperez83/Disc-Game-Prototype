using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponBase
{
    protected override void FireWeapon()
    {
        //Projectile creation
        CreateProjectile();

        //Recoil
        playerData.GetPlayerMovement().ApplyForce(playerData.GetPlayerAngle() + 180f, recoilStrength);
    }
}