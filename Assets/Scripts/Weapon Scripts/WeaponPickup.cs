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
    public AudioSource pickUpAS;    //This is public because there are two audiosources on this object, which GetComponent can't tell the difference between
    public Collider2D col;

    bool alreadyPickedUp;   //Explicitly to fix the bug where multiple people are on the weapon spawner at the same time



    void Awake()
    {
        deg = Random.Range(0f, 360f);
        if (transform.parent) weaponSpawner = transform.parent.GetComponent<WeaponSpawner>();
    }

    void OnEnable()
    {
        weapon.enabled = false;
        col.enabled = true;
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
        if (other.CompareTag("Player") && !alreadyPickedUp)
        {
            //This ensures nothing funky happens when two people try to pick something up at the same time
            alreadyPickedUp = true;

            //Enable weapon scripts
            weapon.enabled = true;

            //Give the weapon to the player, depending on if it has a parent or not
            other.GetComponent<PlayerData>().GiveWeapon(gameObject);

            //If this weapon is part of a weapon spawner, trigger the weapon spawner respawn coroutine
            if (weaponSpawner) weaponSpawner.StartWeaponRespawn();

            //Play pickup sound
            pickUpAS.Play();
        }
    }
}