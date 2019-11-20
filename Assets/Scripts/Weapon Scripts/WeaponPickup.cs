using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    float amplitude = 30;
    float period = 5;
    float deg;
    float sinVal;

    public GameObject weaponPrefab;

    void Update()
    {
        sinVal = amplitude * Mathf.Sin(deg * Mathf.Deg2Rad);
        deg += (360 * (1 / period)) * Time.deltaTime;
        if (deg > 360) deg = deg - 360;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, sinVal, transform.localEulerAngles.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerData>().GiveWeapon(weaponPrefab);
            Destroy(gameObject);
        }
    }
}