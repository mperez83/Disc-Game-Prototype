using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooledObject
{
    public float duration;
    float explosionForce;
    float explosionRadius;

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
                collider.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius + playerCollider.radius);
            }
        }

        //Setup animation
        LeanTween.cancel(gameObject);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        transform.localScale = new Vector2(explosionRadius*2, explosionRadius*2);

        //Play animation
        LeanTween.alpha(gameObject, 0, 1).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, transform.localScale * 1.5f, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });

        //DrawDebugExplosion();
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

    public void SetExplosionForce(float temp) { explosionForce = temp; }
    public void SetExplosionRadius(float temp) { explosionRadius = temp; }
}