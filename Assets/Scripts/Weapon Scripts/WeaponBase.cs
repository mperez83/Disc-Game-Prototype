using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected PlayerMovement playerMovement;
    protected PlayerData playerData;

    public float shootTimerLength;
    float shootTimer;

    protected virtual void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerData = GetComponentInParent<PlayerData>();
    }

    protected virtual void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0) shootTimer = 0;
    }

    public void Shoot()
    {
        if (shootTimer <= 0)
        {
            shootTimer = shootTimerLength;
            FireWeapon();
        }
    }

    public abstract void FireWeapon();
}