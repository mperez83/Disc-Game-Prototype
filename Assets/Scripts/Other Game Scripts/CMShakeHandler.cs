using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMShakeHandler : MonoBehaviour
{
    public static CMShakeHandler instance;
    float shakeAmount;
    public CinemachineVirtualCamera cmVirtualCamera;
    CinemachineBasicMultiChannelPerlin cmNoise;



    void Awake()
    {
        instance = this;
        cmNoise = cmVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        //This convoluted algorithm makes it so that the larger the shakeAmount is, the faster it'll decrease (because a larger shakeAmount = a smaller denominator)
        //shakeAmount -= (Time.deltaTime / Mathf.Clamp(2 - (shakeAmount * 4), 0.1f, 2f));
        //if (shakeAmount < 0) shakeAmount = 0;

        shakeAmount *= 0.94f;
        if (shakeAmount < 0.1f) shakeAmount = 0;

        cmNoise.m_AmplitudeGain = shakeAmount;
        cmNoise.m_FrequencyGain = shakeAmount;
    }



    public void SetShakeAmount(float newShakeAmount) { shakeAmount = newShakeAmount * 10 * GameManager.instance.cameraShakeModifier; }
    public void IncreaseShakeAmount(float value) { shakeAmount += value * 10 * GameManager.instance.cameraShakeModifier; }
}