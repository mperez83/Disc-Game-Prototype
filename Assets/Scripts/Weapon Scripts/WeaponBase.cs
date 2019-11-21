using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Weapons are in charge of the following functionalities:
//  *Projectile spawn position
//  *Projectile fire rate
//  *Player recoil
//  *Spawning the projectiles

public abstract class WeaponBase : MonoBehaviour
{
    protected PlayerData playerData;
    protected ProjectileData projectileData;

    public Transform projectileStartPos;

    public bool automatic;
    public float shootTimerLength;
    public float accuracyDegreeOffsetRange;
    public float recoilStrength;

    float shootTimer;

    public GameObject hitscanProjectilePrefab;
    public GameObject physicalProjectilePrefab;



    protected virtual void Start()
    {
        playerData = GetComponentInParent<PlayerData>();
        projectileData = GetComponent<ProjectileData>();
    }

    protected virtual void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0) shootTimer = 0;

        //Handle fire input
        if (shootTimer <= 0)
        {
            if (automatic)
            {
                if (Input.GetButton("P" + playerData.GetPlayerNum() + "_Fire"))
                {
                    shootTimer = shootTimerLength;
                    FireWeapon();
                }
            }
            else
            {
                if (Input.GetButtonDown("P" + playerData.GetPlayerNum() + "_Fire"))
                {
                    shootTimer = shootTimerLength;
                    FireWeapon();
                }
            }
        }
        
    }



    protected abstract void FireWeapon();

    protected virtual void CreateHitscanProjectile()
    {
        GameObject newBullet = Instantiate(hitscanProjectilePrefab, projectileStartPos.position, Quaternion.identity);
        HitscanProjectile hitscanProjectile = newBullet.GetComponent<HitscanProjectile>();

        hitscanProjectile.SetAngle(playerData.GetPlayerAngle() + Random.Range(-accuracyDegreeOffsetRange, accuracyDegreeOffsetRange));
        hitscanProjectile.SetDamage(projectileData.damage);
        hitscanProjectile.SetDamageForce(projectileData.damageForce);
        hitscanProjectile.SetDecayTimerLength(projectileData.decayTimerLength);
        hitscanProjectile.SetBounces(projectileData.bounces);
    }
}