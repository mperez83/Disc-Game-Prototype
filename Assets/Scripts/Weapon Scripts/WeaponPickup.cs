using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    float amplitude = 30;
    float period = 5;
    float deg;
    float sinVal;

    public WeaponBase weapon;       //This is a public variable rather than a private GetComponent<> variable because the WeaponBase on melee weapons are not part of this transform
    WeaponSpawner weaponSpawner;    //May or may not exist; we check for it in the Start function



    void Start()
    {
        deg = Random.Range(0f, 360f);
        if (transform.parent) weaponSpawner = transform.parent.GetComponent<WeaponSpawner>();
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
            other.GetComponentInParent<PlayerData>().GiveWeapon(gameObject);

            //If this is a gun, fix the angle and remove the box collider
            if (GetComponent<GunBase>())
            {
                transform.localEulerAngles = new Vector3(0, 90, -90);
            }

            //If this weapon is part of a weapon spawner, trigger the weapon spawner respawn coroutine
            if (weaponSpawner) weaponSpawner.StartWeaponRespawn();

            //Disable pickup script stuff
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this);
        }
    }
}