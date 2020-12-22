using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
    private float _shakeElapsedTime = 0f;

    void Start()
    {
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        CheckCameraShake();
    }

    public void CameraShake(float shakeDuration, float shakeAmplitude, float shakeFrequency)
    {
        _shakeElapsedTime = shakeDuration;
        _virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
        _virtualCameraNoise.m_FrequencyGain = shakeFrequency;
    }

    public void CheckCameraShake()
    {
        if (_shakeElapsedTime >= 0)
        {
            _shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            _virtualCameraNoise.m_AmplitudeGain = 0f;
            _shakeElapsedTime = 0f;
        }
    }
}
