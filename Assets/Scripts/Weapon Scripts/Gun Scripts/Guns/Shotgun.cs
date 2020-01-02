using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunBase
{
    public int numberOfPellets;

    protected override void FireWeapon()
    {
        for (int i = 0; i < numberOfPellets; i++) CreateProjectile();
        owner.GetPlayerMovement().ApplyForce(owner.GetPlayerMovement().GetPlayerAngle() + 180f, recoilStrength);
        CMShakeHandler.instance.IncreaseShakeAmount(camShakeStrength);
        audioSource.PlayRandomize();
    }
}