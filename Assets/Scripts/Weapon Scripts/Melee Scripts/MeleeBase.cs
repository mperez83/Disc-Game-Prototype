using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBase : WeaponBase
{
    public int damage;
    public float knockbackPower;
    List<GameObject> damagedPlayers;

    Animator anim;



    protected override void Start()
    {
        base.Start();
        damagedPlayers = new List<GameObject>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("P" + owner.GetPlayerNum() + "_Fire"))
        {
            anim.SetBool("Swinging", true);
        }
        else
        {
            anim.SetBool("Swinging", false);
        }
    }



    public void ClearDamagedPlayers()
    {
        damagedPlayers.Clear();
    }



    void OnTriggerStay2D(Collider2D other)
    {
        //Ignore owner
        if (other.gameObject == owner.gameObject) return;

        if (other.CompareTag("Player"))
        {
            if (!damagedPlayers.Contains(other.gameObject))
            {
                other.GetComponent<PlayerData>().TakeDamage(damage, owner.GetPlayerAngle(), knockbackPower);
                damagedPlayers.Add(other.gameObject);
            }
        }
    }
}