using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    public GameObject bulletPrefab;
    public Transform bulletStartPos;

    public override void FireWeapon()
    {
        //Create bullets
        for (int i = 0; i < 5; i++)
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletStartPos.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().SetAngle(playerData.GetPlayerAngle() + Random.Range(-15f, 15f));
        }

        //Recoil
        playerMovement.AddRecoilForce(playerData.GetPlayerAngle(), 0.5f);
    }
}