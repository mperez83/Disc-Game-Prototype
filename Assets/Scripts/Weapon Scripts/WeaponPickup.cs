using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    float amplitude = 30;
    float period = 5;
    float deg;
    float sinVal;

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
            //Enable weapon scripts
            GetComponent<WeaponBase>().enabled = true;
            other.GetComponent<PlayerData>().GiveWeapon(gameObject);

            //Fix position and rotation
            transform.localEulerAngles = new Vector3(0, 90, -90);

            //Disable pickup scripts
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }
}