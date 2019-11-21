using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    //General properties
    protected Vector2 direction;

    protected int damage;
    protected float damageForce;

    protected GameObject owner;
    protected bool canHitOwner;

    protected Color color;

    //Special properties shared by both hitscan and physical projectiles
    protected int bounces;
    
    //Getters/Setters
    public void SetDirection(Vector2 temp) { direction = temp; }
    public void SetDamage(int temp) { damage = temp; }
    public void SetDamageForce(float temp) { damageForce = temp; }
    public void SetOwner(GameObject temp) { owner = temp; }
    public void SetCanHitOwner(bool temp) { canHitOwner = temp; }
    public void SetColor(Color temp) { color = temp; }
    public void SetBounces(int temp) { bounces = temp; }
}