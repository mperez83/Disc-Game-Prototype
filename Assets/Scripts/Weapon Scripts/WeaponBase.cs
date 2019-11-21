﻿using System.Collections;
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

    protected virtual void CreateProjectile()
    {
        //Create basic projectile
        GameObject newBullet;
        if (projectileData.hitscan) newBullet = Instantiate(hitscanProjectilePrefab, projectileStartPos.position, Quaternion.identity);
        else newBullet = Instantiate(physicalProjectilePrefab, projectileStartPos.position, Quaternion.identity);
        ProjectileBase projectile = newBullet.GetComponent<ProjectileBase>();

        //Set general properties
        projectile.SetDirection(TrigUtilities.DegreesToVector(playerData.GetPlayerAngle() + Random.Range(-accuracyDegreeOffsetRange, accuracyDegreeOffsetRange)));
        projectile.SetDamage(projectileData.damage);
        projectile.SetDamageForce(projectileData.damageForce);
        projectile.SetOwner(playerData.gameObject);
        projectile.SetCanHitOwner(projectileData.canHitOwner);
        projectile.SetColor(playerData.GetComponent<SpriteRenderer>().GetModifiedBrightness(3f));
        projectile.SetBounces(projectileData.bounces);

        //Set properties depending on if it's hitscan or physical
        if (projectileData.hitscan) SetHitscanProperties(newBullet);
        else SetPhysicalProperties(newBullet);
    }

    void SetHitscanProperties(GameObject bullet)
    {
        HitscanProjectile hitscanProjectile = bullet.GetComponent<HitscanProjectile>();
        hitscanProjectile.SetDecayTimerLength(projectileData.decayTimerLength);
    }

    void SetPhysicalProperties(GameObject bullet)
    {
        PhysicalProjectile physicalProjectile = bullet.GetComponent<PhysicalProjectile>();
        physicalProjectile.SetVelocityMagnitude(projectileData.velocityMagnitude);
    }
}