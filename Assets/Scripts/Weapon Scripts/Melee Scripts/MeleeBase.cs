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

    public Renderer meleeMesh;
    public Material baseMaterial;
    public int indexOfBladeMaterial;

    public AudioSource swingAS;



    protected override void Start()
    {
        base.Start();
        hitboxCollider = GetComponent<PolygonCollider2D>();

        holder = transform.parent;
        holder.Translate(Vector2.up * playerYOffset);

        damagedPlayers = new List<GameObject>();

        Material[] tempMaterialsArray = new Material[meleeMesh.materials.Length];
        System.Array.Copy(meleeMesh.materials, tempMaterialsArray, meleeMesh.materials.Length);

        Material playerMaterial = new Material(baseMaterial);
        playerMaterial.color = owner.GetSpriteRenderer().color;

        tempMaterialsArray[indexOfBladeMaterial] = playerMaterial;
        meleeMesh.materials = tempMaterialsArray;
    }



    public void ActivateHitbox()
    {
        hitboxCollider.enabled = true;
        swingAS.PlayRandomize();
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