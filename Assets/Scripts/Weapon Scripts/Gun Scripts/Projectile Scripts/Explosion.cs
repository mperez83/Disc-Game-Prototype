﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    public float duration;

    float explosionDamage;
    float explosionForce;
    float explosionRadius;

    PlayerData owner;
    bool canHitOwner;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnObjectSpawn()
    {
        //Apply force
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                CircleCollider2D playerCollider = collider.GetComponent<CircleCollider2D>();

                //Add the player's radius to the explosion radius because the distance between the explosion and the player might be farther than the explosion radius
                float updatedExplosionRadius = explosionRadius + playerCollider.radius;

                //Get angle/distance of explosion to target
                Vector2 forceDirection = collider.transform.position - transform.position;
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                //Calculate damage the target will take
                int damage = (int)Mathf.Ceil(explosionDamage * (updatedExplosionRadius - distance) / updatedExplosionRadius);
                if (collider.gameObject == owner.gameObject && !canHitOwner) damage = 0;

                //Tweak the explosion force depending on the distance from the epicenter
                explosionForce *= (explosionRadius - distance) / explosionRadius;

                //Apply the damage/force
                collider.GetComponent<PlayerData>().TakeDamage(damage, TrigUtilities.VectorToDegrees(forceDirection), explosionForce, owner);
            }
        }

        //Setup animation
        LeanTween.cancel(gameObject);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        transform.localScale = new Vector2(explosionRadius * 2, explosionRadius * 2);

        //Play animation
        LeanTween.alpha(gameObject, 0, 1).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, transform.localScale * 1.5f, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            DeactivateObject();
        });

        //Shake camera
        CMShakeHandler.instance.IncreaseShakeAmount(explosionRadius * 0.1f);

        //DrawDebugExplosion();
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    void DrawDebugExplosion()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        int segments = 50;
        line.positionCount = segments + 1;
        float x, y;
        float angle = 0;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle);
            y = Mathf.Cos(Mathf.Deg2Rad * angle);
            line.SetPosition(i, new Vector2(x, y));
            angle += (360f / segments);
        }
    }

    public void SetExplosionDamage(int temp) { explosionDamage = temp; }
    public void SetExplosionForce(float temp) { explosionForce = temp; }
    public void SetExplosionRadius(float temp) { explosionRadius = temp; }
    public void SetOwner(PlayerData temp) { owner = temp; }
    public void SetCanHitOwner(bool temp) { canHitOwner = temp; }
}