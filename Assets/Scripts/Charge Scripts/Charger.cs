using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    bool following;
    Transform followTarget;

    [Range(1, 100)]
    public int followQueueLength;
    Queue<Vector3> followQueue;
    bool filledQueue;
    Vector3 vel;
    float smoothFactor = 0.1f;

    //Charge stuff
    public GameObject chargePrefab;
    public float secondsPerDischarge;



    void Start()
    {
        followQueue = new Queue<Vector3>();
    }

    void Update()
    {
        if (following)
        {
            if (!filledQueue)
            {
                followQueue.Enqueue(followTarget.position);
                if (followQueue.Count >= followQueueLength) filledQueue = true;
            }
            else
            {
                followQueue.Enqueue(followTarget.position);
                transform.position = Vector3.SmoothDamp(transform.position, followQueue.Dequeue(), ref vel, smoothFactor);
            }
        }
    }



    IEnumerator ExpelChargeCoroutine()
    {
        while (true)
        {
            Charge newCharge = ObjectPooler.instance.SpawnFromPool("Charge", transform.position).GetComponent<Charge>();
            newCharge.SetTarget(followTarget);

            yield return new WaitForSeconds(secondsPerDischarge);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!following)
            {
                following = true;
                followTarget = other.transform;
                StartCoroutine(ExpelChargeCoroutine());
            }
        }
    }
}