using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    
    public Cinemachine.CinemachineVirtualCamera virtualcamera;
    void Start()
    {
        
    }
    void Update()
    {
        virtualcamera.Follow = PlayerController.player.transform;
        virtualcamera.LookAt = PlayerController.player.transform;
    }
}
