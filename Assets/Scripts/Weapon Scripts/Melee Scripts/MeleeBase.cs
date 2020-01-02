using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBase : WeaponBase
{
    public int damage;
    public float knockbackPower;
    public float camShakeStrength;
    List<GameObject> damagedPlayers;
    public float playerYOffset;

    Transform holder;
    Collider2D hitboxCollider;
    public Animator anim;
    public MeleeDisplay meleeDisplay;



    protected override void Start()
    {
        base.Start();
        hitboxCollider = GetComponent<PolygonCollider2D>();

        holder = transform.parent; //The holder is pretty much explicitly used for this purpose; offsetting the weapon by some amount upon startup
        holder.Translate(Vector2.up * playerYOffset);

        damagedPlayers = new List<GameObject>();

        meleeDisplay.ColorBlade(owner.GetComponent<SpriteRenderer>().color);
    }



    public void ActivateHitbox()
    {
        hitboxCollider.enabled = true;
        CMShakeHandler.instance.IncreaseShakeAmount(camShakeStrength);
    }

    public void DeactivateHitbox()
    {
        hitboxCollider.enabled = false;
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
                other.GetComponentInParent<PlayerData>().TakeDamage(damage, owner.GetPlayerMovement().GetPlayerAngle(), knockbackPower, owner);
                damagedPlayers.Add(other.gameObject);
            }
        }
    }
}