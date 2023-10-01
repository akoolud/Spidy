using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    void LateUpdate()
    {
        Vector3 pos = PlayerController.Instance.transform.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
