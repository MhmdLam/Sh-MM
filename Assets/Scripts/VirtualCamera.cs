using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{

    public static CinemachineVirtualCamera cinemachineVirtual;
    CinemachineFramingTransposer framingTransposer;
    private bool CheckCameraZom = true;
    public InputManager input;
    void Awake()
    {
        cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
        framingTransposer = cinemachineVirtual.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    void Start()
    {


        framingTransposer.m_CameraDistance = 7.5f;
    }
    void Update()
    {

    }
    public void ZoomOut()
    {
        framingTransposer.m_CameraDistance = 15f;

    }
    public void ZoomIn()
    {
        framingTransposer.m_CameraDistance = 7.5f;
    }
}
