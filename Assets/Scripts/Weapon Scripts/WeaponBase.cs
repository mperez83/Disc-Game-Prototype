using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected PlayerData owner;
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        owner = GetComponentInParent<PlayerData>();
        audioSource = GetComponent<AudioSource>();
    }
}