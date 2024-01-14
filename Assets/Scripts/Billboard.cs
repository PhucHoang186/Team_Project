using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        target = CameraController.Instance.MainCamera.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(target.transform.position);
    }
}
