using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] potentialWeapons;
    public float spawnTimerLength;
    float spawnTimer;
    public Image timerImage;



    void Start()
    {
        if (!transform.GetComponentInChildren<WeaponPickup>()) CreateNewWeapon();
        timerImage.gameObject.SetActive(false);
    }



    public void StartWeaponRespawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        spawnTimer = spawnTimerLength;
        timerImage.gameObject.SetActive(true);

        while (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            timerImage.fillAmount = (spawnTimerLength - spawnTimer) / spawnTimerLength;
            yield return null;
        }

        timerImage.gameObject.SetActive(false);
        CreateNewWeapon();
    }

    void CreateNewWeapon()
    {
        Instantiate(potentialWeapons[Random.Range(0, potentialWeapons.Length)], transform);
    }
}
