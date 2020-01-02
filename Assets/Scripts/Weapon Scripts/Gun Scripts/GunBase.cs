using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : WeaponBase
{
    protected ProjectileData projectileData;

    public Transform projectileStartPos;

    public bool automatic;
    public float shootTimerLength;
    public float accuracyDegreeOffsetRange;
    public float recoilStrength;

    float shootTimer;

    public GameObject hitscanProjectilePrefab;
    public GameObject physicalProjectilePrefab;



    protected override void Start()
    {
        base.Start();
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
                if (Input.GetButton("P" + owner.GetPlayerNum() + "_Fire"))
                {
                    shootTimer = shootTimerLength;
                    FireWeapon();
                }
            }
            else
            {
                if (Input.GetButtonDown("P" + owner.GetPlayerNum() + "_Fire"))
                {
                    shootTimer = shootTimerLength;
                    FireWeapon();
                }
            }
        }
        
    }



    protected virtual void FireWeapon()
    {
        CreateProjectile();
        owner.GetPlayerMovement().ApplyForce(owner.GetPlayerMovement().GetPlayerAngle() + 180f, recoilStrength);
        CameraShakeHandler.instance.IncreaseShakeAmount(recoilStrength * 0.0002f);
        audioSource.PlayRandomize();
    }

    protected virtual void CreateProjectile()
    {
        //Create basic projectile
        GameObject newBullet;
        if (projectileData.hitscan) newBullet = Instantiate(hitscanProjectilePrefab, projectileStartPos.position, Quaternion.identity);
        else newBullet = Instantiate(physicalProjectilePrefab, projectileStartPos.position, Quaternion.identity);
        ProjectileBase projectile = newBullet.GetComponent<ProjectileBase>();

        //Set general properties
        projectile.SetDirection(TrigUtilities.DegreesToVector(owner.GetPlayerMovement().GetPlayerAngle() + Random.Range(-accuracyDegreeOffsetRange, accuracyDegreeOffsetRange)));
        projectile.SetDamage(projectileData.damage);
        projectile.SetDamageForce(projectileData.damageForce);
        projectile.SetOwner(owner);
        projectile.SetCanHitOwner(projectileData.canHitOwner);
        projectile.SetColor(owner.GetComponent<SpriteRenderer>().GetModifiedBrightness(3f));
        projectile.SetBounces(projectileData.bounces);
        projectile.SetCauseExplosion(projectileData.causeExplosion);
        projectile.SetExplosionRadius(projectileData.explosionRadius);
        projectile.SetExplodeEveryBounce(projectileData.explodeEveryBounce);

        //Set properties depending on if it's hitscan or physical
        if (projectileData.hitscan) SetHitscanProperties(newBullet);
        else SetPhysicalProperties(newBullet);
    }

    void SetHitscanProperties(GameObject bullet)
    {
        HitscanProjectile hitscanProjectile = bullet.GetComponent<HitscanProjectile>();
        hitscanProjectile.SetDecayTimerLength(projectileData.decayTimerLength);
        hitscanProjectile.SetInstantTravel(projectileData.instantTravel);
    }

    void SetPhysicalProperties(GameObject bullet)
    {
        PhysicalProjectile physicalProjectile = bullet.GetComponent<PhysicalProjectile>();
        physicalProjectile.SetVelocityMagnitude(projectileData.velocityMagnitude);
        physicalProjectile.SetLifeSpan(projectileData.lifeSpan);
    }
}