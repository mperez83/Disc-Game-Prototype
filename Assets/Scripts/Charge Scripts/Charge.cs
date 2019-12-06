using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour, IPooledObject
{
    public int chargeValue;
    public float elasticStrength;
    public float elasticDegradeRate;

    bool canBeCollected;

    Rigidbody2D rb;
    Transform target;
    PlayerChargeData pcd;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnObjectSpawn()
    {
        float angle = Random.Range(0, 360);
        Vector2 direction = TrigUtilities.DegreesToVector(angle);

        rb.AddForce(direction * Random.Range(10f, 15f), ForceMode2D.Impulse);
        StartCoroutine(InvisibleFrames());
    }

    public void DeactivateObject()
    {
        rb.velocity = Vector2.zero;
        canBeCollected = false;
        gameObject.SetActive(false);
    }



    void FixedUpdate()
    {
        rb.AddForce(elasticStrength * (target.transform.position - transform.position));
        rb.velocity *= elasticDegradeRate;
    }

    IEnumerator InvisibleFrames()
    {
        yield return new WaitForSeconds(0.5f);
        canBeCollected = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (canBeCollected && other.GetComponent<PlayerChargeData>() == pcd)
            {
                pcd.GainCharge(1);
                DeactivateObject();
            }
        }
    }

    public void SetTarget(Transform newTarget) { target = newTarget; pcd = target.GetComponent<PlayerChargeData>(); }
}