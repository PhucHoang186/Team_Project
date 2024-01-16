using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Controller;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public CamType cameraType;
    CinemachineComposer composer;
    CinemachineTransposer transposer;

    void Start()
    {
        composer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void SetLookAt(Transform target)
    {
        virtualCamera.m_LookAt = target;
    }

    public void SetFollow(Transform target)
    {
        virtualCamera.m_Follow = target;
    }
}
