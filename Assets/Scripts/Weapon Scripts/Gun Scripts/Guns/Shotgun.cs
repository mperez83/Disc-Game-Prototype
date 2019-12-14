﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    public int numberOfPellets;

    protected override void FireWeapon()
    {
        //Projectile creation
        for (int i = 0; i < numberOfPellets; i++) CreateProjectile();

        //Recoil
        owner.GetPlayerMovement().ApplyForce(owner.GetPlayerMovement().GetPlayerAngle() + 180f, recoilStrength);
    }
}