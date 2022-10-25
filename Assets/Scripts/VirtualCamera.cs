using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{

    public CinemachineVirtualCamera cinemachineVirtual;
    private bool CheckCameraZom = true;
    public InputManager input;
    void Start()
    {
         cinemachineVirtual = GetComponent<CinemachineVirtualCamera>();
         CinemachineFramingTransposer framingTransposer = cinemachineVirtual.AddCinemachineComponent<CinemachineFramingTransposer>();
    }
    void Update()
    {
        cinemachineVirtual.Follow = PlayerController.player.transform;
        //virtualcamera.LookAt = PlayerController.player.transform;
        if (input.bodyChangeActive && CheckCameraZom)
        {
            CheckCameraZom = false;
            ZomOutCamera();
            Debug.Log("ZomOut");
        }
        else if (!input.bodyChangeActive && !CheckCameraZom)
        {
            CheckCameraZom = true;
            CameraBackTpNormal();
            Debug.Log("BackToNormal");
        }
    }
    private void ZomOutCamera()
    {
        CinemachineFramingTransposer framingTransposer = cinemachineVirtual.AddCinemachineComponent<CinemachineFramingTransposer>();
        framingTransposer.m_CameraDistance=15f;

    }
    private void CameraBackTpNormal()
    {
        CinemachineFramingTransposer framingTransposer = cinemachineVirtual.AddCinemachineComponent<CinemachineFramingTransposer>();
        framingTransposer.m_CameraDistance=7.5f;
    }
}
