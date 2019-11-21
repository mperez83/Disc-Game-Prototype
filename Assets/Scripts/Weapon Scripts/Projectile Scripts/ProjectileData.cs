using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    //General properties
    public int damage;
    public float damageForce;
    public bool canHitOwner;
    public int bounces;

    public bool hitscan;

    //Hitscan properties
    public float decayTimerLength;
    float decayTimer;

    //Physical properties
    public float velocityMagnitude;
}