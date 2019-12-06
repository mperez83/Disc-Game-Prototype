using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBase : WeaponBase
{
    public int damage;
    public float knockbackPower;
    List<GameObject> damagedPlayers;
    public float playerYOffset;

    Transform holder;
    public Animator anim;
    Collider2D hitboxCollider;



    protected override void Start()
    {
        base.Start();
        hitboxCollider = GetComponent<PolygonCollider2D>();

        holder = transform.parent;
        holder.Translate(Vector2.up * playerYOffset);

        damagedPlayers = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetButtonDown("P" + owner.GetPlayerNum() + "_Fire"))
        {
            anim.SetBool("SwingBool", true);
        }

        if (Input.GetButtonUp("P" + owner.GetPlayerNum() + "_Fire"))
        {
            anim.SetBool("SwingBool", false);
        }
    }



    public void ToggleHitbox()
    {
        hitboxCollider.enabled = !hitboxCollider.enabled;
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