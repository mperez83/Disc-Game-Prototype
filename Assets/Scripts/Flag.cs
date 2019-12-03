using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    bool following;
    Transform targetFollow;
    [Range(1, 100)]
    public int followQueueLength;
    Queue<Vector3> followQueue;
    bool filledQueue;
    Vector3 vel;
    float smoothFactor = 0.15f;

    public PlayerData playerData;



    void Start()
    {
        followQueue = new Queue<Vector3>();
        GetComponent<SpriteRenderer>().color = playerData.playerColors[playerData.GetPlayerNum() - 1];
    }

    void Update()
    {
        if (following)
        {
            if (!filledQueue)
            {
                followQueue.Enqueue(playerData.transform.position);
                if (followQueue.Count >= followQueueLength) filledQueue = true;
            }
            else
            {
                followQueue.Enqueue(playerData.transform.position);
                transform.position = Vector3.SmoothDamp(transform.position, followQueue.Dequeue(), ref vel, smoothFactor);
                //transform.position = followQueue.Dequeue();
            }
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (!following && other.gameObject == playerData.gameObject)
        {
            following = true;
            targetFollow = other.transform;
        }
    }
}