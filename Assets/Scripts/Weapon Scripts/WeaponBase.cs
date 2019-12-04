using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected PlayerData owner;

    protected virtual void Start()
    {
        owner = GetComponentInParent<PlayerData>();
    }
}