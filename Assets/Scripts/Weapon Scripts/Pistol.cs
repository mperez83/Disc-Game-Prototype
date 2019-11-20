using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform bulletStartPos;

    public override void FireWeapon()
    {
        //Create bullet
        GameObject newBullet = Instantiate(bulletPrefab, bulletStartPos.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetAngle(playerData.GetPlayerAngle());

        //Recoil
        playerMovement.AddRecoilForce(playerData.GetPlayerAngle(), 0.25f);
    }
}