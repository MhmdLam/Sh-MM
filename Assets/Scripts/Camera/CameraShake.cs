using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private float ShakeTimer;
    CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
    private void Awake()
    {
        basicMultiChannelPerlin = VirtualCamera.cinemachineVirtual.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Start()
    {

    }
    void Update()
    {
        if (ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime;
        }
        else
        {
            basicMultiChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
    public void Shake(float intensity, float time)
    {
        Debug.Log("Shaking");
        basicMultiChannelPerlin.m_AmplitudeGain = intensity;
        ShakeTimer = time;
        
    }
}
