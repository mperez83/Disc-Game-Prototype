using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    float amplitude = 30;
    float period = 5;
    float deg;
    float sinVal;

    public WeaponBase weapon;



    void Start()
    {
        deg = Random.Range(0f, 360f);
    }

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
            weapon.enabled = true;

            //Give the weapon to the player, depending on if it has a parent or not
            other.GetComponent<PlayerData>().GiveWeapon(gameObject);

            //If this is a gun, fix the angle and remove the box collider
            if (GetComponent<GunBase>())
            {
                transform.localEulerAngles = new Vector3(0, 90, -90);
            }

            //Disable pickup script stuff
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }
}