﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    protected override void FireWeapon()
    {
        //Projectile creation
        if (projectileData.hitscan)
        {
            CreateHitscanProjectile();
        }
        else
        {

        }

        //Recoil
        playerData.GetPlayerMovement().ApplyForce(playerData.GetPlayerAngle() + 180f, recoilStrength);
    }
}