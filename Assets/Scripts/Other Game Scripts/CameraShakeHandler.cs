using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeHandler : MonoBehaviour
{
    public static CameraShakeHandler instance;
    Vector3 originalCameraPosition;
    float shakeAmount;



    void Awake()
    {
        instance = this;
        originalCameraPosition = transform.position;
    }

    void Update()
    {
        //This convoluted algorithm makes it so that the larger the shakeAmount is, the faster it'll decrease (because a larger shakeAmount = a smaller denominator)
        //shakeAmount -= (Time.deltaTime / Mathf.Clamp(2 - (shakeAmount * 4), 0.1f, 2f));
        //if (shakeAmount < 0) shakeAmount = 0;

        shakeAmount *= 0.95f;
        if (shakeAmount < 0.1f) shakeAmount = 0;

        if (shakeAmount > 0)
        {
            float angle = Random.Range(0f, 360f);
            Vector2 offset = TrigUtilities.DegreesToVector(angle) * shakeAmount;
            transform.position = originalCameraPosition + new Vector3(offset.x, offset.y);
        }
        else
        {
            transform.position = originalCameraPosition;
        }
    }



    public void SetShakeAmount(float newShakeAmount) { shakeAmount = newShakeAmount * GameManager.instance.cameraShakeModifier; }
    public void IncreaseShakeAmount(float value) { shakeAmount += value * GameManager.instance.cameraShakeModifier; }
}