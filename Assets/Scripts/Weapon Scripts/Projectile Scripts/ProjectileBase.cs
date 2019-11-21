using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    //General properties
    protected float angle;
    protected int damage;
    protected float damageForce;

    //Special properties shared by both hitscan and physical projectiles
    protected int bounces;

    //Getters/Setters
    public void SetAngle(float temp) { angle = temp; }
    public void SetDamage(int temp) { damage = temp; }
    public void SetDamageForce(float temp) { damageForce = temp; }
    public void SetBounces(int temp) { bounces = temp; }
}