using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    //General properties
    protected Vector2 direction;

    protected int damage;
    protected float damageForce;

    protected PlayerData owner;
    protected bool canHitOwner;

    protected Color color;

    //Special properties shared by both hitscan and physical projectiles
    protected int bounces;
    protected bool causeExplosion;
    protected float explosionRadius;
    protected bool explodeEveryBounce;
    
    //Getters/Setters
    public void SetDirection(Vector2 temp) { direction = temp; }
    public void SetDamage(int temp) { damage = temp; }
    public void SetDamageForce(float temp) { damageForce = temp; }

    public PlayerData GetOwner() { return owner; }
    public void SetOwner(PlayerData temp) { owner = temp; }

    public void SetCanHitOwner(bool temp) { canHitOwner = temp; }
    public void SetColor(Color temp) { color = temp; }
    public void SetBounces(int temp) { bounces = temp; }
    public void SetExplosionRadius(float temp) { explosionRadius = temp; }
    public void SetCauseExplosion(bool temp) { causeExplosion = temp; }
    public void SetExplodeEveryBounce(bool temp) { explodeEveryBounce = temp; }
}