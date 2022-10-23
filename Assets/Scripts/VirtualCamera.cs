using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera virtualcamera;
    private bool CheckCameraZom = true;
    public InputManager input;
    void Start()
    {

    }
    void Update()
    {
        virtualcamera.Follow = PlayerController.player.transform;
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
        Cinemachine.CinemachineFramingTransposer framingTransposer = virtualcamera.AddCinemachineComponent<Cinemachine.CinemachineFramingTransposer>();
        framingTransposer.m_CameraDistance=15f;

    }
    private void CameraBackTpNormal()
    {
        Cinemachine.CinemachineFramingTransposer framingTransposer = virtualcamera.AddCinemachineComponent<Cinemachine.CinemachineFramingTransposer>();
        framingTransposer.m_CameraDistance=7.5f;
    }
}
